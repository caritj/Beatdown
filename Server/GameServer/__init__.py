#!/usr/bin/env python


## GAME SERVER
## GameServer/__init__.py

import pika
import json
import uuid
import logging
import Game

from functools import partial

class Server:

    connection = None
    channel = None

    game_list = None
    id = None

    def __init__(self, broker, port):
      self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
      self.channel = self.connection.channel()
      self.id = None
      self.name = None
      self.game_list = {}

      print 'Server started in Game Server mode.'


    def __on_lobby_reg(self, ch, method, props, body, correlation_id):
      params = json.loads(body)
      print 'Registered with lobby as %s (%s).' % (params['name'], params['id'])

      self.name = params['name']
      self.id = params['id']


    def __on_create_game(self, ch, method, props, body):

      print 'Creating game...'
      params = json.loads(body)

      game_id = params['id']
      game_instance = Game.instance(game_id, self)

      game_dict = {'params': params, 'instance': game_instance }

      self.game_list[game_id] = game_dict

      print game_dict
      print 'Sending message to game server...'
      ch.basic_publish(exchange='',
                       routing_key=props.reply_to,
                       properties=pika.BasicProperties(
                                                       correlation_id = props.correlation_id),
                       body=json.dumps({'params': params, 'server': self.id}))



    def __on_ping(self, ch, method, props, body):
      ch.basic_publish(exchange='',
                       routing_key='ping',
                       properties=pika.BasicProperties(),
                       body=json.dumps({'ack': self.id}))

    def serve(self):

        # Register with lobby server

        lobby_reg_result = self.channel.queue_declare(exclusive=True)
        lobby_reg_callback_queue = lobby_reg_result.method.queue
        lobby_reg_correlation_id = str(uuid.uuid4())

        self.channel.queue_bind(exchange='game_admin',
                       queue=lobby_reg_callback_queue,
                       routing_key=lobby_reg_callback_queue)

        self.channel.basic_consume(
                          partial(self.__on_lobby_reg, correlation_id=lobby_reg_correlation_id),
                          no_ack=True, queue=lobby_reg_callback_queue
                          )

        self.channel.basic_publish(
                           exchange='game_admin',
                           routing_key='game_server_announce',
                           properties=pika.BasicProperties(
                                content_type="application/json",
                                reply_to = lobby_reg_callback_queue,
                                correlation_id = lobby_reg_correlation_id),
                           body=json.dumps({'name': 'Test Server!'})
                        )


        # Wait for id assignment from lobby server before serving games

        while self.id is None:
          self.connection.process_data_events()


        self.channel.exchange_declare(exchange=self.id, type="topic")
        admin_queue = self.channel.queue_declare(exclusive=True)
        admin_queue_name = admin_queue.method.queue

        self.channel.queue_bind(exchange=self.id,
                                queue=admin_queue_name,
                                routing_key='create_game')


        ping_queue = self.channel.queue_declare(exclusive=True)
        ping_queue_name = ping_queue.method.queue
        self.channel.queue_bind(exchange=self.id,
                                queue=ping_queue_name,
                                routing_key='ping')

        self.channel.basic_consume(
                                   self.__on_ping,
                                   no_ack=True, queue=ping_queue_name
                                   )

        self.channel.basic_consume(
                          self.__on_create_game,
                          no_ack=True, queue=admin_queue_name
                          )




        print 'Ready!'

        # Start serving
        while True:
          self.channel.start_consuming()