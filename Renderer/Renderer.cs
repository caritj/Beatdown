using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Gwen;
using BeatDown.Game;

namespace BeatDown.Renderer
{
	public class Render:GameWindow
	{
		public Game.Game theGame = null;
		public  Settings settings = null;
		public GUI gui = null;

		private Gwen.Input.OpenTK input;
		private Gwen.Renderer.OpenTK renderer;
		private Gwen.Skin.Base skin;
		private Gwen.Control.Canvas canvas;

		public  static readonly Vector3 UP = Vector3.UnitY;

		protected Vector3 InGameCameraPosition= new Vector3(10,5,10);
		protected Vector3 InGameCameraTarget = Vector3.Zero;
		protected Matrix4 CameraMatrix;

		public static Render Instance = null;


		#region test data
		Game.World w = new World();
		Game.Unit u = new Unit();

		#endregion

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
			SharedResources.GUIFont = new Gwen.Font(renderer, "arial",16);


			//setup gui systems
			gui = new GUI (settings, canvas);

		

			//Decide what open gl capacities we want running.

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			//GL.Enable(EnableCap.Lighting);
			//GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.Zero);


			base.OnLoad(e);

			//TODO Tell the game Load finished

		}
		protected override void OnResize (EventArgs e)
		{
			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);

			gui.Layout();
			canvas.SetSize(Width,Height);
			base.OnResize(e);
		}
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			if (Keyboard [OpenTK.Input.Key.Left]) {
				InGameCameraPosition.X += (float)Math.Sin (e.Time) * 5f;
				InGameCameraPosition.Z += (float)Math.Cos (e.Time) * 5f;
			}
			if (Keyboard [OpenTK.Input.Key.Right]) {
				InGameCameraPosition.X -= (float)Math.Sin (e.Time) * 5f;
				InGameCameraPosition.Z -= (float)Math.Cos (e.Time) * 5f;
			}

			if (Keyboard [OpenTK.Input.Key.Escape]) {
				this.Exit();
			}
			base.OnUpdateFrame(e);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			this.Title = string.Format(" BEATDOWN : FPS:{0:F} mouse:{1},{2}", 1.0 / e.Time, Mouse.X, Mouse.Y);

			//clear the buffer;
			GL.ClearColor(0f,0f,0f,0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			//setup the camera
			CameraMatrix = Matrix4.LookAt(InGameCameraPosition, InGameCameraTarget, UP);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref CameraMatrix);

			//draw picking data to the buffer.


			//draw real data to the buffer.
			GameObjects.WorldRenderer.RenderViewable(w);
			GameObjects.BaseRender.RenderViewable(u);

			//draw gui to the buffer.
			this.gui.OnStateChange(Beatdown.Game.State.States.INGAME);


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

