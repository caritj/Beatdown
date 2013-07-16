using System;
using System.Collections.Generic;

namespace BeatDown.Game
{

	public class IManager:IDisposable
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


}

