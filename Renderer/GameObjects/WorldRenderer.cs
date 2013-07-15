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
				w.X  -.5, 
				w.Y, 
				w.Z -.5);


			GL.Rotate (w.Rotation, Renderer.Render.UP);
			//GL.Scale (w.SizeX, w.SizeY, w.SizeZ);

			for (int y = 0; y< w.Heightmap.GetLength(0); y++) {




				for(int x = 0; x  <w.Heightmap.GetLength(1);x++){


					//TODO TEXTURING.
					//TODO SIDES;
					GL.Begin (BeginMode.Quads);


					GL.Color3 (w.Color);
					if(w.Heightmap[y,x].Y >0){
						System.Drawing.Color c = System.Drawing.Color.FromArgb(
							(int)w.Color.A, 
							(int)w.Color.R+10* w.Heightmap[y,x].Y, 
							(int)w.Color.G+10* w.Heightmap[y,x].Y, 
							(int)w.Color.B+10* w.Heightmap[y,x].Y);
						GL.Color3(c);
					}
					GL.Vertex3 ( x , w.Heightmap[y,x].Y,  y);
					GL.Vertex3 ( x , w.Heightmap[y,x].Y, y+1);
					GL.Vertex3 (x +1, w.Heightmap[y,x].Y,y+1);
					GL.Vertex3 ( x +1, w.Heightmap[y,x].Y, y);

					

					GL.End ();


					GL.Begin(BeginMode.QuadStrip);
					GL.Color3(w.SideColor);

					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y);

					GL.Vertex3(x+1,0,y);
					GL.Vertex3(x+1,w.Heightmap[y,x].Y,y);
					

					GL.Vertex3(x+1,0,y+1);
					GL.Vertex3(x+1,w.Heightmap[y,x].Y,y+1);


					GL.Vertex3(x,0,y+1);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y+1);

					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y);


					GL.End();

			


				}
			}

			GL.PopMatrix();
		
		}
		public static void RenderPickable(Game.World w){
			GL.PushMatrix ();
				
			GL.Translate (
				w.X  -.5, 
				w.Y, 
				w.Z-.5);

			int _color = 1;
			GL.Rotate (w.Rotation, Renderer.Render.UP);
			//GL.Scale (w.SizeX, w.SizeY, w.SizeZ);
			for (int y = 0; y < w.Heightmap.GetLength(0); y++) {
				for(int x = 0; x  <w.Heightmap.GetLength(1);x++){
					GL.Color4(1f,1f,1f,1f);
					_color = w.Heightmap.GetLength(1) * y  + x+1;

					byte[] color =PickingColorfromInt(_color);
					GL.Color3(color);

					//TODO TEXTURING.
					//TODO SIDES;
					GL.Begin (BeginMode.Quads);





					GL.Vertex3 ( x , w.Heightmap[y,x].Y,  y);
					GL.Vertex3 ( x , w.Heightmap[y,x].Y, y+1);
					GL.Vertex3 (x +1, w.Heightmap[y,x].Y,y+1);
					GL.Vertex3 ( x +1, w.Heightmap[y,x].Y, y);

					

					GL.End ();

					//TODO match sides to the correct number
					GL.Begin(BeginMode.QuadStrip);
				
					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y);

					GL.Vertex3(x+1,0,y);
					GL.Vertex3(x+1,w.Heightmap[y,x].Y,y);
					

					GL.Vertex3(x+1,0,y+1);
					GL.Vertex3(x+1,w.Heightmap[y,x].Y,y+1);


					GL.Vertex3(x,0,y+1);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y+1);

					GL.Vertex3(x,0,y);
					GL.Vertex3(x,w.Heightmap[y,x].Y,y);


					GL.End();

				}
			}

			GL.PopMatrix();
		}


	}
}

