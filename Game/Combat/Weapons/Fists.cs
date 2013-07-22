using System;

namespace BeatDown.Combat.Weapons
{
	public class Fists:BeatDown.Combat.Weapon
	{
		public Fists ()
		{
			this.damageType = DamageTypes.BLUNT;
			this.weaponType = WeaponType.MELEE;
		}
	}
}
