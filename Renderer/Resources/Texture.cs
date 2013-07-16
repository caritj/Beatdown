using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace  BeatDown.Renderer.Resources
{
	public class Texture : Resource
	{
		public static Font font= new Font("Arial", 24f);
		protected Bitmap bmp;
		public Texture (Bitmap bmp){
			Type = Types.TEXTURE;
			glId = GL.GenTexture ();
			GL.BindTexture (TextureTarget.Texture2D, glId);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMinFilter, 
			                (int)TextureMinFilter.Linear);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMagFilter, 
			                (int)TextureMagFilter.Linear);

			BitmapData bmpd = bmp.LockBits (new Rectangle(0,0,bmp.Width,bmp.Height),
			                                ImageLockMode.ReadOnly, 
			                                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D (TextureTarget.Texture2D,
			               0,
			               PixelInternalFormat.Rgba,
			               bmpd.Width,
			               bmpd.Height,
			               0,
			               OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
			               PixelType.UnsignedByte, 
			               bmpd.Scan0);
			bmp.UnlockBits (bmpd);
		}


		public Texture (String File)
		{
			Type = Types.TEXTURE;
			base.Filename = File;

			bmp = new Bitmap (File);

			glId = GL.GenTexture ();
			GL.BindTexture (TextureTarget.Texture2D, glId);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMinFilter, 
			                (int)TextureMinFilter.Linear);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMagFilter, 
			                (int)TextureMagFilter.Linear);

			BitmapData bmpd = bmp.LockBits (new Rectangle(0,0,bmp.Width,bmp.Height),
			                                ImageLockMode.ReadOnly, 
			                                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D (TextureTarget.Texture2D,
			              0,
			              PixelInternalFormat.Rgba,
			              bmpd.Width,
			              bmpd.Height,
			              0,
			              OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
			              PixelType.UnsignedByte, 
			              bmpd.Scan0);
			bmp.UnlockBits (bmpd);
			 

		}

		public Texture(String text, Brush b, int w, int h){
			
			Type = Types.TEXTURE;
			bmp = new Bitmap (w, h);

			bmp.MakeTransparent ();
			System.Drawing.Graphics g = Graphics.FromImage (bmp);
			int t_size = (int)(g.MeasureString (text, font).Width);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			g.DrawString (text, font, b, (w-t_size)/2, 8, new StringFormat ());
			g.Flush ();

			glId = GL.GenTexture ();
			GL.BindTexture (TextureTarget.Texture2D, glId);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMinFilter, 
			                (int)TextureMinFilter.Linear);

			GL.TexParameter(TextureTarget.Texture2D, 
			                TextureParameterName.TextureMagFilter, 
			                (int)TextureMagFilter.Linear);

			BitmapData bmpd = bmp.LockBits (new Rectangle(0,0,bmp.Width,bmp.Height),
			                                ImageLockMode.ReadOnly, 
			                                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D (TextureTarget.Texture2D,
			               0,
			               PixelInternalFormat.Rgba,
			               bmpd.Width,
			               bmpd.Height,
			               0,
			               OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
			               PixelType.UnsignedByte, 
			               bmpd.Scan0);
			bmp.UnlockBits (bmpd);
		}
	}
}

