using System;
using OpenTK.Input;
using BeatDown.Game;


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
				if (Game.Selection.SelectedId != Game.Selection.NONE) {

					Unit selected = Game.Game.Instance.Manager.Units [Game.Selection.SelectedId];

					if (Game.Selection.HoveredId == Game.Selection.NONE ) {
						//move order
						if( Game.Selection.Maploc > 0){
							//TODO can move to.here
							selected.MoveTo (Game.Selection.MapCoords, 0d);

							//this should deduct action points.
							if(selected.ActionPoints ==0){
								Game.Selection.SelectedId = Game.Selection.NONE;
							}
						}
					} else {
						//did we click a unit?
						if(Game.Selection.HoveredId != Game.Selection.NONE){
							Unit target = Game.Game.Instance.Manager.Units [Game.Selection.HoveredId];
						
							if(target.Team == selected.Team){
								//aid?

							}
							else{
								//attack?
							}
						}
					}
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

