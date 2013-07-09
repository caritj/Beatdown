using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Renderer.GameObjects
{
	public abstract class BaseRender
	{
		protected int glId;

		public static void RenderViewable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();
				
				GL.Translate(r.X,r.Y,r.Z);
				GL.Rotate((float)r.Rotation, Renderer.Render.UP);
				GL.Scale(r.SizeX, r.SizeY, r.SizeZ);

				
				GL.Begin(BeginMode.QuadStrip);
				GL.Color3(r.Color);
				GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f,r.SizeZ/-2f);
				GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);
				GL.Vertex3(r.SizeX/-2f, r.SizeY/2f,r.SizeZ/-2f);
				GL.Vertex3(r.SizeX/-2f, r.SizeY/2f, r.SizeZ/2f);

				GL.Vertex3( r.SizeX/2f,  r.SizeY/2f,r.SizeZ/-2f);
				GL.Vertex3( r.SizeX/2f,  r.SizeY/2f, r.SizeZ/2f);

				GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/-2f);
				GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/2f);

				GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f,r.SizeZ/-2f);
				GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);

				GL.End();

				GL.Begin(BeginMode.Quads);
				GL.Vertex3( r.SizeX/2f, r.SizeY/2f, r.SizeZ/2f);
				GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/2f);
				GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);
				GL.Vertex3(r.SizeX/-2f, r.SizeY/2f, r.SizeZ/2f);
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
			GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY/2f,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY/2f, r.SizeZ/2f);

			GL.Vertex3( r.SizeX/2f,  r.SizeY/2f,r.SizeZ/-2f);
			GL.Vertex3( r.SizeX/2f,  r.SizeY/2f, r.SizeZ/2f);

			GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/-2f);
			GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/2f);

			GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f,r.SizeZ/-2f);
			GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);

			GL.End();

			GL.Begin(BeginMode.Quads);
			GL.Vertex3( r.SizeX/2f, r.SizeY/2f, r.SizeZ/2f);
			GL.Vertex3( r.SizeX/2f,r.SizeY/-2f, r.SizeZ/2f);
			GL.Vertex3(r.SizeX/-2f,r.SizeY/-2f, r.SizeZ/2f);
			GL.Vertex3(r.SizeX/-2f, r.SizeY/2f, r.SizeZ/2f);
			GL.End ();


			GL.PopMatrix();
		}

	}
}

