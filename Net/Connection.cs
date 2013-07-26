using System;
using ZMQ;

namespace BeatDown.Net
{
	public class Connection:IConnection
	{
		private ZMQ.Context context;
		private ZMQ.Socket reqSocket;
		private ZMQ.Socket subSocket;
		private string url;

		public Connection (string url)
		{

			this.url = url;
			this.context = new ZMQ.Context(1);
			this.reqSocket = this.context.Socket (SocketType.REQ);
			//this.subSocket = this.context.Socket (SocketType.SUB);

			this.reqSocket.Connect (this.url);
			//this.subSocket.Connect (this.url);

		}

		public Game.Game CreateGame(string name, Game.Settings settings)
		{
			return new Game.Game (settings);
		}

		public Game.Game JoinGame(Int64 id, Game.Settings settings)
		{
			return new Game.Game (settings);
		}

		public static IConnection GetControlConnection()
		{
			return (IConnection)(new Connection ("tcp://localhost:8787"));
		}


		public IMessage Send(IMessage message)
		{
			//this.reqSocket.Send(message.ToString (), System.Text.Encoding.Unicode);
			string req = "Howdy.";
			this.reqSocket.Send(req, System.Text.Encoding.UTF8);
			Console.WriteLine ("Got it?");
			string response = this.reqSocket.Recv(System.Text.Encoding.UTF8, ZMQ.SendRecvOpt.NONE);
			Console.WriteLine ("Got it!");
			Console.WriteLine (response);

			//return MessageDecoder.Decode(this.reqSocket.Recv (System.Text.Encoding.UTF8));
			return new TestMessage ();
		}


	}
}

