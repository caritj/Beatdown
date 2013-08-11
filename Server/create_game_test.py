#!/usr/bin/env python

import pika
import json
import uuid
from functools import partial

if __name__ == "__main__":

    response = None

    def on_response(ch, method, props, body, correlation_id):
        if not correlation_id == props.correlation_id: return
        print body

    def on_game_list(ch, method, props, body):
        print "-----------------------"
        print body

    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost', 5672))
    channel = connection.channel()

    result = channel.queue_declare(exclusive=True)
    callback_queue = result.method.queue


    game_list_result = channel.queue_declare(exclusive=True)
    game_list_queue = game_list_result.method.queue
    channel.queue_bind(exchange='game_list',
                   queue=game_list_queue)


    corr_id = str(uuid.uuid4())

    channel.basic_consume(
                          partial(on_response, correlation_id=corr_id),
                          no_ack=True, queue=callback_queue)

    channel.basic_consume(
                          on_game_list,
                          no_ack=True,
                          queue = game_list_queue
                          )

    channel.basic_publish(
                           exchange='',
                           routing_key='create_game',
                           properties=pika.BasicProperties(
                                content_type="application/json",
                                reply_to = callback_queue,
                                correlation_id = corr_id),
                           body=json.dumps({'name': 'Test Game!'})
                        )


    while response is None:
      connection.process_data_events()


