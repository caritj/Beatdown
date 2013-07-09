using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BeatDown.Renderer.GameObjects
{
	public abstract class BaseRender
	{
		protected int glId;

		public static void RenderViewable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();
				
				GL.Translate(r.X,r.Y,r.Z);
				GL.Rotate((float)r.Rotation, Renderer.Render.UP);
				GL.Scale(.75f,.75f,.75f);
				GL.Translate(r.SizeX/-2f, 0f, r.SizeZ/-2f);
				
					
				GL.Begin(BeginMode.QuadStrip);
					GL.Color3(r.Color);

					GL.Vertex3(0,0,0);
					GL.Vertex3(0,r.SizeY,0);

					GL.Vertex3(r.SizeX,0,0);
					GL.Vertex3(r.SizeX,r.SizeY,0);
					

					GL.Vertex3(r.SizeX,0,r.SizeZ);
					GL.Vertex3(r.SizeX,r.SizeY,r.SizeZ);


					GL.Vertex3(0,0,r.SizeZ);
					GL.Vertex3(0,r.SizeY,r.SizeZ);

					GL.Vertex3(0,0,0);
					GL.Vertex3(0,r.SizeY,0);


					GL.End();

				GL.Begin(BeginMode.Quads);
				
				GL.Vertex3(0, r.SizeY, 0);
				GL.Vertex3(0,r.SizeY, r.SizeZ);
				GL.Vertex3( r.SizeX, r.SizeY, r.SizeZ);
				GL.Vertex3( r.SizeX, r.SizeY, 0);
				
				

				
				GL.End ();


			GL.PopMatrix();
		}
		public static void RenderPickable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();

			GL.Translate(r.X,r.Y,r.Z);
			GL.Rotate(r.Rotation, Renderer.Render.UP);
			GL.Scale(r.SizeX, r.SizeY, r.SizeZ);


			GL.Begin(BeginMode.QuadStrip);
			GL.Color3(ref r.glId);
			GL.Vertex3(r.SizeX/-2f,0,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f,0, r.SizeZ/2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY, r.SizeZ/2f);

			GL.Vertex3( r.SizeX/2f,  r.SizeY,r.SizeZ/-2f);
			GL.Vertex3( r.SizeX/2f,  r.SizeY, r.SizeZ/2f);

			GL.Vertex3( r.SizeX/2f,0, r.SizeZ/-2f);
			GL.Vertex3( r.SizeX/2f,0, r.SizeZ/2f);

			GL.Vertex3(r.SizeX/-2f,0,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f,0, r.SizeZ/2f);

			GL.End();

			GL.Begin(BeginMode.Quads);
			GL.Vertex3( r.SizeX/2f, r.SizeY, r.SizeZ/2f);
			GL.Vertex3( r.SizeX/2f, r.SizeY, r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY, r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY, r.SizeZ/2f);
			GL.End ();


			GL.PopMatrix();
		}

	}
}

