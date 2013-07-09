using System;
using OpenTK.Graphics.OpenGL;

namespace BeatDown.Renderer.GameObjects
{
	public class WorldRenderer:BaseRender
	{
		public static void RenderViewable (Game.World w)
		{
			GL.PushMatrix ();
				
			GL.Translate (
				w.X + w.SizeX/-2f -.5, 
				w.Y, 
				w.Z+w.SizeZ/-2f -.5);


			GL.Rotate (w.Rotation, Renderer.Render.UP);
			//GL.Scale (w.SizeX, w.SizeY, w.SizeZ);

			for (int y = 0; y+1 < w.Heightmap.GetLength(0); y++) {




				for(int x = 0; x +1 <w.Heightmap.GetLength(1);x++){


					//TODO TEXTURING.
					//TODO SIDES;
					GL.Begin (BeginMode.Quads);


					GL.Color3 (w.Color);
					GL.Vertex3 ( x , w.Heightmap[y,x],  y);
					GL.Vertex3 ( x , w.Heightmap[y,x], y+1);
					GL.Vertex3 (x +1, w.Heightmap[y,x],y+1);
					GL.Vertex3 ( x +1, w.Heightmap[y,x], y);

					

					GL.End ();


					GL.Begin(BeginMode.QuadStrip);
					GL.Color3(w.SideColor);

					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x],y);

					GL.Vertex3(x+1,0,y);
					GL.Vertex3(x+1,w.Heightmap[y,x],y);
					

					GL.Vertex3(x+1,0,y+1);
					GL.Vertex3(x+1,w.Heightmap[y,x],y+1);


					GL.Vertex3(x,0,y+1);
					GL.Vertex3(x,w.Heightmap[y,x],y+1);

					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x],y);


					GL.End();

			


				}
			}

			GL.PopMatrix();
		
		}
		public static void RenderPickable(Game.World w){
			// NOPE.
		}
	}
}

