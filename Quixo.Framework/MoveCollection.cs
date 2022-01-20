using System.Text;

namespace Quixo.Framework;

/// <summary>
/// A collection of <see cref="Move"/> objects.
/// </summary>
[Serializable]
public class MoveCollection
	: List<Move>, ICloneable
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MoveCollection"/> class
	/// </summary>
	public MoveCollection() : base() { }

	/// <summary>
	/// Gets a printable version of the move history.
	/// </summary>
	/// <returns>The complete history of the game.</returns>
	public string Print()
	{
		var history = new StringBuilder();

		foreach (var moveHistory in this)
		{
			history.AppendLine(moveHistory.Print());
		}

		return history.ToString();
	}

	public object Clone()
	{
		var moves = new MoveCollection();

		foreach (var move in this)
		{
			moves.Add(move);
		}

		return moves;
	}
}