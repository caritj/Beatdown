using System;

namespace BeatDown.Game
{
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
			color = System.Drawing.Color.Crimson;
			x =X;
			y =Y;
			z =Z;
			team = Team;
			glId =2;
		}
	}
}

