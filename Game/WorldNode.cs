using System;
using System.Collections.Generic;
using BeatDown.Game.AStar;

namespace BeatDown.Game
{
	public class WorldNode:BeatDown.Game.AStar.INode
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
		
		bool BeatDown.Game.AStar.INode.IsOpenList (IEnumerable<BeatDown.Game.AStar.INode> openList)
		{
			return isOpenList;
		}

		void BeatDown.Game.AStar.INode.SetOpenList (bool value)
		{
			isOpenList =value;
		}

		bool BeatDown.Game.AStar.INode.IsClosedList (IEnumerable<BeatDown.Game.AStar.INode> closedList)
		{
			return isClosedList;
		}

		void BeatDown.Game.AStar.INode.SetClosedList (bool value)
		{
			isClosedList = value;
		}

		void BeatDown.Game.AStar.INode.SetMovementCost (BeatDown.Game.AStar.INode parent)
		{
			//allow climbing but make it expensive.
			int deltaY = parent.Y - this.Y;

			if (deltaY <= 0) {
				this.MovementCost = parent.MovementCost + 1;
			} else {
				if(deltaY >1){
					this.MovementCost = parent.MovementCost +500;
				}
				else{
					this.MovementCost = parent.MovementCost +2;
				}
			}
		}

		void BeatDown.Game.AStar.INode.SetEstimatedCost (BeatDown.Game.AStar.INode goal)
		{

			this.EstimatedCost = Math.Abs(this.X - goal.X) + Math.Abs(this.Z - goal.Z);
		}

		bool BeatDown.Game.AStar.INode.IsGoal (BeatDown.Game.AStar.INode goal)
		{
			return goal.X == this.X &&this.Z==goal.Z;
		}





		BeatDown.Game.AStar.INode INode.Parent {
			get;
			set; 
		}

		IEnumerable<BeatDown.Game.AStar.INode> INode.Children {
			get
			{
				var children = new List<WorldNode>();

				for (int i = 0; i < childXPos.Length; i++)
				{
					// skip any nodes out of bounds.
					if (X + childXPos[i] >= BeatDown.Game.World.WORLD_SIZE || Y + childYPos[i] >= BeatDown.Game.World.WORLD_SIZE)
						continue;
					if (X + childXPos[i] < 0 || Y + childYPos[i] < 0)
						continue;

					children.Add( owner.Heightmap[Z + childYPos[i],X + childXPos[i]]);
				}

				return children;
			}
		}
		

	}
}

