using System;

namespace Game
{
	public class World:Renderable
	{
		const int WORLD_SIZE  =10;
		public int[,] Heightmap = new int[WORLD_SIZE,WORLD_SIZE];

		public World ()
		{
			this.sizeX = WORLD_SIZE;
			this.sizeY = WORLD_SIZE;
			this.sizeZ = WORLD_SIZE;
		}

	}
}

