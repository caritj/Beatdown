#!/usr/bin/env python

import json
import uuid

class instance:

  id = None
  channel = None
  server = None
  teams = {}
  users = {}
  units = {}

  def __init__(self, game_id, server):
    self.id = game_id
    self.server = server


    # Wait for clients and settings

    # Set up queue for joining clients
    client_join_result = self.server.channel.queue_declare(exclusive=True)
    client_join_queue = client_join_result.method.queue

    self.channel.queue_bind(exchange=server.id,
                            queue=client_join_queue,
                            routing_key=self.id+'.join_game')

    self.server.channel.basic_consume(
                                      self.__process_join_game,
                                      queue=client_join_queue)





  def __process_join_game(self, ch, method, props, body):
    print "Someone's joining!"
    params = json.loads(body)
    team_id = max(self.teams.keys()) + 1 or 0

    user = User(params['user_id'])

    self.teams[team_id] = {'name': params['team_name'], 'user': user}
    self.users[user.id] = user

    #for unit_id in params['units']:
    #  unit[unit_id] = Unit.get_unit(unit_id)

    self.channel.basic_publish(
                              exchange='',
                              routing_key=props.reply_to,
                              properties=pika.BasicProperties(
                                                              content_type="application/json",
                                                              correlation_id = props.correlation_id),
                              body=json.dumps({'status': 'SUCCESS'})
                              )






