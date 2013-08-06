using System;
using System.Collections.Generic;

namespace BeatDown.Game
{
	[Serializable]
	public class Save
	{

		List<Unit> Stable = new List<Unit>();
		String filename;
		String Name{ get { return filename.Substring (filenameStarts, filenameEnds); } }

		protected int filenameStarts=0;
		protected int filenameEnds=0;

		public Save(string filedata){
			filename = filedata;

					if(filename.Contains("/")){
						filenameStarts= filename.LastIndexOf('/')+1;
					}	
					if(filename.Contains("\\")){
						filenameStarts = filename.LastIndexOf('\\')+1;
					}
					filenameEnds = filename.LastIndexOf('.') - filenameStarts;

			//TODO parse the file
		}


		protected void LoadData(){

		}

		protected void SaveData(){

		}
	}
}

