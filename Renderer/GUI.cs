using System;
using Gwen;
using BeatDown.Renderer.InterfaceElements;

namespace BeatDown.Renderer
{
	public class GUI
	{
		MainMenu Menu;
		Lobby Lobby;
		Victory Victory;
		InGame Game;


		public GUI (Game.Settings s,Gwen.Control.Canvas c)
		{
			Menu = new MainMenu (c);
			Lobby = new BeatDown.Renderer.InterfaceElements.Lobby(c);
			Victory = new BeatDown.Renderer.InterfaceElements.Victory(c);
			Game = new InGame(c);


		}

		public void Layout(){
		
		}
		public void OnStateChange(Beatdown.Game.State.States state){
			Menu.Hide();
			Lobby.Hide ();
			Victory.Hide ();
			Game.Hide ();

			switch(state){
				case Beatdown.Game.State.States.MENU:
					Menu.Show();
					break;
			
				case Beatdown.Game.State.States.INGAME:
					Game.Show();
					break;
		
				case Beatdown.Game.State.States.DEFEAT:
				case Beatdown.Game.State.States.VICTORY:
					Victory.Show();
					break;
			
				case Beatdown.Game.State.States.LOBBY:
					Lobby.Show ();
					break;
		
				case Beatdown.Game.State.States.LOADING:
				default:
				break;
			}
		}
	}
}

