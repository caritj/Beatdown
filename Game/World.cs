using System;
using System.Collections.Generic;


namespace BeatDown.Game
{
	public class World:Renderable
	{
		public const int WORLD_SIZE  =10;
		public WorldNode[,] Heightmap = new WorldNode[WORLD_SIZE,WORLD_SIZE];

		protected System.Drawing.Color sideColor = System.Drawing.Color.Tan;
		public System.Drawing.Color SideColor{ get { return sideColor; } }

		protected AStar.AStar Astar;

		public World ()
		{
			this.sizeX = WORLD_SIZE;
			this.sizeY = WORLD_SIZE/3;//UP

			this.sizeZ = WORLD_SIZE;
			Random r = new Random ();
			this.color = System.Drawing.Color.Green; 

			for (int i = 0; i < this.Heightmap.GetLength(0); i++) {
				for (int j = 0; j < this.Heightmap.GetLength(1); j++) {
					Heightmap [i, j] = new WorldNode(this, j,r.Next (0, sizeY),i);
				}
			}
		}
		public int HeightAt(int x, int z){
			return Heightmap[z,x].Y;
		}
		public List<coords>GetPath(int StartX, int StartZ, int EndX, int EndZ){
			//TODO a*
			List<coords> output = new List<coords>();

			output.Add (new coords(StartX, this.HeightAt(StartX,StartZ), StartZ));

			output.Add (new coords(EndX,this.HeightAt(StartX,EndZ), EndZ));

			return output;
		}



	}
}

