using System;
using System.Collections.Generic;

namespace BeatDown.Game
{
	public class Player
	{
		public int Team;
		public string Name;
		public int Id;

		List<Unit> Stable;
		List<Unit> Active;

		//TODO IP


		public Player (string name, int team, int id)
		{
			Name= name;
			Team = team;
			Id =id;
		}
	}
}

