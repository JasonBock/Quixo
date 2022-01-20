using System.Drawing;

namespace Quixo.Framework;

/// <summary>
/// A collection of <see cref="Piece"/> objects.
/// </summary>
public sealed class PieceCollection
	: List<Piece>
{
	public bool Contains(Point position)
	{
		var hasPieceAtPosition = false;

		foreach (var piece in this)
		{
			if (piece.Position.Equals(position))
			{
				hasPieceAtPosition = true;
				break;
			}
		}

		return hasPieceAtPosition;
	}
}