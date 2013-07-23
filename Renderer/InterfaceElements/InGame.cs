using System;
using Gwen.Control;
using BeatDown.Game;

namespace BeatDown.Renderer.InterfaceElements
{
	public class InGame:DockBase
	{

		Button menu;
		Button endTurn;
		Label remainingActions;
		Label selectedName;

		public InGame (Base parent):base(parent)
		{
			this.Dock = Gwen.Pos.Fill;
			this.SetSize(parent.Width, parent.Height);
			this.Clicked+= OnEndTurnClicked;
			menu = new Button(this);
			menu.Alignment = Gwen.Pos.Right;
			menu.SetPosition(Width - 128,0);
			menu.AutoSizeToContents= true;
			menu.Font = GUI.Font;
			menu.TextColor = System.Drawing.Color.White;
			menu.Text = "MENU";
			menu.Pressed += OnMenuClicked;


			endTurn = new Button(this);
			endTurn.Alignment = Gwen.Pos.Center;
			endTurn.SetPosition(Width/2,0);
			endTurn.AutoSizeToContents= true;
			endTurn.Font = GUI.Font;
			endTurn.TextColor = System.Drawing.Color.White;
			endTurn.Text = "END TURN";
			endTurn.Released += OnEndTurnClicked;


			remainingActions = new Label(this);
			remainingActions.Alignment = Gwen.Pos.Left;
			remainingActions.SetPosition(0,24);
			remainingActions.AutoSizeToContents= true;
			remainingActions.Font = GUI.Font;
			remainingActions.TextColor = System.Drawing.Color.White;
			remainingActions.Text = "";

			selectedName = new Label(this);
			selectedName.Alignment = Gwen.Pos.Left;
			selectedName.SetPosition(0,0);
			selectedName.AutoSizeToContents= true;
			selectedName.Font = GUI.Font;
			selectedName.TextColor = System.Drawing.Color.White;
			selectedName.Text = "";
		}
		protected override void Layout (Gwen.Skin.Base skin)
		{
			base.Layout (skin);
		}

		protected override void Render (Gwen.Skin.Base skin)
		{
			if(Game.Selection.SelectedId == Game.Selection.NONE){
				remainingActions.Text = "";
				selectedName.Text ="";
			}
			else{
				Unit selected =Game.Game.Instance.Manager.Units[Game.Selection.SelectedId];
				remainingActions.Text = "Actions:" +selected.ActionPoints;
				selectedName.Text =selected.Name;
			}

			base.Render (skin);
		}

		protected void OnMenuClicked(Base control, EventArgs args){
			//TODO
			Console.WriteLine("/MnueClicled");
			Game.Game.State.ChangeState(State.States.MENU);
		}
		protected void OnEndTurnClicked(Base control, EventArgs args){
			//TODO
			Console.WriteLine("/released");
		}
	}
}