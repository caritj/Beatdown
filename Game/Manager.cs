using System;
using System.Collections.Generic;

namespace BeatDown.Game
{

	public interface IManager:IDisposable
	{

		World World
		{
			get;
		}

		Dictionary<Int32, Unit> Units
		{
			get;
		}

		int AddUnit(Unit unit);
		Unit GetUnitByName(string name);

	}

	public class Manager:IManager {
		public Dictionary<Int32, Unit> Units
		{
			get{return null;}
		}

		public World World {
			get { return null;}
		}
		public int AddUnit(Unit unit)
		{
			return 0;
		}
		public Unit GetUnitByName(string name)
		{
			return null;
		}

		public void Dispose()
		{
		}

	}


}



