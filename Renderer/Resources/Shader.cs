using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace  BeatDown.Renderer.Resources
{
	public class Shader:Resource
	{
		public ShaderType ShaderType;
		public Shader(ShaderType type, string filename)
		{
			ShaderType = type;
			Type = Types.SHADER;
			glId = GL.CreateShader(type);
			GL.ShaderSource(glId, File.ReadAllText(Render.Instance.settings.ShaderDirectory+filename));
			GL.CompileShader(glId);
			Console.WriteLine(GL.GetShaderInfoLog(glId));
		}
	}
}

