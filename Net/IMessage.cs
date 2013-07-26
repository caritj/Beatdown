using System;
using System.Runtime.Serialization;

namespace BeatDown.Net
{

	public static class MessageDecoder
	{
		public static IMessage Decode(string message)
		{
			return new TestMessage ();
		}
	}

	public interface IMessage:ISerializable
	{
	}

	public class TestMessage:IMessage
	{
		public TestMessage()
		{
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("TestValue", "TEST!", typeof(string));

		}

	}
}

