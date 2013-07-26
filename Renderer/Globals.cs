using System;
using System.Collections.Generic;
using System.Drawing;
using BeatDown.Renderer.Resources;

namespace BeatDown.Renderer
{
	public static class SharedResources
	{
		public static Font InGameFont = new Font("arial",16);
		public static bool MouseIsDown =false;
		public static Dictionary<String,Texture> TextureCache = new Dictionary<String,Texture>();
		public static Dictionary<String,Texture> StringTextureCache = new Dictionary<String,Texture>();
	}
}

