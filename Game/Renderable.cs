using System;
using System.Drawing;

namespace BeatDown.Game
{
	public abstract class Renderable
	{
	
		public  int X{get{return position.X; }}
		public  int Y{get{return position.Y; }}
		public  int Z{get{return position.Z; }}

		

		protected Coords position = new Coords(0,0,0);
		public Coords Position { get { return this.position; } }

		protected int sizeX=1;
		protected int sizeY=1;
		protected int sizeZ=1;

		protected double rotation = 0;

		protected Color color = Color.HotPink;

		protected bool hidden = false;

		
		public int SizeX{get{ return sizeX; }}
		public int SizeY{get{ return sizeY; }}
		public int SizeZ{get{ return sizeZ; }}

		public float Rotation{ get { return (float)rotation; } }

		public System.Drawing.Color Color{get{return color; }}

		public bool Visible{ get { return ! hidden; } }

		public void MoveTo(int X, int Y, int Z, double Rotation){
			position = new Coords(X,Y,Z);
			rotation = Rotation;
		}

		public void Show(){
			hidden = false;
		}
		public void Hide(){
			hidden = true;
		}

		//this is the id asssoicated with the object by open gl picking.
		public int  glId =0;



	}


}

