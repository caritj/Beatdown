using System;
using Gwen.Control;

namespace BeatDown.Renderer.InterfaceElements
{
	public class Victory:InterfaceElement
	{


		public Victory ()
		{
			this.Width = 640;
			this.Height =480;
			this.LoadTexture("victory.png");

			Base Lable= new Base(this);
			Lable.Width = 320;
			Lable.Height =64;
			Lable.Text = "VICTORY";



			this.children.Add(Lable);

			this.OnMouseUp += delegate(OpenTK.Input.MouseButtonEventArgs args) {
				Game.Game.State.ChangeState(BeatDown.Game.State.States.MENU);
			};


		}

	}
}
