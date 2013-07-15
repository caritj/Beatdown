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
	
	
	
	public class Unit:Renderable
	{
		int team = 0;
		public int Team{get{return team;}}
		public Unit ()
		{
			color = System.Drawing.Color.Aquamarine;
			y =1;
			glId =1;
		}
		public Unit (int X, int Y, int Z, int Team)
		{
			color = System.Drawing.Color.Bisque;
			x =X;
			y =Y;
			z =Z;
			team = Team;
			glId =2;
		}
	}
}

