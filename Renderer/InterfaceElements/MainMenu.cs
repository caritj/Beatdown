using System;


namespace BeatDown.Renderer.InterfaceElements
{
	public class MainMenu:InterfaceElement{



		public MainMenu ()
		{
			Base newGame;

			newGame = new Base(this);
			newGame.Width = 96;
			newGame.Height = 64;
			newGame.Text = "new game";


			newGame.OnMouseUp += delegate(OpenTK.Input.MouseButtonEventArgs args) {
				Game.Game.State.ChangeState(BeatDown.Game.State.States.INGAME);
			};
			this.children.Add(newGame);

		} 	


	}
}