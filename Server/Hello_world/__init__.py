import pika;
import logging;


class Server:
    
    connection = None
    channel = None
    
    def __init__(self, broker="localhost", port=5672):
        
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(broker, port))
        self.channel = self.connection.channel()
        
        print "Hello World Server Initialized."
        
    

    def __process(self, ch, method, properties, body):
        print " [x] Received %r" % (body,)
    
    def serve(self):
        self.channel.queue_declare(queue='hello')
        self.channel.basic_consume(self.__process,
                          queue='hello',
                          no_ack=True)
        print "Hello World Server Listening."
        self.channel.start_consuming()
                