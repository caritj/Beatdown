using System;
//using Game.Net;
using Beatdown.Game;
	
namespace BeatDown.Game
{
	public class Game:IDisposable
	{
		public static State State = new State();
//		private Server server;
		
		public Game (Settings s)
		{
		}
		
		public static Game Create (Settings s)
		{
			return new Game( s);
		}

		public static Game Join (Settings s )
		{
			return new Game( s);
		}		


		public void Dispose ()
		{
			//TODO
		}



	}
	
	
	

}

