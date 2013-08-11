using System;
using System.Collections.Generic;

namespace BeatDown.Game.Planning
{

	public interface ITask
	{
		int GetCost();
		ITask Execute(int ActionPoints);
		bool GetCompleted();

	}

	public class Path:ITask
	{
		public List<Coords> Coords;
		public Path()
		{
			this.Coords = new List<Coords>();
		}

		public void AddCoords(Coords c)
		{
			this.Coords = new List<Coords>();
			this.Coords.Add (c);
		}		#region ITask implementation
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
		#endregion


	}

	public class ImpossibleTaskException:Exception
	{
		public ImpossibleTaskException(string message): base(message)
		{
		}
	}

	public class UnsupportedTaskType:ImpossibleTaskException
	{
		public UnsupportedTaskType(string message): base(message)
		{
		}
	}

	public class ImpossiblePathException:ImpossibleTaskException
	{
		public ImpossiblePathException(string message): base(message)
		{
		}
	}

	public class OutOfAtionPointsException:ImpossibleTaskException
	{
		public OutOfAtionPointsException(string message): base(message)
		{
		}
	}



}

