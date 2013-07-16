using System;

namespace BeatDown.Game
{
	public static class Selection
	{
		public const int NONE=0;
		public static int SelectedId = NONE;
		public static int HoveredId =NONE;
		public static int Maploc = NONE;


		public static int MapX{get{return (Maploc-1)%World.WORLD_SIZE;}}
		public static int MapZ{ get { return  (int)Math.Floor ((double)(Maploc - 1) / World.WORLD_SIZE); } }
		public static Renderable.coords MapCoords {	get{return new Renderable.coords(MapX,1,MapZ);}}
	}
}

