using System;
using Gwen.Control;
using BeatDown.Game;

namespace BeatDown.Renderer.InterfaceElements
{

	public class Loading:DockBase
	{
		Label loading;

		public Loading (Base parent):base(parent)
		{
			loading = new Label(this);
			loading.Alignment = Gwen.Pos.Center;
			loading.SetPosition(Width/2, Height/2);
			loading.AutoSizeToContents= true;
		//	loading.Font = GUI.Font;
			loading.TextColor = System.Drawing.Color.White;
			loading.Text = "LOADING";
		}

	}
}
