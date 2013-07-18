using System;
using System.Runtime.Serialization;

namespace BeatDown.Net
{

	public IMessage Decode(string message)
	{
	}

	public interface IMessage:ISerializable
	{
	}
}

