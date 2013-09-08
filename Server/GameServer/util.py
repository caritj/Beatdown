import json
from functools import partial

def bind_game_to_exchange(game, channel, exchange):
    print "Binding game '%s' to exchange '%s'" % (game, exchange)

    # Listen for "join_game" requests
    join_q = channel.queue_declare(exclusive=True)
    join_queue = join_q.method.queue
    channel.queue_bind(exchange=self.id,
                       queue=join_queue,
                       routing_key='%s.join' % game)

    channel.basic_consume(partial(__join_consume, game=game), queue=join_queue )


    # Listen for "close_game" requests
    close_q = channel.queue_declare(exclusive=True)
    close_queue = join_q.method.queue
    channel.queue_bind(exchange=self.id,
                       queue=close_queue,
                       routing_key='%s.close' % game)

    channel.basic_consume(partial(__close_consume, game=game), queue=close_queue )



def __close_consume(ch, method, props, body, game):
    __close(game)

    ch.basic_ack(delivery_tag = method.delivery_tag)

def __close(game):
    game.close()



def __join_consume(ch, method, props, body, game):
    params = json.dumps(body)
    __join(game, **params)

    ch.basic_ack(delivery_tag = method.delivery_tag)


def __join(game, player_dict):

    player = Player(player_dict['id'])
    for u in player_dict['units']:
        unit = Unit(u['id'], player=player, team=u['team'])
        player.units[unit.id] = unit

    game.join_player(player)





