#!/usr/bin/env python

from optparse import OptionParser
import LobbyServer
import GameServer

if __name__ == "__main__":
    parser = OptionParser()
    parser.add_option("-l", "--lobby", action="store_false", dest='game', default=False)
    parser.add_option("-g", "--game", action="store_true", dest='game', default=True)
    parser.add_option("-b", "--broker", dest='broker', default="127.0.0.1")
    parser.add_option("-p", "--port", dest='port', default=5672)
    (options, args) = parser.parse_args()

    server = None

    if options.game: server = GameServer.Server(broker=options.broker, port=options.port)
    else: server = LobbyServer.Server(broker=options.broker, port=options.port)

    server.serve()


