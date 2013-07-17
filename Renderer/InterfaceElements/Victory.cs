using System;
using Gwen.Control;

namespace BeatDown.Renderer.InterfaceElements
{
	public class Victory:DockBase
	{
		bool didWin = true;
		Label message;
		Button okay;

		public Victory (Base parent):base(parent)
		{
			message = new Label (this);
			message.Alignment = Gwen.Pos.Center;

			message.AutoSizeToContents = true;
			message.Font = SharedResources.GUIFont;
			message.TextColor = System.Drawing.Color.White;
			if (didWin) {
				message.Text = "VICTORY";
			} else {
				message.Text = "DEFEAT!";
			}


			okay =new Button(this);
			okay.Text ="OKAY!";

			this.Hide();
		}
		protected override void Render (Gwen.Skin.Base skin)
		{


			base.Render (skin);
		}
	}
}

