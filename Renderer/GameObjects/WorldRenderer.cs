using System;
using OpenTK.Graphics.OpenGL;

namespace BeatDown.Renderer.GameObjects
{
	public class WorldRenderer:BaseRender
	{
		public static void RenderViewable (Game.World w)
		{
			GL.PushMatrix ();
				
			GL.Translate (w.X, w.Y, w.Z);
			GL.Rotate (w.Rotation, Renderer.Render.UP);
			GL.Scale (w.SizeX, w.SizeY, w.SizeZ);



			for (int i = 0; i < w.Heightmap.GetLength(0); i++) {
				GL.Begin (BeginMode.QuadStrip);

				GL.Color3 (w.Color);

				for(int j = 0; j<w.Heightmap.GetLength(1)-1;i++){


					//TODO TEXTURING.
					GL.Vertex3 (w.SizeX / 2f + j , 0, w.SizeZ / -2f);
					GL.Vertex3 (w.SizeX / 2f + j , 0, w.SizeZ / 2f +1);

					GL.Vertex3 (w.SizeX / -2f + j , w.Heightmap[i,j], w.SizeZ / -2f + i);
					GL.Vertex3 (w.SizeX / -2f + j , w.Heightmap[i,j], w.SizeZ / -2f + i+1);
					GL.Vertex3 (w.SizeX / -2f + j +1, w.Heightmap[i,j], w.SizeZ / -2f + i+1);
					GL.Vertex3 (w.SizeX / -2f + j +1, w.Heightmap[i,j], w.SizeZ / -2f + i);

					GL.Vertex3 (w.SizeX / 2f + j +1, 0, w.SizeZ / -2f);
					GL.Vertex3 (w.SizeX / 2f + j +1, 0, w.SizeZ / 2f +1);



				}
				GL.End ();
			}

			GL.PopMatrix();
		
		}
		public static void RenderPickable(Game.World w){
			//do nothing.
		}
	}
}

