using System;
using System.Drawing;
using System.Drawing.Text;
using BeatDown.Game;


namespace BeatDown
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine (String.Format ("Init BEATDOWN version:{0}", System.Reflection.Assembly.GetExecutingAssembly ().GetName ().Version));

			Settings s = new Settings ();
			using (Game.Game g = new Game.Game(s)) {
				using (Renderer.Render renderer = new Renderer.Render(ref g, ref s)){
					renderer.Run (30.0);
				}
			}
			Console.WriteLine ("exiting and saving");

			s.SaveConfig ();

		}

		public static void checkforFonts ()
		{
			string[] fonts = {"arial"};
			bool [] found = new bool[fonts.Length];

			for (int i=0; i<fonts.Length; i++) {
				found[i]=false;

			}

			InstalledFontCollection ifc = new InstalledFontCollection ();
			foreach (FontFamily fm in  ifc.Families) {
				for(int i = 0 ;i <  fonts.Length;i++){
					if(fm.Name.ToLower() == fonts[i].ToLower()){

					}
				}
			}

		}
	}
}
