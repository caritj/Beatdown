using System;
//using Game.Net;
	
namespace BeatDown.Game
{
	public class Game:IDisposable
	{
		
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
	
	
	
	public class Unit
	{
		public Unit ()
		{
		}
	}
}

