using System;
using Gwen;
using Renderer.InterfaceElements;

namespace Renderer
{
	public class GUI
	{
		MainMenu Menu;
		Lobby Lobby;
		Victory Victory;
		InGame Game;


		public GUI (Game.Settings s,Gwen.Control.Canvas c)
		{
			Menu = new MainMenu (c);

		}

		public void Layout(){
		
		}
	}
}

