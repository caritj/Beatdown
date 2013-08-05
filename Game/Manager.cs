using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;


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

	/// <summary>
	/// Manager.
	/// This class handels operations like IO and caching. Designed to keep the game logic simple and encapsulated. This should be the only place that the game touches the disk in teh game assembly
	/// </summary>
	public class Manager:IManager {

		protected Int32 currentGLID = 1;	//cannot be 0 based or we cant tell the difference from nothing.
		protected IDictionary<Int32, Unit> units = new Dictionary<Int32, Unit>();
		protected IDictionary<string, Int32> unitNames = new Dictionary<string, Int32>();
		protected IDictionary<string, World> worlds = new Dictionary<String, World>();
		protected IDictionary<string, Save> saves = new Dictionary<string, Save> ();


		protected World world;

		protected List<Int32> removalQueue = new List<Int32>();

		public Manager()
		{
			this.world = new World ();

			this.AddUnit(new Unit ("Paul", world.GetCoords (1, 1), 1));
			this.AddUnit (new Unit ("Lee", world.GetCoords (5, 5), 2));	

			//check to ensure teh directories we need exist
 			if (!Directory.Exists (Game.Instance.Settings.DataDirectory)) {
				Directory.CreateDirectory (Game.Instance.Settings.DataDirectory);
				if (!Directory.Exists (Game.Instance.Settings.SaveDirectory)) {
					Directory.CreateDirectory (Game.Instance.Settings.SaveDirectory);
				}
			}

		}

		public IDictionary<Int32, Unit> Units
		{
			get{return this.units;}
		}

		public World World {
			get { return this.world;}
		}
		public int AddUnit(Unit unit)
		{
			Int32 glID = getGLID();
			unit.glId = glID;
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
		public void AddToRemovalQueue (int i)
		{
			if (!removalQueue.Contains (i)) {
				removalQueue.Add(i);
			}
		}

		public void Update (double time)
		{
			//Remvoe teh removal queue
			foreach (int i in removalQueue) {
				if(units.ContainsKey(i)){
					units.Remove(i);
				}
			}

			removalQueue.Clear();
		}

		/// <summary>
		/// Enumerates the maps available tot eh player
		/// </summary>
		public void EnumerateWorlds(){
			String[] files = Directory.GetFiles (Game.Instance.Settings.WorldDirectory, "*.world");
			foreach (string file in files) {
				if (!this.worlds.ContainsKey (file)) {
					this.worlds [file] = null;
				}
			}

		}

		/// <summary>
		/// Enumerates the saves on the local disk.
		/// </summary>
		public void EnumerateSaves(){
			String[] files = Directory.GetFiles (Game.Instance.Settings.SaveDirectory, "*.save");
			foreach (string file in files) {
				if (!this.saves.ContainsKey (file)) {
					this.saves [file] = null;
				}
			}
		}

		/// <summary>
		/// Loads the save into the manager if the 
		/// </summary>
		/// <returns>The save.</returns>
		/// <param name="key">Key.</param>
		public Save LoadSave(string key){
			if (this.saves.ContainsKey (key) && this.saves [key] != null) {
				return this.saves [key];
			} else {
				if(File.Exists(key)){
					this.saves[key] = new Save(File.ReadAllText(key));
				}
				else{
					this.saves[key] =  new Save();
				}
				return this.saves [key];

			}
		}

	}


}



