using System;
using System.Drawing;

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

		double rotation = 0;

		Color color = Color.HotPink;

		bool hidden = false;

		public int X{get{return x; }}
		public int Y{get{return y; }}
		public int Z{get{return z; }}

		public int SizeX{get{ return sizeX; }}
		public int SizeY{get{ return sizeY; }}
		public int SizeZ{get{ return sizeZ; }}

		public double Rotation{ get { return rotation; } }

		public System.Drawing.Color Color{get{return color; }}

		public bool Visible{ get { return ! hidden; } }

		public void MoveTo(int X, int Y, int Z, double Rotation){
			x = X;
			y = Y;
			z = Z;
			rotation = Rotation;
		}

		public void Show(){
			hidden = false;
		}
		public void Hide(){
			hidden = true;
		}




	}
}

