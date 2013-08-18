#!/usr/bin/env python

## LOBBY SERVER
## LobbyServer/__init__.py

import pika
import json
import uuid
import random
import logging
import thread
from datetime import datetime
from time import sleep
from functools import partial


class Server:

    connection = None
    channel = None
    game_list = None
    game_servers = None

    ping_wait = 1
    ping_interval = 2

    def __init__(self, broker, port):

        self.game_list = {}
        self.game_servers = {}

        self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
        self.channel = self.connection.channel()

        self.callbacks_waiting = {}

        print 'Server started in Lobby Server mode.'


    def __on_callback(self, ch, method, props, body):
      if props.correlation_id in self.callbacks_waiting:
        f = self.callbacks_waiting[props.correlation_id]
        del(self.callbacks_waiting[props.correlation_id])
        f(ch, method, props, body)


    def start_pinging(self):
      while True:
        if len(self.game_servers.keys()) > 0:
          game_server_id = random.choice(self.game_servers.keys())
          self.__ping(game_server_id)
        sleep(self.ping_interval)


    def __ping(self, game_server_id):
      print 'Ping %s' % game_server_id

      corr_id = str(uuid.uuid4())


      self.callbacks_waiting[corr_id] = partial(self.__on_ping_ack, server_id=game_server_id)

      self.channel.basic_publish(exchange=game_server_id,
                       routing_key='ping',
                       properties=pika.BasicProperties(                                                        content_type="application/json",
                                                        reply_to = self.ping_callback_queue,
                                                        correlation_id = corr_id),
                       body="")

      sleep(self.ping_wait)
      if not (game_server_id in self.game_servers):
        print "Server %s left without saying goodbye. :( " % (game_server_id)

      elif not ('last_ping' in self.game_servers[game_server_id]):
        print "Server %s left without saying goodbye. :( " % (game_server_id)
        del self.game_servers[game_server_id]
      elif((datetime.now() - self.game_servers[game_server_id]['last_ping']).total_seconds() > self.ping_wait):
          print "Server %s left without saying goodbye. :( " % (game_server_id)
          del self.game_servers[game_server_id]


    def __on_ping_ack(self, ch, method, props, body, server_id):
      try:
        self.game_servers[server_id]['last_ping'] = datetime.now()
      except:
        pass



    # Create a game to be joined by other players
    def __create_game(self, ch, method, props, body):

        print 'Go!!!!!'
        params = json.loads(body)

        game_id = str(uuid.uuid4())

        broker = '127.0.0.1'
        server = random.choice(self.game_servers.keys())


        game_params = {'id': game_id,
                       'name': params['name'],
                       'broker': broker,
                       'server': server}

        self.game_list[game_id] = game_params
        print json.dumps(game_params)
        #ch.basic_publish(exchange='',
        #             routing_key=props.reply_to,
        #             properties=pika.BasicProperties(
        #                 correlation_id = props.correlation_id
        #                 ),
        #             body=json.dumps(game_params))

        ch.basic_publish(exchange=server,
                         routing_key='create_game',
                         properties=pika.BasicProperties(),
                         body='WELL ELLO!')


        self.__push_game_list()

        ch.basic_ack(delivery_tag = method.delivery_tag)

    def __push_game_list(self):

        self.channel.basic_publish(
                           exchange='game_list',
                           routing_key='',
                           properties=pika.BasicProperties(
                                content_type="application/json"),
                           body=json.dumps(self.game_list)
                        )

    def __register_game_server(self, ch, method, props, body):

      params = json.loads(body)

      server_id = params['id']
      print "Got request for game server '%s'; assigned id '%s'" %(params['name'], server_id)


      server_params = {'id': server_id, 'name': params['name']}

      self.game_servers[server_id] = server_params

      ch.basic_publish(exchange="",
                       routing_key=props.reply_to,
                       properties=pika.BasicProperties(
                                                       correlation_id = props.correlation_id
                                                       ),
                       body=json.dumps(server_params))

      ch.basic_ack(delivery_tag = method.delivery_tag)


    def serve(self):

        self.channel.exchange_declare(exchange="lobby", type='direct')

        # Set up callback queues
        callback_result = self.channel.queue_declare(exclusive=True)
        callback_queue = callback_result.method.queue
        self.channel.basic_consume(self.__on_callback, queue=callback_queue)

        # Set up server registration queues
        self.channel.queue_declare(queue='game_server_announce')
        self.channel.basic_consume(self.__register_game_server, queue='game_server_announce')
        self.channel.queue_bind(exchange='lobby',
                       queue='game_server_announce',
                       routing_key='game_server_announce')

        # Set up game creation queues
        create_game_result = self.channel.queue_declare()
        create_game_queue = create_game_result.method.queue
        self.channel.queue_bind(exchange='lobby',
                       queue=create_game_queue,
                       routing_key='create_game')
        self.channel.basic_consume(self.__create_game, queue=create_game_queue)


        # Set up ping queues
        ping_callback_result = self.channel.queue_declare(exclusive=True)
        self.ping_callback_queue = callback_result.method.queue
        self.channel.basic_consume(self.__on_callback,
                                   queue=self.ping_callback_queue,
                                   no_ack=True)
        # Start serving
        thread.start_new_thread ( self.start_pinging, () )

        self.channel.start_consuming()
