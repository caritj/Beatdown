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
			this.Dock = Gwen.Pos.Fill;
			this.SetSize(parent.Width, 64);

			menu = new Button(this);
			menu.Alignment = Gwen.Pos.Right;
			menu.SetPosition(Width - 128,0);
			menu.AutoSizeToContents= true;
			menu.Font = SharedResources.GUIFont;
			menu.TextColor = System.Drawing.Color.White;
			menu.Text = "MENU";


			endTurn = new Button(this);
			endTurn.Alignment = Gwen.Pos.Center;
			endTurn.SetPosition(Width/2,0);
			endTurn.AutoSizeToContents= true;
			endTurn.Font = SharedResources.GUIFont;
			endTurn.TextColor = System.Drawing.Color.White;
			endTurn.Text = "END TURN";


			remainingActions = new Label(this);
			remainingActions.Alignment = Gwen.Pos.Left;
			remainingActions.SetPosition(0,0);
			remainingActions.AutoSizeToContents= true;
			remainingActions.Font = SharedResources.GUIFont;
			remainingActions.TextColor = System.Drawing.Color.White;
			remainingActions.Text = "Actions:";
		}
		protected override void Layout (Gwen.Skin.Base skin)
		{
			base.Layout (skin);
		}

		protected override void Render (Gwen.Skin.Base skin)
		{
			remainingActions.Text = String.Format("Actions:{0}@{1}",Game.Selection.HoveredId, Game.Selection.Maploc);

			base.Render (skin);
		}
	}
}

