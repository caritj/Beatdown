using System;
using BeatDown.Game;

namespace BeatDown.Net
{

	public enum ConnectionStatus {Open, Closed}

	public interface IConnection
	{
		ConnectionStatus Status { get; }
		BeatDown.Game.Game CreateGame(string name);
		BeatDown.Game.Game JoinGame (Int64 id);
		IMessage Send(IMessage message);
	}
}

