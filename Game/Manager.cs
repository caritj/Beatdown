using System;
using System.Collections.Generic;

namespace BeatDown.Game
{
	public class Manager:IDisposable
	{
		int nextid =0;
		protected Dictionary<int, Unit> units = new Dictionary<int, Unit>();
		protected Dictionary<string,int> names = new Dictionary<string, int>();

		public Manager ()
		{
		}
		public void AddUnit (Unit u)
		{
			nextid++;
			units.Add(nextid,u);
			//name this dude?
		}

		public Unit GetUnitByGlId(int glid){
			if(units.ContainsKey(glid)){
				return units[glid];
			}
			return null;
		}
		public Unit GetUnitByName (String name)
		{
			int id = 0;
			if (names.ContainsKey (name)) {
				id = names[name];
				if (units.ContainsKey (id)) {
					return units [id];
				}
			}
			return null;
		}

		public void Dispose ()
		{

		}


	}
}

