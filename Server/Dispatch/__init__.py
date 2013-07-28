import zmq;

class Server:
    
    host = None
    _context = None
    _socket = None
    
    def __init__(self, host="tcp://*", port=8787):
        self.host = host.rstrip('/').rstrip(':') + ":" + str(port)
        
    def listen(self):
        self._context = zmq.Context(1)
        self._socket = self._context.socket(zmq.REP)
        print "Listening at %s" % self.host
        self._socket.bind(self.host)
        
        while True:
            print "EGAD!"
            
            message = self._socket.recv()
            print "Message: " + message
            print len(message)
            response = self._handle_message(message)
            print "Response:" + response
            self._socket.send(response)
            
    def _handle_message(self, message):
        return "Hi!"
        
        
    