using System;
using Gwen;
using Gwen.Control;
using Gwen.Control.Layout;	

namespace BeatDown.Renderer.InterfaceElements
{
	public class MainMenu:Gwen.Control.DockBase
	{
		ListBox Menu ;

		public MainMenu (Base parent):base(parent)
		{
			Menu= new ListBox(this);
			Menu.SetPosition(Height/3, Width/6);
			Menu.SetBounds(220, 10, 200, 200);
			Menu.RowSelected += MenuClicked;

			Menu.AddRow("PLAY");
			Menu.AddRow("SETTINGS");
			Menu.AddRow("QUIT");

			Menu.SizeToContents();
		} 	

		public void MenuClicked(Base contorl, EventArgs e){

		}
	}
}