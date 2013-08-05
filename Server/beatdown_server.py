#!/usr/bin/env python

from optparse import OptionParser
import Lobby
import Hello_world

if __name__ == "__main__":
    parser = OptionParser()
    parser.add_option("-l", "--lobby", action="store_true", dest='lobby', default=False)
    parser.add_option("", "--hello_world", action="store_true", dest='hello_world', default=False)
    (options, args) = parser.parse_args()
    
    server = None
    
    if options.lobby: server = Lobby.Server()
    elif options.hello_world: server = Hello_world.Server()
    server.serve()
    
    
    