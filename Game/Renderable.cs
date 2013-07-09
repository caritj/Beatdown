using System;
using System.Drawing;

namespace BeatDown.Game
{
	public abstract class Renderable
	{

		protected int x=0;
		protected int y=0;
		protected int z=0;

		protected int sizeX=1;
		protected int sizeY=1;
		protected int sizeZ=1;

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

