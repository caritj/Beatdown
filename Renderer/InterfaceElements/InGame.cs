using System;

using BeatDown.Game;

namespace BeatDown.Renderer.InterfaceElements
{
	public class InGame:InterfaceElement
	{

		SelectedUnitInfo unitInfo;

		public InGame ()
		{
			Base endTurn = new Base(this);
			endTurn.Width = 96;
			endTurn.Height =64;
			endTurn.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
			endTurn.ShowBackgroundColor =false;
			endTurn.Text = "End Turn";
			//this.LoadTexture("button.png");
			endTurn.OnMouseUp += delegate(OpenTK.Input.MouseButtonEventArgs args) {
				Game.Game.Instance.EndTurn();
			};

			unitInfo = new SelectedUnitInfo(this);



			Base menuButton = new Base(this);
			menuButton.Width = 64;
			menuButton.Height = 64;
			menuButton.BackgroundColor = System.Drawing.Color.Black;
			menuButton.ShowBackgroundColor =false;
			menuButton.Y =129;
			menuButton.Text = "Menu";
			menuButton.OnMouseUp+= delegate(OpenTK.Input.MouseButtonEventArgs args) {
				Game.Game.State.ChangeState(State.States.MENU);
				Console.WriteLine("Menuclicked");
			};


			this.children.Add(endTurn);
			this.children.Add(unitInfo);
			this.children.Add(menuButton);

		}

		public void updateSelectedUnit(Unit selected){
			unitInfo.Draw();

		}
		public override void Layout (int width, int height)
		{
			foreach (InterfaceElement i in children) {
				i.Layout (width, height);

			}
		}



		
	
	}
}