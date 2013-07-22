using System;
using OpenTK.Graphics.OpenGL;
using BeatDown.Combat;

namespace BeatDown.Renderer.GameObjects
{
	public class WeaponRenderer:BaseRender
	{
		public static void RenderViewable (Weapon w){
			GL.PushMatrix();
			GL.Scale(.5f,.5f,.5f);
			GL.Translate(1,1,0);
			BaseRender.RenderViewable(w);

			GL.PopMatrix();

		}

	}
}

