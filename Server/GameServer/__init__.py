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
      self.registered = False
      self.game_list = {}
      self.callbacks_waiting = {}

      print 'Server started in Game Server mode.'


    def __on_callback(self, ch, method, props, body):
      if props.correlation_id in self.callbacks_waiting:
        f = self.callbacks_waiting[props.correlation_id]
        del(self.callbacks_waiting[props.correlation_id])
        f(ch, method, props, body)



    def __on_lobby_reg(self, ch, method, props, body):

      params = json.loads(body)
      self.registered = True


    def __on_ping(self, ch, method, props, body):
      ch.basic_publish(exchange='',
                       routing_key=props.reply_to,
                       properties=pika.BasicProperties(correlation_id = props.correlation_id),
                       body=json.dumps({'ack': self.id}))

    def __on_create_game(self, ch, method, props, body):
      print body
      print "... ..."
      #params = json.loads(body)


    def serve(self):

      self.id = str(uuid.uuid4())

      self.channel.exchange_declare(exchange=self.id, type="topic")


      # Set up callback queue

      callback_result = self.channel.queue_declare(exclusive=True)
      callback_queue = callback_result.method.queue

      self.channel.basic_consume(
                          self.__on_callback,
                          queue=callback_queue
                          )


      ping_queue = self.channel.queue_declare(exclusive=True)
      ping_queue_name = ping_queue.method.queue
      self.channel.queue_bind(exchange=self.id,
                                queue=ping_queue_name,
                                routing_key='ping')
      self.channel.basic_consume(
                                 self.__on_ping,
                                 no_ack=True, queue=ping_queue_name
                                   )

      # Register with lobby server

      lobby_reg_corr_id = str(uuid.uuid4())
      self.callbacks_waiting[lobby_reg_corr_id] = self.__on_lobby_reg

      self.channel.basic_publish(
                        exchange='lobby',
                        routing_key='game_server_announce',
                        properties=pika.BasicProperties(
                                                        content_type="application/json",
                                                        reply_to = callback_queue,
                                                        correlation_id = lobby_reg_corr_id),
                        body=json.dumps({'name': 'Test Server!', 'id': self.id})
                        )

      print "Waiting or acknowledgement from lobby server..."

      while not self.registered:
        self.connection.process_data_events()

      print 'Registered with lobby as %s (%s).' % (self.name, self.id)

      create_game_queue = self.channel.queue_declare(exclusive=True)
      create_game_name = ping_queue.method.queue
      self.channel.queue_bind(exchange=self.id,
                                queue=create_game_name,
                                routing_key='create_game')
      self.channel.basic_consume(
                                 self.__on_create_game,
                                 no_ack=True, queue=create_game_name
                                   )

      # Start serving'
      self.channel.start_consuming()


