using System;

using BeatDown.Game;

namespace BeatDown.Renderer.InterfaceElements
{
	public class InGame:InterfaceElement
	{



		public InGame ()
		{
			this.Width = 120;
			this.Height =40;
			this.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
			this.ShowBackgroundColor =false;
			this.Text = "End Turn";
			this.TextBrush = System.Drawing.Brushes.Firebrick;
			//this.LoadTexture("button.png");
		}	

		public override void MouseUp (OpenTK.Input.MouseButtonEventArgs args)
		{
			//does nothing
			Console.WriteLine ("BeatDown");
			this.ShouldDraw = !this.ShouldDraw;
		}

		public override void MouseDown (OpenTK.Input.MouseButtonEventArgs args)
		{
			//does nothing
		}
	



	
	}
}