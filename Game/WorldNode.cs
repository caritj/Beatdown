using System;
using System.Collections.Generic;
using BeatDown.Game.Pathfinding;

namespace BeatDown.Game
{
	public class WorldNode:BeatDown.Game.Pathfinding.INode
	{


		private bool isOpenList = false;
		private bool isClosedList = false;

		public int X { get; private set; }
		public int Y { get; private set; }
		public int Z { get; private set; }

		World owner;
		public int TotalCost { get { return MovementCost + EstimatedCost; } }
		public int MovementCost { get; private set; }
		public int EstimatedCost { get; private set; }

		private static int[] childXPos = new int[] { 0, -1, 1, 0, };
		private static int[] childYPos = new int[] { -1, 0, 0, 1, };

		public WorldNode (World w, int x, int y, int z)
		{
			X =x;
			Y=y;
			Z=z;
			owner =w;
		}
		
		bool INode.IsOpenList (IEnumerable<INode> openList)
		{
			return isOpenList;
		}

		void INode.SetOpenList (bool value)
		{
			isOpenList =value;
		}

		bool INode.IsClosedList (IEnumerable<INode> closedList)
		{
			return isClosedList;
		}

		void INode.SetClosedList (bool value)
		{
			isClosedList = value;
		}

		void INode.SetMovementCost (INode parent)
		{
			//allow climbing but make it expensive.
			int deltaY = (int)Math.Abs (parent.Y - this.Y);

			if (deltaY <= 0) {
				this.MovementCost = parent.MovementCost + 1;
			} else {
				if(deltaY >1){
					this.MovementCost = parent.MovementCost + 1000000;
				}
				else{
					this.MovementCost = parent.MovementCost +2;
				}
			}
		}

		void INode.SetEstimatedCost (INode goal)
		{

			this.EstimatedCost = Math.Abs(this.X - goal.X) + Math.Abs(this.Z - goal.Z);
		}

		bool INode.IsGoal (INode goal)
		{
			return goal.X == this.X &&this.Z==goal.Z;
		}





		INode INode.Parent {
			get;
			set; 
		}

		IEnumerable<INode> INode.Children {
			get
			{
				var children = new List<WorldNode>();

				for (int i = 0; i < childXPos.Length; i++)
				{
					// skip any nodes out of bounds.
					if (X + childXPos[i] >= BeatDown.Game.World.WORLD_SIZE || Z + childYPos[i] >= BeatDown.Game.World.WORLD_SIZE)
						continue;
					if (X + childXPos[i] < 0 || Z + childYPos[i] < 0)
						continue;
				//	Console.WriteLine ("C:"+(Z + childYPos[i])+","+(X + childXPos[i]));
					children.Add( owner.Heightmap[Z + childYPos[i],X + childXPos[i]]);
				}

				return children;
			}
		}
		

	}
}

