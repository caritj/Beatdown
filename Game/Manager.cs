using System;
using System.Collections.Generic;
using System.Drawing;
using ZMQ;

namespace BeatDown.Game
{

	public interface IManager:IDisposable
	{

		World World
		{
			get;
		}

		IDictionary<Int32, Unit> Units
		{
			get;
		}

		int AddUnit(Unit unit);
		Unit GetUnitByName(string name);
		System.Drawing.Color GetTeamColor (int team);
	}

	public class Manager:IManager {

		protected Int32 currentGLID = 0;
		protected IDictionary<Int32, Unit> units = new Dictionary<Int32, Unit>();
		protected IDictionary<string, Int32> unitNames = new Dictionary<string, Int32>();
		protected World world;

		public Manager()
		{
			this.world = new World ();

			this.AddUnit(new Unit ("Paul", world.GetCoords (1, 1), 1));
			this.AddUnit (new Unit ("Lee", world.GetCoords (5, 5), 2));	

		}

		public IDictionary<Int32, Unit> Units
		{
			get{return this.Units;}
		}

		public World World {
			get { return this.world;}
		}
		public int AddUnit(Unit unit)
		{
			Int32 glID = getGLID();

			this.units.Add (glID, unit);
			this.unitNames.Add (unit.Name, glID);

			return glID;
		}
		public Unit GetUnitByName(string name)
		{
			return this.units[this.unitNames[name]];
		}

		public void Dispose()
		{
		}

		protected Int32 getGLID()
		{
			Int32 thisGLID = this.currentGLID;
			this.currentGLID++;
			return thisGLID;
		}

		public Color GetTeamColor(int team)
		{
			switch(team)
			{
			case 0:
				return Color.DarkRed;
			case 1:
				return Color.Azure;
			case 2:
				return Color.Firebrick;
			case 3:
				return Color.Yellow;
			default:
				return Color.Black;

			}

		}

	}


}



