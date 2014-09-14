using System;
using System.Drawing;

namespace Quixo.Framework
{
	/// <summary>
	/// This class represents a piece that is put on a Quixo <see cref="Board"/>.
	/// </summary>
	[Serializable]
	public sealed class Piece
	{
		private Player player = Player.None;
		private Point position = Point.Empty;

		private Piece() : base() { }

		public Piece(Point position, Player player)
			: this()
		{
			this.player = player;
			this.position = position;
		}

		/// <summary>
		/// Gets the location of the <see cref="Piece"/>.
		/// </summary>
		public Point Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>
		/// Gets the current player of the <see cref="Piece"/>.
		/// </summary>
		public Player Player
		{
			get
			{
				return this.player;
			}
		}
	}
}
