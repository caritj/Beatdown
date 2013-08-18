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


class Server:

    connection = None
    channel = None
    game_list = None
    game_servers = None

    ping_wait = 0.5
    ping_interval = 5

    def __init__(self, broker, port):

        self.game_list = {}
        self.game_servers = {}

        self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
        self.channel = self.connection.channel()

        print 'Server started in Lobby Server mode.'

    def __on_ping_ack(self, ch, method, props, body):
      params = json.loads(body)
      try:
        self.game_servers[params['ack']]['last_ping'] = datetime.now()
      except:
        pass

    def start_pinging(self):
      while True:
        if len(self.game_servers.keys()) > 0:
          game_server_id = random.choice(self.game_servers.keys())
          self.__ping(game_server_id)
        sleep(self.ping_interval)

    def __ping(self, game_server_id):
      print 'Ping %s' % game_server_id

      corr_id = str(uuid.uuid4())

      self.channel.basic_publish(exchange=game_server_id,
                       routing_key='ping',
                       properties=pika.BasicProperties(),
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




    # Create a game to be joined by other players
    def __process_create_game(self, ch, method, props, body):

        if not len(self.game_servers) > 0:
          return

        params = json.loads(body)

        game_id = str(uuid.uuid4())

        broker = '127.0.0.1'
        server = random.choice(self.game_servers.keys())


        game_params = {'id': game_id,
                       'name': params['name'],
                       'broker': broker,
                       'server': server}

        self.game_list[game_id] = game_params

        ch.basic_publish(exchange=server,
                         routing_key='create_game',
                         properties=pika.BasicProperties(
                                                         reply_to = props.reply_to,
                                                         correlation_id = props.correlation_id),
                         body=json.dumps(game_params))


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

      server_id = str(uuid.uuid4())
      print "Got request for game server '%s'; assigned id '%s'" %(params['name'], server_id)


      server_params = {'id': server_id, 'name': params['name']}

      self.game_servers[server_id] = server_params

      ch.basic_publish(exchange='game_admin',
                       routing_key=props.reply_to,
                       properties=pika.BasicProperties(
                                                       correlation_id = props.correlation_id
                                                       ),
                       body=json.dumps(server_params))

      ch.basic_ack(delivery_tag = method.delivery_tag)

    def serve(self):
        self.channel.exchange_declare(exchange="game_list", type='fanout')


        # Set up game creation queues
        self.channel.queue_declare(queue='create_game')
        self.channel.basic_consume(self.__process_create_game, queue='create_game')


        # Set up ping queues
        self.channel.queue_declare(queue='ping')
        self.channel.basic_consume(self.__on_ping_ack, queue='ping')

        # Set up server registration queues
        self.channel.exchange_declare(exchange="game_admin", type='direct')
        self.channel.queue_declare(queue='game_server_announce')

        self.channel.queue_bind(exchange='game_admin',
                       queue='game_server_announce',
                       routing_key='game_server_announce')


        self.channel.basic_consume(self.__register_game_server, queue='game_server_announce')

        # Start serving
        thread.start_new_thread ( self.start_pinging, () )

        self.channel.start_consuming()
