#!/usr/bin/env python

## LOBBY SERVER
## LobbyServer/__init__.py

import pika
import json
import uuid
import random
import logging


class Server:

    connection = None
    channel = None
    game_list = None
    game_servers = None

    def __init__(self, broker, port):

        self.game_list = {}
        self.game_servers = {}

        self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
        self.channel = self.connection.channel()

        print 'Server started in Lobby Server mode.'

    # Create a game to be joined by other players
    def __process_create_game(self, ch, method, props, body):

        params = json.loads(body)

        game_id = str(uuid.uuid4())

        broker = '127.0.0.1'
        server = random.choice(self.game_servers.keys())


        game_params = {'id': game_id,
                       'name': params['name'],
                       'broker': broker,
                       'server': server}

        self.game_list[game_id] = game_params

        ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(
                         correlation_id = props.correlation_id
                         ),
                     body=json.dumps(game_params))

        ch.basic_publish(exchange=server,
                         routing_key='create_game',
                         properties=pika.BasicProperties(),
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

      #self.channel.exchange_declare(exchange=self.id, type="topic")

      ch.basic_ack(delivery_tag = method.delivery_tag)

    def serve(self):
        self.channel.exchange_declare(exchange="game_list", type='fanout')


        # Set up game creation queues
        self.channel.queue_declare(queue='create_game')
        self.channel.basic_consume(self.__process_create_game, queue='create_game')


        # Set up server registration queues
        self.channel.exchange_declare(exchange="game_admin", type='direct')

        self.channel.queue_bind(exchange='game_admin',
                       queue='game_server_announce',
                       routing_key='game_server_announce')

        self.channel.basic_consume(self.__register_game_server, queue='game_server_announce')


        # Start serving
        self.channel.start_consuming()
