using System;
using System.IO;

namespace  BeatDown.Renderer.Resources
{
	/// <summary>
	/// Represnets a resource to be laoded from the disk and used
	/// </summary>
	public abstract class Resource:IDisposable
	{
		protected string Filename;
		public enum Types{TEXTURE,SOUND,SHADER, MODEL, VBO};
		public int glId;
		public Types Type;

		protected bool contentsUpdated = false;		

		public void Dispose(){

		}
	}
}

