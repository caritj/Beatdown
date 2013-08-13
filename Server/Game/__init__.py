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

  def __process_join_game(self, ch, method, props, body):
    params = json.loads(body)
    team_id = max(self.teams.keys()) + 1 or 0

    user = User(params['user_id'])

    self.teams[team_id] = {'name': params['team_name'], 'user': user}
    self.users[user.id] = user

    for unit_id in params['units']:
      unit[unit_id] = Unit.get_unit(unit_id)

    self.channel.basic_publish(
                              exchange='game_admin',
                              routing_key=server.id+'.'+self.id+'.users.'+user.id,
                              properties=pika.BasicProperties(
                                                              content_type="application/json",
                                                              correlation_id = lobby_reg_correlation_id),
                              body=json.dumps({'status': 'SUCCESS'})
                              )






