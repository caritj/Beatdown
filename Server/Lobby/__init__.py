import pika;
import json;
import uuid;


class Server:
    
    connection = None
    channel = None
    game_list = None
    
    def __init__(self, broker="localhost", port=5672):
    
        self.game_list = {}
    
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
        self.channel = self.connection.channel()
        
    def __process_create_game(self, ch, method, props, body):
        
        params = json.loads(body)
        
        game_id = uuid.uuid4
        self.game_list[game_id] = params['name']
        
        ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(
                         correlation_id = props.correlation_id
                         ),
                     body=json.dumps({'id': game_id}))
        
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
    
    def serve(self):
        self.channel.exchange_declare(exchange="game_list", type='fanout')
        self.channel.queue_declare(queue='create_game')
        self.channel.basic_consume(self.__process_create_game, queue='create_game')
        self.channel.start_consuming()
                