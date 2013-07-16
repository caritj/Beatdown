using System;
using OpenTK.Input;


namespace BeatDown.Renderer
{
	public class InputHandler
	{

		public static void OnMouseUp(object sender, MouseButtonEventArgs args){
			SharedResources.MouseIsDown=false;
		}

		public static void OnMouseDown(object sender, MouseButtonEventArgs args){

			SharedResources.MouseIsDown=true;

		}

		public static void OnMouseClick(object sender, MouseButtonEventArgs args){

		}

		public static void OnMouseWheeled(object sender, MouseWheelEventArgs args){

		}

		public static void OnKeyDown(object sender, KeyboardKeyEventArgs e){

		}

		public static void OnKeyUp (object sender, KeyboardKeyEventArgs e){

		}

	}
}

