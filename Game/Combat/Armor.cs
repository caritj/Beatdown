using System;
using System.Collections.Generic;

namespace BeatDown.Combat
{
	public abstract class Armor
	{
		public enum ArmorTypes{
			NONE,
			TERRIBAD,
			AMAZING,
			BULLETPROOF,
			THE_MAGENETIC_SHIELD// from scorch no one?
		}

		protected Dictionary<Weapon.DamageTypes, double> reductions =  new Dictionary<Weapon.DamageTypes, double>();

		protected const int MIN_DAMAGE =1;

		public int GetReducedDamage (int input_damage, Weapon.DamageTypes type)
		{

			int output = MIN_DAMAGE;

			if (reductions.ContainsKey (type)) {
				output = (int)Math.Floor (reductions [type] * input_damage);
			} else {
				output = input_damage;
			}

			if (output < MIN_DAMAGE) {
				output = MIN_DAMAGE;
			}

			return output;
		}

	}
}

