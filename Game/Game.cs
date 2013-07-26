using System;
//using Game.Net;

	
namespace BeatDown.Game
{
	public class Game:IDisposable
	{
		public static Game Instance;

		public static State State = new State();
		public Manager Manager = new Manager();

		public Player LocalPlayer= new Player("steve",1,1);
		public Player WhoseTurn = new Player("steve",1,1);

		public Game (Settings s)
		{
			Instance =this;
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

