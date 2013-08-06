using System;
//using Game.Net;
/*
 *  This is the part taht simultes what happens in the game.
 *  It determines things like who hits who can move an whose turn it is.
 * 
 * 
 * 
 */
	
namespace BeatDown.Game
{
	public class Game:IDisposable
	{
		public static Game Instance;
		public Settings Settings;

		public Random RandomNumberGenerator = new Random();
		public static State State = new State();
		public Manager Manager;

		public Player LocalPlayer= new Player("steve",1,1);
		public Player WhoseTurn = new Player("steve",1,1);

		public Game (Settings s)
		{
			Instance =this;
			Settings = s;
			Manager  = new Manager();
		

			//load data from the disk.
			Manager.EnumerateSaves ();
			Manager.EnumerateWorlds ();


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

		public void Update(double ticks){

		}



	}
	
	

}

