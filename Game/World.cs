using System;

namespace BeatDown.Game
{
	public class World:Renderable
	{
		const int WORLD_SIZE  =10;
		public int[,] Heightmap = new int[WORLD_SIZE,WORLD_SIZE];

		protected System.Drawing.Color sideColor = System.Drawing.Color.Tan;
		public System.Drawing.Color SideColor{ get { return sideColor; } }

		public World ()
		{
			this.sizeX = WORLD_SIZE;
			this.sizeY = WORLD_SIZE/5;//UP

			this.sizeZ = WORLD_SIZE;
			Random r = new Random ();
			this.color = System.Drawing.Color.Green; 

			for (int i = 0; i < this.Heightmap.GetLength(0); i++) {
				for (int j = 0; j < this.Heightmap.GetLength(1); j++) {
					Heightmap [i, j] = r.Next (0, sizeY);
				}
			}
		}
		public int HeightAt(int x, int y){
			return Heightmap[y,x];
		}

	}
}

