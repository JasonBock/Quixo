using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;

namespace Quixo.Framework.Tests;

public static class BoardSerializationTests
{
	[Test]
	public static void RoundTripWithCustomFormatter()
	{
		var board = new Board();

		board.MovePiece(new Point(0, 0), new Point(0, 4));
		board.MovePiece(new Point(4, 0), new Point(4, 4));
		board.MovePiece(new Point(0, 0), new Point(0, 4));
		board.MovePiece(new Point(4, 0), new Point(4, 4));
		board.MovePiece(new Point(0, 0), new Point(0, 4));

		using var outStream = new MemoryStream();
		BoardFormatter.Serialize(outStream, board);
		using var inStream = new MemoryStream(outStream.ToArray());
		var newBoard = BoardFormatter.Deserialize(inStream);

		Assert.Multiple(() =>
		{
			Assert.That(newBoard, Is.Not.Null, "The new board is null.");
			Assert.That(newBoard.Moves.Count, Is.EqualTo(5), "The move history is incorrect.");
			Assert.That(newBoard.CurrentPlayer, Is.EqualTo(Player.O), "The current player is incorrect.");
			Assert.That(newBoard.WinningPlayer, Is.EqualTo(Player.None), "The winning player is incorrect.");
		});
	}

	[Test]
	public static void DeserializeWithValidMoves()
	{
		var boardData = "0,0:0,4|4,0:4,4|0,0:0,4";
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		var board = BoardFormatter.Deserialize(stream);
		Assert.That(board.Moves.Count, Is.EqualTo(3));
	}

	[Test]
	public static void SerializeWithNullBoard() => 
		Assert.That(() => BoardFormatter.Serialize(new MemoryStream(), null!), Throws.TypeOf<ArgumentNullException>());

	[Test]
	public static void SerializeWithNullStream()
	{
		var board = new Board();
		Assert.That(() => BoardFormatter.Serialize(null!, board), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void DeserializeWithInvalidMoveInState()
	{
		var boardData = "0,0:0,4|4,6:4,4|0,0:0,4";
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.That(() => BoardFormatter.Deserialize(stream), Throws.TypeOf<ArgumentOutOfRangeException>());
	}

	[Test]
	public static void DeserializeWithInvalidMoveDelimiter()
	{
		var boardData = "0,0:0,4|4,0:4,4!0,0:0,4";
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.That(() => BoardFormatter.Deserialize(stream), Throws.TypeOf<SerializationException>());
	}

	[Test]
	public static void DeserializeWithInvalidCoordinateDelimiter()
	{
		var boardData = "0,0:0,4|4,0:4,4|0,0:0*4";
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.That(() => BoardFormatter.Deserialize(stream), Throws.TypeOf<SerializationException>());
	}

	[Test]
	public static void DeserializeWithInvalidMovePairDelimiter()
	{
		var boardData = "0,0?0,4|4,0:4,4|0,0:0,4";
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.That(() => BoardFormatter.Deserialize(stream), Throws.TypeOf<SerializationException>());
	}
}