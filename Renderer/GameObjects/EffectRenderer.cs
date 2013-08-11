using System;
using OpenTK.Graphics.OpenGL;
using BeatDown.Renderer.Resources;



namespace BeatDown.Renderer.GameObjects
{
	public class EffectRenderer:BaseRender
	{
		public static void RenderViewable (Effect e)
		{

			GL.PushMatrix ();
			GL.Translate (e.X, e.Y, e.Z);
				
			//Rotate based on activities
			switch (e.Aligned) {
				case Effect.Alignment.FIXED:
					GL.Rotate(e.Rotation, e.RotationAlignment);
				break;
				case Effect.Alignment.CAMERA:
				//TODO

				case Effect.Alignment.UP:
				default:
					GL.Rotate(e.Rotation, Render.UP);
				break;
			}
			GL.BindTexture(TextureTarget.Texture2D, e.Texture.glId);
			GL.Begin(BeginMode.Quads);
					
				GL.TexCoord2(0,1);
				GL.Vertex3(e.X-.5f*e.SizeX,e.Y, e.Z+.5f*e.SizeX);
				GL.TexCoord2(1,1);
				GL.Vertex3(e.X+.5f*e.SizeX,e.Y, e.Z+.5f*e.SizeX);
				GL.TexCoord2(1,0);
				GL.Vertex3(e.X+.5f*e.SizeX,e.Y, e.Z-.5f*e.SizeX);
				GL.TexCoord2(0,0);
				GL.Vertex3(e.X-.5f*e.SizeX,e.Y, e.Z-.5f*e.SizeX);
					

			GL.End ();	

			GL.PopMatrix();
		}
		public static void RenderPickable(Effect e){
			//DOES NOTHING SINCE EFFECTS ARE NOT PICKABLE
		}
	}
}

