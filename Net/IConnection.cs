using System;
using BeatDown.Game;

namespace BeatDown.Net
{

	public enum ConnectionStatus {Open, Closed}

	public interface IConnection
	{
		BeatDown.Game.Game CreateGame(string name, Settings settings);
		BeatDown.Game.Game JoinGame (Int64 id, Settings settings);
		IMessage Send(IMessage message);
	}
}

