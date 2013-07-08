using System;
using Game.Net;
	
namespace Game
{
	public class Game
	{
		
		private Server server;
		
		public Game ()
		{
		}
		
		public static Game Create ()
		{
			return new Game();
		}
		
		public static Game Join ()
		{
			return new Game();
		}

	}
	
	
	
	public class Unit
	{
		public Unit ()
		{
		}
	}
}

