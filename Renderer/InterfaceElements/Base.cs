using System;

namespace BeatDown.Renderer.InterfaceElements
{
	public class Base:InterfaceElement
	{
		public Base (InterfaceElement parent)
		{
			this.parent = parent;
		}
		public Base (){

		}
		public override void Layout (int width, int height)
		{
			//does nothing;
		}
			

	}
}

