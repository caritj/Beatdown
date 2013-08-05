using System;


namespace BeatDown.Renderer.InterfaceElements
{
	public class MainMenu:InterfaceElement{



		public MainMenu ()
		{
			Base newGame, lobby,dimmer;

			newGame = new Base(this);
			newGame.Width = 128;
			newGame.Height = 32;
			newGame.Text = "new game";

			lobby = new Base (this);
			lobby.Width = 128;
			lobby.Height = 32;
			lobby.Text = "OnlineLobby";

			dimmer = new Base (this);
			dimmer.Width = Renderer.Render.Instance.Width;
			dimmer.Height = Renderer.Render.Instance.Height;
			dimmer.BackgroundColor = System.Drawing.Color.FromArgb (128, 0, 0, 0);
			dimmer.ShowBackgroundColor = true;



			newGame.OnMouseUp += delegate(OpenTK.Input.MouseButtonEventArgs args) {
				//TODO CREATE NEW GAME

				Game.Game.State.ChangeState(BeatDown.Game.State.States.INGAME);
			};
			this.children.Add(newGame);

		} 	

		public override void Layout (int width, int height)
		{
			//?
		}


	}
}