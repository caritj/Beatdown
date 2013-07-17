using System;
using ZMQ;

namespace BeatDown.Net
{
	public class Connection
	{
		private ZMQ.Context context;
		private ZMQ.Socket reqSocket;
		private ZMQ.Socket subSocket;
		private string url;

		public Connection (string url)
		{

			this.url = url;
			this.context = new ZMQ.Context (2);
			this.reqSocket = this.context.Socket (SocketType.REQ);
			this.subSocket = this.context.Socket (SocketType.SUB);

			this.reqSocket.Connect (this.url);
			this.subSocket.Connect (this.url);

		}

		public static IConnection GetControlConnection()
		{
			return (IConnection)(new Connection ("tcp://localhost:9000"));
		}


		public IMessage Send(IMessage message)
		{
			this.reqSocket.Send(message.ToString (), System.Text.Encoding.Unicode);
			return this.reqSocket.Recv (System.Text.Encoding.Unicode);
		}


	}
}

