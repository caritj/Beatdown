using System;
using System.Drawing;

namespace Game
{
	public abstract class Renderable
	{

		protected int x;
		protected int y;
		protected int z;

		protected int sizeX;
		protected int sizeY;
		protected int sizeZ;

		protected double rotation = 0;

		protected Color color = Color.HotPink;

		protected bool hidden = false;

		public int X{get{return x; }}
		public int Y{get{return y; }}
		public int Z{get{return z; }}

		public int SizeX{get{ return sizeX; }}
		public int SizeY{get{ return sizeY; }}
		public int SizeZ{get{ return sizeZ; }}

		public float Rotation{ get { return (float)rotation; } }

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

		//this si teh id asssoicated with the  object by open gl.
		public int  glId =0;



	}
}

