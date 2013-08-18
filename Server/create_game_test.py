#!/usr/bin/env python

import pika
import json
import uuid
from functools import partial


class Client:

  def __on_callback(self, ch, method, props, body, correlation_id):
    if correlation_id in self.callbacks_waiting:
      f = self.callbacks_waiting[correlation_id]
      del(self.callbacks_waiting[correlation_id])
      f(ch, method, props, body)

  def __on_game_list(self, ch, method, props, body):
    print "-----------------------"
    print body

  def __on_game_creation(self, ch, method, props, body):
    params = json.loads(body)
    self.server = params['server']
    print 'GAME CREATED!'

  def __on_game_join(self, ch, method, props, body):
    params = json.loads(body)
    print 'GAME JOINED!'
    print body

  def __init__(self, con, ch):
    self.server = None
    self.channel = ch
    self.connection = con
    self.callbacks_waiting = {}

    result = self.channel.queue_declare(exclusive=True)
    callback_queue = result.method.queue

    game_list_result = self.channel.queue_declare(exclusive=True)
    game_list_queue = game_list_result.method.queue
    self.channel.queue_bind(exchange='game_list',
                   queue=game_list_queue)

    corr_id = str(uuid.uuid4())

    self.channel.basic_consume(
                          partial(self.__on_callback, correlation_id=corr_id),
                          no_ack=True, queue=callback_queue)

    self.channel.basic_consume(
                          self.__on_game_list,
                          no_ack=True,
                          queue = game_list_queue
                          )

    self.callbacks_waiting[corr_id] = self.__on_game_creation
    self.channel.basic_publish(
                           exchange='',
                           routing_key='create_game',
                           properties=pika.BasicProperties(
                                content_type="application/json",
                                reply_to = callback_queue,
                                correlation_id = corr_id),
                           body=json.dumps({'name': 'Test Game!'})
                        )
    while self.server is None:
      self.connection.process_data_events()

    print "CREATED!"
    print 'Server: %s' % self.server

    while True:
      self.connection.process_data_events()



if __name__ == "__main__":


    response = None

    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost', 5672))
    channel = connection.channel()

    client = Client(connection, channel)











    #user_id = 'pcaritj'
    #print user_id
    #corr_id = str(uuid.uuid4())

    #callbacks_waiting[corr_id] = __on_game_join
    #channel.basic_publish(
    #                      exchange=server,
    #                      routing_key='create_game',
    #                     properties=pika.BasicProperties(
    #                                                     content_type="application/json",
    #                                                      reply_to = callback_queue,
    #                                                      correlation_id = corr_id),
    #                      body=json.dumps({'user_id': user_id, 'team_name': 'Beans'}))


    #while response is None:
    #  connection.process_data_events()


