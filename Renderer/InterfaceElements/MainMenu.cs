using System;


namespace BeatDown.Renderer.InterfaceElements
{
	public class MainMenu:InterfaceElement{



		public MainMenu ()
		{
			Base newGame, lobby;

			newGame = new Base(this);
			newGame.Width = 128;
			newGame.Height = 32;
			newGame.Text = "new game";

			lobby = new Base (this);
			lobby.Width = 128;
			lobby.Height = 32;
			lobby.Text = "OnlineLobby";




			newGame.OnMouseUp += delegate(OpenTK.Input.MouseButtonEventArgs args) {
				//TODO CREATE NEW GAME

				Game.Game.State.ChangeState(BeatDown.Game.State.States.INGAME);
			};
			this.children.Add(newGame);

		} 	


	}
}