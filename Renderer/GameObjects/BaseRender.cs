using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using BeatDown.Renderer.Resources;
using System.Drawing;

namespace BeatDown.Renderer.GameObjects
{
	public abstract class BaseRender
	{
		private static VertexBufferObject vbo=null;


		public static void Init ()
		{
			if(vbo==null){
				 VertexPositionColor[] CubeVertices = new VertexPositionColor[]
        {
                new VertexPositionColor( 0.0f, 0.0f,  1.0f, Color.HotPink),
                new VertexPositionColor( 1.0f, 0.0f,  1.0f, Color.HotPink),
                new VertexPositionColor( 1.0f,  1.0f,  1.0f, Color.HotPink),
                new VertexPositionColor( 0.0f,  1.0f,  1.0f, Color.HotPink),
                new VertexPositionColor( 0.0f, 0.0f, 0.0f, Color.HotPink),
                new VertexPositionColor( 1.0f, 0.0f, 0.0f, Color.HotPink), 
                new VertexPositionColor( 1.0f,  1.0f, 0.0f, Color.HotPink),
                new VertexPositionColor( 0.0f,  1.0f, 0.0f, Color.HotPink) 
        };
				short[] CubeElements = new short[]
				{
					0, 1, 2, 2, 3, 0, // front face
					3, 2, 6, 6, 7, 3, // top face
					7, 6, 5, 5, 4, 7, // back face
					4, 0, 3, 3, 7, 4, // left face
					0, 1, 5, 5, 4, 0, // bottom face
					1, 5, 6, 6, 2, 1, // right face
				};
				vbo= new VertexBufferObject(CubeVertices, CubeElements);
			}

		}
 

		public static void RenderViewable (Game.Renderable r)
		{
			//dpo things in a isolate envorinment
			GL.PushMatrix ();
			{
				GL.Translate (r.X, r.Y, r.Z);
				GL.Rotate ((float)r.Rotation, Renderer.Render.UP);
				GL.Scale (.75f, .75f, .75f);
				GL.Translate (r.SizeX / -2f, 0f, r.SizeZ / -2f);
				GL.Scale(r.SizeX, r.SizeY, r.SizeZ);
					
				if(vbo == null){	
				//	Console.WriteLine ("NOT using VBO");
					GL.Begin (BeginMode.QuadStrip);
					{
						GL.Color3 (r.Color);


						GL.Vertex3 (0, 0, 0);
						GL.Vertex3 (0, r.SizeY, 0);

						GL.Vertex3 (r.SizeX, 0, 0);
						GL.Vertex3 (r.SizeX, r.SizeY, 0);
									

						GL.Vertex3 (r.SizeX, 0, r.SizeZ);
						GL.Vertex3 (r.SizeX, r.SizeY, r.SizeZ);


						GL.Vertex3 (0, 0, r.SizeZ);
						GL.Vertex3 (0, r.SizeY, r.SizeZ);

						GL.Vertex3 (0, 0, 0);
						GL.Vertex3 (0, r.SizeY, 0);

						
					}

					GL.End ();

					GL.Begin (BeginMode.Quads);
					{
						GL.Vertex3 (0, r.SizeY, 0);
						GL.Vertex3 (0, r.SizeY, r.SizeZ);
						GL.Vertex3 (r.SizeX, r.SizeY, r.SizeZ);
						GL.Vertex3 (r.SizeX, r.SizeY, 0);
					}
					GL.End ();
				}
				else{
				//	Console.WriteLine ("using VBO");
					vbo.Draw ();
				}


				if (r.glId == Game.Selection.SelectedId) {

					GL.Color3(System.Drawing.Color.PaleVioletRed);
					GL.Begin(BeginMode.Lines);
					GL.Vertex3 (0, 0, 0);
					GL.Vertex3 (r.SizeX, 0, 0);
					GL.Vertex3 (r.SizeX, 0, r.SizeZ);
					GL.Vertex3 (0, 0, r.SizeZ);
					GL.Vertex3 (0,0,0);
					GL.End ();

				}
			}
			GL.PopMatrix();
		}
		public static void RenderPickable(Game.Renderable r){
			//dpo things in a isolate envorinment
			GL.PushMatrix();
				
				GL.Translate(r.X,r.Y,r.Z);
				GL.Rotate((float)r.Rotation, Renderer.Render.UP);
				//GL.Scale(.75f,.75f,.75f);//Dont scale teh clicing thing sowe occupy thew whole tile
				GL.Translate(r.SizeX/-2f, 0f, r.SizeZ/-2f);
				
					
				GL.Begin(BeginMode.QuadStrip);
					byte[] color =PickingColorfromInt(r.glId);
					GL.Color3(color[0], color[1], color[2]);

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

				/*GL.Begin(BeginMode.Quads);
				
				GL.Vertex3(0, r.SizeY, 0);
				GL.Vertex3(0,r.SizeY, r.SizeZ);
				GL.Vertex3( r.SizeX, r.SizeY, r.SizeZ);
				GL.Vertex3( r.SizeX, r.SizeY, 0);
				
				

				
				GL.End ();
*/

			GL.PopMatrix();

		}

		public static byte[] PickingColorfromInt(int id){
			Byte[] output = new byte[3];
			output[0] = (byte) (id & 0x000000FF);
			output[1] = (byte)((id & 0x0000FF00) >> 08);
			output[2] = (byte)((id & 0x00FF0000) >> 16);
			return output;
		}

	}
}

