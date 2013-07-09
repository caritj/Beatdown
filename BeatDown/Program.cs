using System;
using Game;

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
	}
}
