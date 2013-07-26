using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using BeatDown.Combat;
using BeatDown.Renderer.Resources;

namespace BeatDown.Renderer.GameObjects
{
	public class WeaponRenderer:BaseRender
	{
		private static Dictionary<Weapon.WeaponType, VertexBufferObject> Vbos = new Dictionary<Weapon.WeaponType, VertexBufferObject>();

		public new static void Init ()
		{
			//loads the VBOS
			if (Vbos.Count == 0) {
			
			}
		}


		public static void RenderViewable (Weapon w)
		{
			GL.PushMatrix ();
			GL.Scale (.5f, .5f, .5f);
			GL.Translate (1, 1, 0);
			if (!Vbos.ContainsKey (w.Type)) {
				BaseRender.RenderViewable (w);
			} else {
				Vbos[w.Type].Draw();
			}

			GL.PopMatrix();

		}

	}
}

