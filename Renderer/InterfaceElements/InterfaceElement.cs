using System;
using System.Drawing;
using BeatDown.Renderer.Resources;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;

namespace BeatDown.Renderer.InterfaceElements
{
	public abstract class InterfaceElement
	{
		public delegate void onMouseUp(MouseButtonEventArgs args);
		public delegate void onMouseDown(MouseButtonEventArgs args);
		public delegate void onClicked(MouseButtonEventArgs args);

		public virtual event onMouseUp  OnMouseUp;
		
		public virtual event onMouseDown OnMouseDown;

		public bool UseParentOffset =true;
		

		public float X,Y,Z;

		public float Width, Height, Depth;

		public float RotationX, RotationY, RotationZ;

		protected InterfaceElement parent;

		public  bool ShouldRedraw =true;
		public bool ShouldDraw =true;

		public bool ShowBackgroundColor =false;
		public System.Drawing.Color BackgroundColor;
		public Brush TextBrush = Brushes.White;

		public Texture Texture=null;

		public string Text{get{return text;}set{RenderString(value); text=value;}}
		protected String text ="";

		public Texture StringTexture =null;

		protected List<InterfaceElement> children = new List<InterfaceElement>();



		public bool WasClicked (int mouseX, int mouseY)
		{
			if (children.Count == 0) {
				return(mouseX >= X && mouseX <= X + Width && mouseY >= Y && mouseY <= Y + Height);
			}


			bool found = false;
			foreach (InterfaceElement child in children) {
				found = found || child.WasClicked(mouseX,mouseY);
 			}
			return found;
		}

		public void MouseUp (MouseButtonEventArgs args)
		{
			if (OnMouseUp != null) {
				OnMouseUp(args);
			}

			foreach (InterfaceElement child in children) {
				if(child.WasClicked(args.X,args.Y)){
					child.MouseUp(args);
				}
			}
		}
		public  void MouseDown (MouseButtonEventArgs args){

			if (OnMouseDown != null) {
				OnMouseDown(args);
			}

			foreach (InterfaceElement child in children) {
				if(child.WasClicked(args.X,args.Y)){
					child.MouseDown(args);
				}
			}
		}

	
		
		public void LoadTexture(String TextureFileName){
			if (SharedResources.TextureCache.ContainsKey (TextureFileName)) {
				Texture = SharedResources.TextureCache [TextureFileName];
			} else {
				Texture = new BeatDown.Renderer.Resources.Texture(Render.Instance.settings.TextureDirectory+ TextureFileName);
				SharedResources.TextureCache.Add(TextureFileName, Texture);
			}
		}
		public void LoadTexture(Bitmap texture){
				Texture = new Texture(texture);
		}


		public void Layout(){

		}
		public abstract  void Layout (int width, int height);

		public void Show(){
			ShouldDraw= true;
		}
		public void Hide(){
			ShouldDraw= false;
		}

		public virtual void Draw ()
		{
			if (ShouldDraw) {

				GL.PushMatrix();

				//offsets base on parent enable
				//if(UseParentOffset &&parent !=null){
				//	GL.Translate(parent.X,parent.Y, parent.Z);
				//}

				//draw colored box if needed
				if (ShowBackgroundColor) {
					DrawBackgroundColor ();
				}
				//draw texture if needed
				if (Texture != null) {
					DrawTexture (Texture);
				}

				//draw text if needed;
				if (StringTexture != null) {
					DrawTexture (StringTexture);
				}

				foreach(InterfaceElement child in children){
					child.Draw();
				}
				GL.PopMatrix();
			}
		}

		protected void DrawBackgroundColor(){
			GL.Begin(BeginMode.Quads);
				GL.Color3(BackgroundColor);
				GL.Vertex3(X,Y,Z);
				GL.Vertex3(X+Width,Y,Z);
				GL.Vertex3(X+Width,Y+Height,Z);
				GL.Vertex3(X,Y+Height,Z);
				GL.Color3(1f,1f,1f);
			GL.End();
		}

		protected void DrawTexture(Texture outputTex){
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, outputTex.glId);

			GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0f,0f);
				GL.Vertex3(X,Y,Z);
			GL.TexCoord2(1f,0f);
				GL.Vertex3(X+Width,Y,Z);
				GL.TexCoord2(1f,1f);
				GL.Vertex3(X+Width,Y+Height,Z);
				GL.TexCoord2(0f,1f);
				GL.Vertex3(X,Y+Height,Z);
			GL.End();
			GL.Disable (EnableCap.Texture2D);

		}
		protected void RenderString (string value)
		{
			if (SharedResources.StringTextureCache.ContainsKey (value)) {
				StringTexture  = SharedResources.StringTextureCache[value];
			} else {
				StringTexture = new Texture (SharedResources.InGameFont, value, TextBrush, (int)Width, (int)Height);
				SharedResources.StringTextureCache.Add(value, StringTexture);
			}
		}
	}
}

