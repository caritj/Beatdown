using System;
using OpenTK.Input;


namespace BeatDown.Renderer
{
	public class InputHandler
	{
	

		public static void OnMouseUp (object sender, MouseButtonEventArgs args)
			{

				SharedResources.MouseIsDown = false;

				//select with left click
				if (args.Button == MouseButton.Left) {
					Game.Selection.SelectedId = Game.Selection.HoveredId;
				}
				//act with right click
				if (args.Button == MouseButton.Right) {
					if (Game.Selection.HoveredId == 0) {
						//move order
					//	Renderer.Render.Instance.theGame.Manager.GetUnitByName (Game.Selection.SelectedId.ToString).MoveTo (Game.Selection.MapCoords);
					} else {
						//attack order?
						//see whats up.
						
					}
				}
		
		}

		public static void OnMouseDown (object sender, MouseButtonEventArgs args)
		{
			//switch( Render.Instance.theGame.State//
			SharedResources.MouseIsDown = true;


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

