using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Gwen;
using Game;

namespace Renderer
{
	public class Render:GameWindow
	{
		protected Game.Game theGame = null;
		protected Game.Settings settings = null;
		protected GUI gui = null;

		private Gwen.Input.OpenTK input;
		private Gwen.Renderer.OpenTK renderer;
		private Gwen.Skin.Base skin;
		private Gwen.Control.Canvas canvas;

		public  static readonly Vector3 UP = Vector3.UnitY;


		public static Render Instance = null;

		public Render (ref Game.Game g, ref Settings s):base(s.GraphicsX, s.GraphicsY, OpenTK.Graphics.GraphicsMode.Default, "Raven"){
			theGame = g;
			settings = s;


			//in case some one else wants to acess this bit.
			Instance = this;

			//TODO setup camera

			//TODO bind events.
		}

		protected override void OnLoad (EventArgs e)
		{

			//setup GWEN
			renderer = new Gwen.Renderer.OpenTK ();
			skin = new Gwen.Skin.TexturedBase (renderer, Settings.GUI_DATA_DIR+"DefaultSkin.png");
			canvas = new Gwen.Control.Canvas (skin);
			canvas.SetSize(Width,Height);
			canvas.ShouldDrawBackground =false;
			canvas.BackgroundColor = System.Drawing.Color.Aqua;
			input = new Gwen.Input.OpenTK (this);
			input.Initialize(canvas);

			//setup gui systems
			gui = new GUI (settings, canvas);


			//Decide what open gl capacities we want running.

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcColor);


			base.OnLoad(e);

			//TODO Tell the game Load finished

		}
		protected override void OnResize (EventArgs e)
		{

			gui.Layout();
			canvas.SetSize(Width,Height);
			base.OnResize(e);
		}
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			//draw picking data to the buffer.

			//draw real data to the buffer.

			//draw gui to the buffer.

			//draw to screen
			SwapBuffers();
		
		}

		protected override void OnDisposed (EventArgs e)
		{
			renderer.Dispose();
			skin.Dispose();
			canvas.Dispose();
			base.OnDisposed(e);

		}
	}
}

