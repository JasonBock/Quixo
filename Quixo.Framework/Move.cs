using System;
using System.Drawing;

namespace Quixo.Framework
{
	/// <summary>
	/// This class represents a move within a Quixo game.
	/// </summary>
	[Serializable]
	public sealed class Move
	{
		private const string PassFormat = "Player {0}: Passed";
		private const string MoveFormat = "Player {0}: {1} to {2}";

		private Player player = Player.None;
		private Point source = Point.Empty;
		private Point destination = Point.Empty;

		private Move() : base() { }

		public Move(Player player, Point source, Point destination)
		{
			this.player = player;
			this.source = source;
			this.destination = destination;
		}

		public string Print() => string.Format(MoveFormat, this.player, this.source, this.destination);

		public Point Destination => this.destination;

		public Player Player => this.player;

		public Point Source => this.source;
	}
}