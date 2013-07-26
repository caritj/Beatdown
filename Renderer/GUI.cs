using System;

using OpenTK.Graphics.OpenGL;
using BeatDown.Game;
using BeatDown.Renderer.InterfaceElements;
using System.Collections.Generic;
using OpenTK.Input;

namespace BeatDown.Renderer
{
	public class GUI
	{
		protected Dictionary<Game.State.States, InterfaceElement> Interfaces = new Dictionary<Game.State.States, InterfaceElement>();
		


		public State.States lastState = BeatDown.Game.Game.State.Current;


		public GUI (Settings s)
		{

			Interfaces.Add(State.States.INGAME, new InGame());
			Interfaces.Add(State.States.MENU, new MainMenu());
		//	Interfaces.Add(State.States.LOBBY, new Lobby());
		}
		public bool WasClicked (int MouseX, int MouseY)
		{
			if (Interfaces.ContainsKey (lastState)) {

				return Interfaces [lastState].WasClicked(MouseX,MouseY);
			}
			Console.WriteLine("Invalid ui");
			return false;
		}

		public void MouseUp (MouseButtonEventArgs args)
		{
			if (Interfaces.ContainsKey (lastState)) {

				Interfaces [lastState].MouseUp(args);
			}
		}
		public void Layout(int Width, int Height){
		
		
		}
		public void Render ()
		{
			GL.PushMatrix ();
				GL.Disable (EnableCap.CullFace);
				GL.LoadIdentity ();
				GL.MatrixMode (MatrixMode.Projection);
				GL.Ortho (0, Renderer.Render.Instance.Width, 0, Renderer.Render.Instance.Height, -1, 1);

				GL.LoadIdentity ();
				GL.Scale (2f / Renderer.Render.Instance.Width, -2f / Renderer.Render.Instance.Height, 1f);
					

				GL.Translate (Renderer.Render.Instance.Width / -2f, Renderer.Render.Instance.Height / -2f, -0.5);
					
				Renderer.Render.Instance.drawAxes (0, 0, 0);
					
				if (Interfaces.ContainsKey (lastState)) {
					Interfaces [lastState].Draw ();
				}
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
			lastState = state;
		}


	}
}

