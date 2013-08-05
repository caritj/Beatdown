using System;
using BeatDown.Game;

namespace BeatDown.Combat
{
	public abstract class Weapon:Renderable
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
			MAGIC,
			GENERIC

		}
		protected bool usesAmmo = false;
		public bool UsesAmmo { get { return usesAmmo; } }

		protected int remainingAmmo  =1;
		public int RemainingAmmo { get { return remainingAmmo; } }


		protected WeaponType weaponType = WeaponType.MELEE;
		public WeaponType Type {get{ return weaponType;}}

		protected DamageTypes damageType = DamageTypes.BLUNT;
		public DamageTypes DamageType{get{return damageType;}}

		protected int range =1;
		public int Range{get{return range;}}

		protected int minDamage=1;
		public int MinDamage{ get { return minDamage; } }

		protected int maxDamage=5;
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


		public void Fire (Unit Shooter, Unit Target)
		{
			if (Shooter.CanAttack(Target) ) {
				if (usesAmmo) {
					remainingAmmo--;
				}

				//skills and what not
				if(Shooter.DidHit(Target, this)){

					//Calculate Damage
					int dam = Game.Game.Instance.RandomNumberGenerator.Next(minDamage, maxDamage);
					//apply to the Target
					Target.TakeDamage(dam, this.damageType);

				}


			}


		}






	}
}

