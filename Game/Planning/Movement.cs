using System;
using System.Collections.Generic;

namespace BeatDown.Game.Planning
{
	public class Movement:ITask
	{	
		protected List<Coords> path;
		protected Unit movee;
		protected bool done =false;

		public List<Coords>Path{ get { return path; } }
		public Movement (Unit Mover, List<Coords> Path)
		{
			path = Path;

			movee = Mover;
		}
		public Movement (Unit Mover, List<Coords> Path, bool completed)
		{
			path = Path;

			movee = Mover;
			done =completed;
		}
		public int GetCost ()
		{
			return path.Count;
		}

		public ITask Execute (int ActionPoints)
		{
			List< Coords> moved = new List<Coords> ();
			int start = Math.Min (ActionPoints, path.Count);
			for (int i = 0; i<start; i++) {
				moved.Add (path [0]);
				path.RemoveAt (0);
			}

			if (path.Count == 0) {
				done=true;
			}
			//the user had aps to finish this much movement.
			return new Movement(movee, moved, true);

		}

		public bool GetCompleted ()
		{
			return done;
		}


	}
}

