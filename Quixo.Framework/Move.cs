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
		private readonly Player player = Player.None;
		private Point source = Point.Empty;
		private Point destination = Point.Empty;

		public Move(Player player, Point source, Point destination) =>
			(this.player, this.source, this.destination) = (player, source, destination);

		public string Print() => $"Player {this.player}: {this.source} to {this.destination}";

		public Point Destination => this.destination;

		public Player Player => this.player;

		public Point Source => this.source;
	}
}