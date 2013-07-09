using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Renderer.GameObjects
{
	public abstract class BaseRender
	{
		protected int glId;

		public void RenderViewable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();
				
				GL.Translate(r.X,r.Y,r.Z);
				GL.Rotate(r.Rotation);
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
		public void RenderViewable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();

			GL.Translate(r.X,r.Y,r.Z);
			GL.Rotate(r.Rotation);
			GL.Scale(r.SizeX, r.SizeY, r.SizeZ);


			GL.Begin(BeginMode.QuadStrip);
			GL.Color3(this.glId);
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

