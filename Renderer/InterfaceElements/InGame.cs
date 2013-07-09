using System;
using Gwen.Control;

namespace BeatDown.Renderer.InterfaceElements
{
	public class InGame:DockBase
	{

		Button menu;
		Button endTurn;
		Label remainingActions;

		public InGame (Base parent):base(parent)
		{
			this.Dock = Gwen.Pos.Bottom;
			this.SetSize( Width, 64);

			menu = new Button(this);
			menu.Alignment = Gwen.Pos.Right;
			menu.AutoSizeToContents= true;
			menu.Font = SharedResources.GUIFont;
			menu.TextColor = System.Drawing.Color.Black;
			menu.Text = "MENU";


			endTurn = new Button(this);
			endTurn.Alignment = Gwen.Pos.Right;
			endTurn.AutoSizeToContents= true;
			endTurn.Font = SharedResources.GUIFont;
			endTurn.TextColor = System.Drawing.Color.Black;
			endTurn.Text = "END TURN";


			remainingActions = new Label(this);
			remainingActions.Alignment = Gwen.Pos.Left;
			remainingActions.AutoSizeToContents= true;
			remainingActions.Font = SharedResources.GUIFont;
			remainingActions.TextColor = System.Drawing.Color.Black;
			remainingActions.Text = "Actions:";
		}
		protected override void Layout (Gwen.Skin.Base skin)
		{
			base.Layout (skin);
		}

		protected override void Render (Gwen.Skin.Base skin)
		{
			remainingActions.Text = String.Format("Actions:{0}","xxx");

			base.Render (skin);
		}
	}
}

