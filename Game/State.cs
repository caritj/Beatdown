using System;
using System.Diagnostics;

namespace BeatDown.Game
{
	public class State
	{

		public enum States{INGAME,MENU,VICTORY,DEFEAT,LOBBY,LOADING};
		public bool InProgress =false;

		private States current = States.LOADING;
		DateTime startDate = new DateTime (2077, 07, 23);
		public DateTime StartDate{ get { return startDate; } }
		//public DateTime CurrentDate{get{return startDate.AddMinutes (Time);}}


		//private Stopwatch time = new Stopwatch();
		//PROPERTIES==================================================a
		/*public bool Paused {
			get{ return paused; }
			set { 
				paused = value; 
				if (paused) {
					time.Stop ();
				} else {
					time.Start ();
				}
			}
		}
		public double Time{get{return time.ElapsedMilliseconds;}}
		*/
		public States Current{
			get{return current;}
		}

		//CONSTRUCTORS================================================b
		public State ()
		{

		}


		//METHODS=====================================================c
		public void ChangeState(States s){
			current = s;
			Console.WriteLine("state changed to " +s);
		}
	}
}

