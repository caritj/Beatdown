using System;
using System.Diagnostics;

namespace Beatdown.Game
{
	public class State
	{
		public int SelectedId = -1;
		public int HoveredId = -1;
		public enum States{INITMENU,INITGAME,LOAD,MAINMENU,TOWN,MAP,COMBAT,PLANNER};
		public bool InProgress =false;

		private States current = States.INITMENU;
		private bool paused= true;
		private bool wasPaused =true;
		DateTime startDate = new DateTime (2077, 07, 23);
		public DateTime StartDate{ get { return startDate; } }
		public DateTime CurrentDate{get{return startDate.AddMinutes (Time);}}


		private Stopwatch time = new Stopwatch();
		//PROPERTIES==================================================a
		public bool Paused {
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
		public States Current{
			get{return current;}
		}

		//CONSTRUCTORS================================================b
		public State ()
		{

		}


		//METHODS=====================================================c
		public void ChangeState(States s){


			if (this.Current == States.MAP) {
				wasPaused = Paused;
			}

			if (current != s) {
				//TODO there may be some cleanup and logging here.

				// change the state
				current = s;

				switch (s) {
					case States .MAP:
					Paused = wasPaused;
					break;
					case States.INITGAME:
						time.Reset ();
						break;
				}
			}
		}
	}
}

