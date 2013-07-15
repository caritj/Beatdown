using System;
using Gwen.Control;

namespace BeatDown.Renderer.InterfaceElements
{
	public class Victory:DockBase
	{
		bool didWin = true;
		public Victory (Base parent):base(parent)
		{
		}
		protected override void Render (Gwen.Skin.Base skin)
		{


			base.Render (skin);
		}
	}
}

