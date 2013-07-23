using System;

using OpenTK.Graphics.OpenGL;
using BeatDown.Game;
using BeatDown.Renderer.InterfaceElements;

namespace BeatDown.Renderer
{
	public class GUI
	{
		MainMenu Menu;
		Lobby Lobby;
		Victory Victory;
		InGame Game;
		Loading Loading;

		public State.States lastState = BeatDown.Game.Game.State.Current;


		public GUI (Settings s)
		{
			Menu = new MainMenu ();
			Lobby = new BeatDown.Renderer.InterfaceElements.Lobby();
			Victory = new BeatDown.Renderer.InterfaceElements.Victory();
			Game = new InGame();
			Loading = new BeatDown.Renderer.InterfaceElements.Loading();

		}
		public bool WasClicked (int MouseX, int MouseY)
		{
			return false;
		}

		public void Layout(){
		
		}
		public void Render ( )
		{

			GL.PushMatrix ();
				GL.Disable(EnableCap.CullFace);
				GL.LoadIdentity();
				GL.MatrixMode(MatrixMode.Projection);
				//GL.Ortho( 0, canvas.Width, 0, canvas.Height, -1, 1);

				GL.LoadIdentity();
			//	GL.Scale (2f/canvas.Width, -2f/canvas.Height, 1f);
				

			//	GL.Translate(canvas.Width/-2f,canvas.Height/-2f, 0);
				

				//GL.Scale(-1f,1f,1f);
			//	GL.Translate(canvas.Width,0f,0f);

				GL.Enable(EnableCap.CullFace);
			GL.PopMatrix();


		}
		public void CheckForStateChange (State.States state)
		{
			if (state != this.lastState) {
				Console.WriteLine("GUI STATECHANGED");
				OnStateChange(state);

			}
		}
		public void OnStateChange(State.States state){


		/*	Menu.Hide();
			Lobby.Hide ();
			Victory.Hide ();
			Game.Hide ();
			Loading.Hide();

			switch(state){
				case State.States.MENU:
					Menu.Show();
					break;
			
				case State.States.INGAME:
					Game.Show();
					break;
		
				case State.States.DEFEAT:
				case State.States.VICTORY:
					Victory.Show();
					break;
			
				case State.States.LOBBY:
					Lobby.Show ();
					break;
		
				case State.States.LOADING:
				default:
					Loading.Show();
				break;
			}
			lastState = state;*/
		}
	}
}

