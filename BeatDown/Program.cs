using System;
using System.Drawing;
using System.Drawing.Text;
using BeatDown.Game;
//using BeatDown.Net;


namespace BeatDown
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine (String.Format ("Init BEATDOWN version:{0}", System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version));

			CheckforFonts ();

			Settings s = new Settings ();
		///	IConnection connection = Connection.GetControlConnection ();
		//	connection.Send (new TestMessage ());

			using (Game.Game g = new Game.Game(s)){//connection.CreateGame("A Winner Is You", s)) {
				using (Renderer.Render renderer = new Renderer.Render(ref g, ref s)){
					renderer.Run (30.0);
				}
			}
			Console.WriteLine ("exiting and saving");

			s.SaveConfig ();

		}


		public static void CheckforFonts ()

		{
			string[] fonts = {"arial"};
			bool [] found = new bool[fonts.Length];


			for (int i=0; i<found.Length; i++) {
				found [i] = false;
		

			}

			InstalledFontCollection ifc = new InstalledFontCollection ();
			foreach (FontFamily fm in  ifc.Families) {
				for (int i = 0; i <  fonts.Length; i++) {
					if (fm.Name.ToLower () == fonts [i].ToLower ()) {
						found [i] = true;
					}
				}
			}

			for (int i=0; i<found.Length; i++) {
				if(!found[i]){
					Console.WriteLine("Unable to load font :"+fonts[i]);
				}
			}

		}
	}
}
