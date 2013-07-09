using System;
using System.IO;

namespace BeatDown.Game
{

			public class Settings
			{
				public const string root= "../../";
				public const String CONFIG_FILE = root+"config.ini";
				public const String RESOURCES_FILE = root+"resources.txt";
				public const String SAVE_DIR = "%appdata%/Raven/save";

				public const String GUI_DATA_DIR = root + "data/gui/";
				public int TimeScale = 30;

				public double MouseSensitivity =1.0d;
				public int GraphicsX = 800;
				public int GraphicsY =600;
				public bool ShowFPS=true;

				public bool VSync = true;
				public static System.Drawing.Font DefaultFont = new System.Drawing.Font ("Arial", 1f);

				public string TextureDirectory =root+"data/tex/";
				public string SoundDirectory =root+"data/snd/";
				public string ModelDirectory =root+"data/mdl/";
				public string WorldDirectory =root+"data/wld/";
				public string ShaderDirectory =root+"data/shd/";
				public string GuiDirectory =GUI_DATA_DIR;


				public Settings(){
					LoadConfig ();
				}

				protected void LoadConfig(){
					Console.WriteLine ("Loading Settings...");
					Console.WriteLine (" reading '"+CONFIG_FILE+"'...");

					String [] configdata = File.ReadAllLines (CONFIG_FILE);
					String tmp;
					String key;

					int idx = 0;
					foreach(String line in configdata){
						tmp = line.Trim ();
						idx = tmp.IndexOf ("#");
						if (idx >= 0) {
							tmp = tmp.Substring (0, idx);
						}
						idx = tmp.IndexOf ("=");

						if (tmp.Length > 0 && idx >= 0 &&tmp.Length > idx) {
							key = tmp.Substring (0, idx);
							tmp = tmp.Substring (idx+1);
							#region reading all the possible config flags
							switch (key) {
								case "vsync":
								this.VSync = tmp.ToLower ().Trim () == "true";
								break;
								case "graphicsX":
								int.TryParse (tmp, out this.GraphicsX);
								break;
								case "graphicsY":
								int.TryParse (tmp, out this.GraphicsY);
								break;
								case "sensitivity":
								double.TryParse (tmp, out this.MouseSensitivity);
								break;


								default:
								Console.WriteLine (String.Format("Invalid config key \"{0}\" ignored",key));
								break;

							}
							#endregion
						}

					}





				}
				public void SaveConfig(){
					//TODO PREserve the file format...
					//File.ReadAllText (CONFIG_FILE);
					File.WriteAllLines (CONFIG_FILE, new String[] {
						"#Config file",
						"vsync="+this.VSync,
						"graphicsX="+this.GraphicsX,
						"graphicsY="+this.GraphicsY,
						"sensitivity="+this.MouseSensitivity

					});



				}

				public void LoadResourceFile(string file){
					String tmp;
					String type;


					int idx;

					Console.WriteLine (" reading '"+RESOURCES_FILE+"'...");
					StreamReader fs  = new StreamReader(File.OpenRead (RESOURCES_FILE));

					while (fs.Peek() >=0) {

						tmp = fs.ReadLine ().Trim ();
						idx = tmp.IndexOf ("#");
						if (idx >= 0) {
							tmp = tmp.Substring (0, idx);
						}
						idx = tmp.IndexOf ("@");

						if (tmp.Length > 0 && idx >= 0 && tmp.Length > idx) {
							type = tmp.Substring (0, idx).ToUpper().Trim();
							tmp = tmp.Substring (idx+1);


							//at thsi point we ahvea  type a filename
							switch(type){
								case "TEXTURE":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.TEXTURE);
								break;
								case "MODEL":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.MODEL);
								break;
								case "SOUND":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.SOUND);
								break;
								case "HEIGHTMAP":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.HEIGHTMAP);
								break;
								case "VBO":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.VBO);
								break;
								case "WORLD":
					//			Game.Instance.Manager.AddResource (tmp, Resource.Types.WORLD);
								break;
							}


						}
					}
				}



		}
	}


