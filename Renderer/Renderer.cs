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
		private float rot = 0;
		private float rotX = 0;

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
			canvas.BackgroundColor = System.Drawing.Color.OrangeRed;
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
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			//TODO RM DEBUG DATA
			this.gui.OnStateChange(Beatdown.Game.State.States.INGAME);

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
				rot+= 0.8f;
			}
			if (Keyboard [OpenTK.Input.Key.Right]) {
				rot-= 0.8f;
			}
			if (Keyboard [OpenTK.Input.Key.Up]) {
				rotX+= 0.8f;
			}
			if (Keyboard [OpenTK.Input.Key.Down]) {
				rotX-= 0.8f;
			}

			if (Keyboard [OpenTK.Input.Key.Escape]) {
				this.Exit();
			}
			base.OnUpdateFrame(e);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			this.Title = string.Format (" BEATDOWN : FPS:{0:F} mouse:{1},{2}", 1.0 / e.Time, Mouse.X, Mouse.Y);

			//clear the buffer;
			ClearBuffer ();

			GL.PushMatrix ();
				//setup the camera
				CameraMatrix = Matrix4.LookAt (InGameCameraPosition, InGameCameraTarget, UP);
				GL.MatrixMode (MatrixMode.Modelview);
				GL.LoadMatrix (ref CameraMatrix);


				//rotate the world. (instead of moving the camera);

				GL.Rotate (rot, UP);
				GL.Rotate (rotX, Vector3.UnitZ);

				//draw picking data to the buffer.
				GameObjects.BaseRender.RenderPickable (u);

				//get the selected object;
				int num = PickObject ();

				//clear the buffer;
				ClearBuffer();


				//draw real data to the buffer.
				GameObjects.WorldRenderer.RenderViewable(w);
				GameObjects.BaseRender.RenderViewable(u);

				//show the axes in teh rotate context
				this.drawAxes(0f,2f,0f);

			GL.PopMatrix();

			//draw gui to the buffer.
			gui.Render(canvas);
		

			//show teh oprigin
			this.drawAxes(0,0,0);
		

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
		private void drawAxes(float x, float y, float z){
			GL.PushMatrix();


			GL.Translate(x,y,z);
			GL.Begin(BeginMode.Lines);
			GL.Color3(1f,0f,0f);
			GL.Vertex3(0f,0f,0f);
			GL.Vertex3(1f,0f,0f);
			
			GL.Color3(0f,1f,0f);;
			GL.Vertex3(0f,0f,0f);
			GL.Vertex3(0f,1f,0f);

			GL.Color3(0f,0f,1f);
			GL.Vertex3(0f,0f,0f);
			GL.Vertex3(0f,0f,1f);
			GL.End();

			GL.PopMatrix();

		}

		private void ClearBuffer(){
			GL.ClearColor(0f,0f,0f,0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		}

		private int PickObject(){

			int glId = -1;
			byte[] pixel = new byte[3];
			int[] viewport = new int[4];
			GL.GetInteger(GetPName.Viewport,viewport);
			GL.ReadPixels(Mouse.X,viewport[3] - Mouse.Y, 1,1, PixelFormat.Rgb,PixelType.UnsignedByte, pixel);
			glId = (int)pixel[0] +(((int)pixel[1]<<8)+((int) pixel[2] <<16));

			return glId;

		}
	}
}

