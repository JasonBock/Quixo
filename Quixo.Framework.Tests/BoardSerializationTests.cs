using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;

namespace Quixo.Framework.Tests;

[TestFixture]
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

		var boardFormatter = new BoardFormatter();
		using var outStream = new MemoryStream();
		boardFormatter.Serialize(outStream, board);
		using var inStream = new MemoryStream(outStream.ToArray());
		var newBoard = (Board)boardFormatter.Deserialize(inStream);

		Assert.IsNotNull(newBoard, "The new board is null.");
		Assert.AreEqual(5, newBoard.Moves.Count, "The move history is incorrect.");
		Assert.AreEqual(Player.O, newBoard.CurrentPlayer, "The current player is incorrect.");
		Assert.AreEqual(Player.None, newBoard.WinningPlayer, "The winning player is incorrect.");
	}

	[Test]
	public static void DeserializeWithValidMoves()
	{
		var boardData = "0,0:0,4|4,0:4,4|0,0:0,4";
		var formatter = new BoardFormatter();
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		var board = (Board)formatter.Deserialize(stream);
		Assert.AreEqual(3, board.Moves.Count);
	}

	[Test]
	public static void SerializeInvalidType()
	{
		var board = "0,0:0,4|4,0:4,4|0,0:0,4";
		var formatter = new BoardFormatter();
		Assert.Throws<ArgumentException>(() => formatter.Serialize(new MemoryStream(), board));
	}

	[Test]
	public static void SerializeWithNullBoard()
	{
		var formatter = new BoardFormatter();
		Assert.Throws<ArgumentNullException>(() => formatter.Serialize(new MemoryStream(), null!));
	}

	[Test]
	public static void SerializeWithNullStream()
	{
		var board = new Board();
		var formatter = new BoardFormatter();
		Assert.Throws<ArgumentNullException>(() => formatter.Serialize(null!, board));
	}

	[Test]
	public static void DeserializeWithInvalidMoveInState()
	{
		var boardData = "0,0:0,4|4,6:4,4|0,0:0,4";
		var formatter = new BoardFormatter();
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.Throws<ArgumentOutOfRangeException>(() => formatter.Deserialize(stream));
	}

	[Test]
	public static void DeserializeWithInvalidMoveDelimiter()
	{
		var boardData = "0,0:0,4|4,0:4,4!0,0:0,4";
		var formatter = new BoardFormatter();
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
	}

	[Test]
	public static void DeserializeWithInvalidCoordinateDelimiter()
	{
		var boardData = "0,0:0,4|4,0:4,4|0,0:0*4";
		var formatter = new BoardFormatter();
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
	}

	[Test]
	public static void DeserializeWithInvalidMovePairDelimiter()
	{
		var boardData = "0,0?0,4|4,0:4,4|0,0:0,4";
		var formatter = new BoardFormatter();
		using var stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

		Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
	}
}