using System;
using BeatDown.Game.Planning;
using BeatDown.Game;
using System.Collections.Generic;
using BeatDown.Combat;

namespace BeatDown.Game
{
	[Serializable]
	public class Unit:Renderable
	{

		public Weapon Weapon = new BeatDown.Combat.Weapons.Fists();
		public Armor Armor = new BeatDown.Combat.Armors.None();


		public Decimal APCost_HorzMove{ get { return APCost_HorzMove; } }
		public Decimal APCost_VertMove{ get { return APCost_VertMove; } }


		protected int actionPoints =5;
		public int ActionPoints { get { return actionPoints; } }
		protected int maxActionPoints =5;
		public int MaxActionPoints { get { return maxActionPoints; } }

		public List<ITask> Plan = new List<ITask>();
		protected List<ITask> completedTasks  =new List<ITask>();

		protected string name;
		public string Name{ get { return this.name; } }

		protected int team;
		public int Team{ get { return team; } }

		protected int health =5;
		public int Health {get{ return health;}}

		protected int maxHealth =5;
		public int MaxHealth {get{ return maxHealth;}}

		protected int maxSlope;
		public int MaxSlope { get { return this.maxSlope; } }


		public Unit (string name, Coords coords, int team)
		{
			this.name= name;
			this.position = coords;
			this.team = team;
			this.maxSlope = 1;
		}

		public void EndTurn()
		{
		}

		public void StartTurn()
		{
			this.Plan = new List<ITask> ();
		}

		public decimal GetPathAPs(Path path)
		{
			Decimal APs = 0;

			Coords PrevCoords = this.Position;
			foreach (Coords c in path.Coords) {

				if (Math.Abs(PrevCoords.X - c.X) > 1 || Math.Abs(PrevCoords.Z - c.Z) > 1)
				{
					throw new ImpossiblePathException("Path is discontinuous.");
				}

				int slope = c.Z - PrevCoords.Z;
				if (slope > this.MaxSlope) {
					throw new ImpossiblePathException("Path is too steep.");
				}

				APs -= this.APCost_HorzMove;

				if (slope > 0) {
					APs -= this.APCost_VertMove * MaxSlope;
				}

				PrevCoords = c;
				   
			}

			return APs;
		}

		public Decimal GetTaskAPs(ITask task)
		{
			if (task is Path) {
				return this.GetPathAPs((Path)task);
			} else {
				throw new UnsupportedTaskType("No method implemented to calculate action points for task type '"+task.GetType().ToString()+"'");
			}
		}

		public Decimal GetRemainingAPs(ITask task)
		{
			Decimal APsAvailable = (Decimal)this.ActionPoints;
			foreach (ITask t in this.Plan)
			{
				APsAvailable -= GetTaskAPs (t);
			}

			return APsAvailable-GetTaskAPs(task);
		}

		public void AddTask (ITask task)
		{
				this.Plan.Add (task);

		}


		public void TakeDamage (int damage, Weapon.DamageTypes type)
		{
			int reducedDamage = Armor.GetReducedDamage(damage,type);
			//TODO this should del in damage types and perhaps arm type aty some points
			this.health -= reducedDamage;


		}
		public void TakeDamage (int damage)
		{
			this.TakeDamage(damage, BeatDown.Combat.Weapon.DamageTypes.GENERIC);
		}

		public bool CanAttack(Unit target){
			return 	this.Weapon.InRange(this.Position, target.Position);
		}	
		public bool DidHit (Unit target, Weapon w)
		{

			return Game.Instance.RandomNumberGenerator.NextDouble() <= w.HitPct;

		}


		public void Update (double time)
		{
			if (health <= 0) {
				Game.Instance.Manager.AddToRemovalQueue (this.glId);
			} else {
				//cehck teh plan
				//TODO SLOW DOWN for animation 
				if(this.Plan !=null && this.Plan.Count >0 && this.ActionPoints >0){

					ITask task = Plan[0].Execute(this.ActionPoints);
					this.actionPoints  -= task.GetCost();

					if(task is Movement){
						//TODO SLWOLY AND ANIMATE
						Movement m = ((Movement)task);
						if(task.GetCost() > 0){
							Coords dest = m.Path[m.Path.Count-1];
							this.MoveTo(dest, dest.Direction(this.Position));
						}
					}

					if(task is Attack){

					}
					if(task is Idle){

					}

					completedTasks.Add(task);

					if(this.Plan[0].GetCompleted()){
						this.Plan.RemoveAt(0);
					}

				}

			}
		}
		public void ExecutePlan ()
		{

		}

		



	}
}

