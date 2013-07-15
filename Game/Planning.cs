using System;
using System.Collections.Generic;

namespace Game.Planning
{

	public interface ITask
	{
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
		}

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

	public class Coords{
		public int X;
		public int Y;
		public int Z;

		public Coords(int x,  int y, int z){
			X=x;
			Y=y;
			Z=z;
		}
	}

}

