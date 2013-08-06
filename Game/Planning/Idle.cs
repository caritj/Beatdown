using System;

namespace BeatDown.Game.Planning
{
	public class Idle:ITask
	{
		public Idle ()
		{
		}
		public int GetCost ()
		{
			throw new System.NotImplementedException ();
		}

		public ITask Execute (int ActionPoints)
		{
			throw new System.NotImplementedException ();
		}

		public bool GetCompleted ()
		{
			throw new System.NotImplementedException ();
		}

	}
}

