using System;
using Game.Planning;
using System.Collections.Generic;

namespace BeatDown.Game
{
	public class Unit:Renderable
	{
		int team = 0;
		public int Team{ get { return team; } }

		public Decimal APCost_HorzMove{ get { return APCost_HorzMove; } }
		public Decimal APCost_VertMove{ get { return APCost_VertMove; } }

		public int MaxSlope;

		public int ActionPoints { get { return ActionPoints; } }

		public Coords Position;
		public List<ITask> Plan;

		public Unit ()
		{
			color = System.Drawing.Color.Aquamarine;
			this.Position = new Coords(1,1,1);
			glId =1;
			this.MaxSlope = 1;
		}
		public Unit (Coords coords, int Team)
		{
			color = System.Drawing.Color.Crimson;
			this.Position = coords;
			team = Team;
			glId =2;
			this.MaxSlope = 1;
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
			if (GetRemainingAPs (task) > 0) {
				this.Plan.Add (task);
			} else {
				throw new OutOfAtionPointsException ("Not enough action points remaining to add this task.");
			}
		}

	}
}

