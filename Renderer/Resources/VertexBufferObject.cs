using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using BeatDown.Renderer.Resources;

namespace BeatDown.Renderer.Resources
{
	public class VertexBufferObject
	{
		public int VboId, EboId, ElementSize;
		private int vbobuffersize, ebobuffersize,vertstride;


		public VertexBufferObject(VertexPositionColor[] verts, short[] elements)
		{

			//TODO error on misallovated buffers
			vertstride =BlittableValueType.StrideOf(verts);
			//bind our vertexes

			GL.GenBuffers(1,out this.VboId);
			GL.BindBuffer (BufferTarget.ArrayBuffer, this.VboId);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts.Length * vertstride), verts, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out vbobuffersize);

			//bind our indexes
			GL.GenBuffers(1, out this.EboId);
			GL.BindBuffer (BufferTarget.ElementArrayBuffer, this.EboId);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * BlittableValueType.StrideOf(elements)), elements, BufferUsageHint.StaticDraw);

			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer,BufferParameterName.BufferSize, out ebobuffersize);

			this.ElementSize   = elements.Length;



		}

		public void Draw(){
 			GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);


            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VboId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EboId);

            GL.VertexPointer(3, VertexPointerType.Float, vertstride, new IntPtr(0));
			GL.ColorPointer(4, ColorPointerType.UnsignedByte, vertstride, new IntPtr(12));
		
          

            GL.DrawElements(BeginMode.Triangles, this.ElementSize, DrawElementsType.UnsignedShort, IntPtr.Zero);
    
		}


	}
}

