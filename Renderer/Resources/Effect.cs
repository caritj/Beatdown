using System;
using BeatDown.Game;

namespace BeatDown.Renderer.Resources
{
	public class Effect
	{
	
		Coords location;
		double time =0;
		int maxTime =1;
	
		public Effect (Coords start,  Texture texture, int howLong)
		{
			location = start;
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

