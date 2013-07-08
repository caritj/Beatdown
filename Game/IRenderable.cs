using System;

namespace Game
{
	public abstract class Renderable
	{
		int x;
		int y;
		int z;

		int sizeX;
		int sizeY;
		int sizeZ;

		public int X{get{return x; }}
		public int Y{get{return y; }}
		public int Z{get{return z; }}

		public int SizeX{get{ return sizeX; }}
		public int SizeY{get{ return sizeY; }}
		public int SizeZ{get{ return sizeZ; }}

		public System.Drawing.Color GetColor();


	}
}

