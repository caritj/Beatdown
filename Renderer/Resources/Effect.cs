using System;
using BeatDown.Game;
using OpenTK;

namespace BeatDown.Renderer.Resources
{
	public class Effect:Renderable
	{
		public enum Alignment{CAMERA,UP,FIXED};

		public Alignment Aligned = Alignment.CAMERA;
		public Vector3 RotationAlignment;
		double time =0;
		int maxTime =1;
		protected Texture texture;
		public Texture Texture{get{return texture;}}
	
		public Effect (Coords start,  Texture texture, int howLong)
		{
			position = start;
			maxTime=howLong;
		}
		public void Update (double deltaT)
		{
			time += deltaT;
			if (time > maxTime) {

			}
		
		}
	}
}

