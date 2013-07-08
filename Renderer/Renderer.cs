using System;
using OpenTK;
using Gwen;
using Game;

namespace Renderer
{
	public class Render:GameWindow
	{
		protected Game theGame = null;
		protected Game.Settings settings = null;
		protected GUI gui = null;

		private Gwen.Input.OpenTK input;
		private Gwen.Renderer.OpenTK renderer;
		private Gwen.Skin.Base skin;
		private Gwen.Control.Canvas canvas;

		private Vector3 UP = Vector3.UnitY;

		public Render (ref Game g, ref Settings s):base(Settings.GraphicsX, Settings.GraphicsY, OpenTK.Graphics.GraphicsMode.Default, "Raven"){
			theGame = g;
			settings = s;
			gui = new GUI (s);
		}

		protected override void OnLoad (EventArgs e)
		{
			input = new Gwen.Input.OpenTK (this);
			renderer = new Gwen.Renderer.OpenTK (
			skin = new Gwen.Skin.Base (renderer, Settings.GUI_DATA_DIR+"DefaultSkin.png");
			canvas = new Gwen.Control.Canvas (skin);

		}
		protected override void OnResize (EventArgs e)
		{

			gui.Layout;
		}
		protected override void OnUpdateFrame (FrameEventArgs e)
		{

		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
		
		}

		protected override void OnDisposed (EventArgs e)
		{

		}
	}
}

