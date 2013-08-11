using System;

namespace BeatDown.Game.Planning
{
	public class Attack:ITask
	{
		Unit Attacker,Defender;
		bool done = false;
		public Attack (Unit attacker, Unit defender)
		{
			Attacker = attacker;
			Defender = defender;

		}

		public int GetCost ()
		{
			return Attacker.Weapon.APCost;
		}

		public ITask Execute (int ActionPoints)
		{
			if (ActionPoints < this.GetCost ()) {
				Attacker.Weapon.Fire(Attacker,Defender);
											
				done = true;
				return this;
			}
			throw new ImpossibleTaskException("Attempt to use Execute Plan with insufficent AP");
			return null;
		}

		public bool GetCompleted ()
		{
			return done;
		}


	}
}

