#!/usr/bin/env python

import json
import uuid
from player import *
from unit import *
from map import *


class instance:

  id = None
  channel = None
  server = None
  teams = None
  players = None
  units = None
  is_open = True

  def __init__(self, game_id, server):
    self.id = game_id
    self.server = server
    self.players = {}
    self.units = {}
    teams = {}

  def join_player(self, player):
        if self.is_open:
            self.players[player.id] = player
            for unit in player.units: self.units[unit.id] = unit

  def close(self):
            self.is_open = False









