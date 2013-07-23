using System;
using Gwen;

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

		public static Gwen.Font Font = null;


		public Gwen.Input.OpenTK Input =new Gwen.Input.OpenTK(Renderer.Render.Instance);
		Gwen.Renderer.OpenTK renderer = new Gwen.Renderer.OpenTK();
		Gwen.Skin.Base skin;
		public Gwen.Control.Canvas Canvas;


		public State.States lastState = BeatDown.Game.Game.State.Current;


		public GUI (Settings s)
		{
			if (Font == null) {
				Font = new Gwen.Font(renderer, "arial", 16);
			}
			skin = new Gwen.Skin.TexturedBase(renderer, s.GuiDirectory+"DefaultSkin.png");
			Canvas = new Gwen.Control.Canvas(skin);
			Input.Initialize(Canvas);

			Menu = new MainMenu (Canvas);
			Lobby = new BeatDown.Renderer.InterfaceElements.Lobby(Canvas);
			Victory = new BeatDown.Renderer.InterfaceElements.Victory(Canvas);
			Game = new InGame(Canvas);
			Loading = new BeatDown.Renderer.InterfaceElements.Loading(Canvas);

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
				GL.Ortho( 0, Canvas.Width, 0, Canvas.Height, -1, 1);

				GL.LoadIdentity();
				GL.Scale (2f/Canvas.Width, -2f/Canvas.Height, 1f);
				

			//	GL.Translate(canvas.Width/-2f,canvas.Height/-2f, 0);
				

				//GL.Scale(-1f,1f,1f);
			//	GL.Translate(canvas.Width,0f,0f);

				GL.Enable(EnableCap.CullFace);
			GL.PopMatrix();


		}
		public void CheckForStateChange (State.States state)
		{
			if (state != this.lastState) {
				Console.WriteLine("GUI STATECHANGED to "+ state);
				OnStateChange(state);

			}
		}
		public void OnStateChange(State.States state){


			Menu.Hide();
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
			lastState = state;
		}
	}
}

