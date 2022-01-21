namespace Quixo.Framework;

/// <summary>
/// This class represents a piece that is put on a Quixo <see cref="Board"/>.
/// </summary>
[Serializable]
public sealed class Piece
{
	public Piece(Point position, Player player) =>
		(this.Position, this.Player) = (position, player);

	/// <summary>
	/// Gets the location of the <see cref="Piece"/>.
	/// </summary>
	public Point Position { get; }

	/// <summary>
	/// Gets the current player of the <see cref="Piece"/>.
	/// </summary>
	public Player Player { get; }
}