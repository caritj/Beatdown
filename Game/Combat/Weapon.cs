using System;
using BeatDown.Game;

namespace BeatDown.Combat
{
	public abstract class Weapon
	{
		public enum WeaponType
		{
			MELEE,
			PROJECILE,
			MAGIC,
			ORBITAL
		}

		public enum DamageTypes
		{
			BLUNT,
			PEIRCING,
			DRAMA,
			FIRE,
			DISINTERGRATION,
			MAGIC

		}

		protected WeaponType weaponType = WeaponType.MELEE;
		public WeaponType Type {get{ return weaponType;}}

		protected DamageTypes damageType = DamageTypes.BLUNT;
		public DamageTypes DamageType{get{return damageType;}}

		protected int range =1;
		public int Range{get{return range;}}

		protected int minDamage;
		public int MinDamage{ get { return minDamage; } }

		protected int maxDamage;
		public int MaxDamage{ get { return maxDamage; } }

		public bool InRange(Coords origin, Coords target){
			bool inRange = false;
			switch(this.Type){
				case WeaponType.PROJECILE:
				//TODO LINE OF SIGHT
				case WeaponType.MELEE:
				case WeaponType.MAGIC:
				inRange =	Math.Abs(target.X- origin.X) <=1 && 
			   				Math.Abs(target.Y- origin.Y) <=1 &&
							Math.Abs(target.Z- origin.Z) <=1;
				break;
				case WeaponType.ORBITAL:
					inRange =true;
				break;

				}

			return inRange;
		}





	}
}

