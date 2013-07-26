using System;

namespace BeatDown.Game
{

	public class Coords{
		public int X;
		public int Y;
		public int Z;

		public Coords(int x,  int y, int z){
			X=x;
			Y=y;
			Z=z;
		}
		public double Direction(Coords Before){
			//ignores y;

			return Math.Atan2( this.Z - Before.Z,this.X- Before.X); 
		}
	}
}
