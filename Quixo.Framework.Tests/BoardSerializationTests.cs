using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
	[TestFixture]
	public sealed class BoardSerializationTests
	{
		[Test]
		public void RoundTripWithBinary()
		{
			IFormatter formatter = null;
			MemoryStream outStream = null, inStream = null;
			Board board = new Board(), newBoard = null;

			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));

			formatter = new BinaryFormatter();
			outStream = new MemoryStream();
			formatter.Serialize(outStream, board);
			inStream = new MemoryStream(outStream.ToArray());
			newBoard = (Board)formatter.Deserialize(inStream);

			Assert.IsNotNull(newBoard, "The new board is null.");
			Assert.AreEqual(5, newBoard.Moves.Count, "The move history is incorrect.");
			Assert.AreEqual(Player.O, newBoard.CurrentPlayer, "The current player is incorrect.");
			Assert.AreEqual(Player.None, newBoard.WinningPlayer, "The winning player is incorrect.");

			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));

			formatter = new BinaryFormatter();
			outStream = new MemoryStream();
			formatter.Serialize(outStream, board);
			inStream = new MemoryStream(outStream.ToArray());
			newBoard = (Board)formatter.Deserialize(inStream);

			Assert.AreEqual(9, newBoard.Moves.Count, "The move history (after the win) is incorrect.");
			Assert.AreEqual(Player.None, newBoard.CurrentPlayer, "The current player (after the win) is incorrect.");
			Assert.AreEqual(Player.X, newBoard.WinningPlayer, "The winning player (after the win) is incorrect.");
		}

		[Test]
		public void RoundTripWithCustomFormatter()
		{
			IFormatter formatter = null;
			MemoryStream outStream = null, inStream = null;
			Board board = new Board(), newBoard = null;

			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));

			formatter = new BoardFormatter();
			outStream = new MemoryStream();
			formatter.Serialize(outStream, board);
			inStream = new MemoryStream(outStream.ToArray());
			newBoard = (Board)formatter.Deserialize(inStream);

			Assert.IsNotNull(newBoard, "The new board is null.");
			Assert.AreEqual(5, newBoard.Moves.Count, "The move history is incorrect.");
			Assert.AreEqual(Player.O, newBoard.CurrentPlayer, "The current player is incorrect.");
			Assert.AreEqual(Player.None, newBoard.WinningPlayer, "The winning player is incorrect.");

			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));

			formatter = new BinaryFormatter();
			outStream = new MemoryStream();
			formatter.Serialize(outStream, board);
			inStream = new MemoryStream(outStream.ToArray());
			newBoard = (Board)formatter.Deserialize(inStream);

			Assert.AreEqual(9, newBoard.Moves.Count, "The move history (after the win) is incorrect.");
			Assert.AreEqual(Player.None, newBoard.CurrentPlayer, "The current player (after the win) is incorrect.");
			Assert.AreEqual(Player.X, newBoard.WinningPlayer, "The winning player (after the win) is incorrect.");
		}

		[Test]
		public void DeserializeWithValidMoves()
		{
			string boardData = "0,0:0,4|4,0:4,4|0,0:0,4";
			IFormatter formatter = new BoardFormatter();
			Stream stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

			Board board = formatter.Deserialize(stream) as Board;
			Assert.AreEqual(3, board.Moves.Count);
		}

		[Test]
		public void SerializeInvalidType()
		{
			string board = "0,0:0,4|4,0:4,4|0,0:0,4";
			IFormatter formatter = new BoardFormatter();
			Stream outStream = new MemoryStream();
			Assert.Throws<ArgumentException>(() => formatter.Serialize(outStream, board));
		}

		[Test]
		public void SerializeWithNullBoard()
		{
			Board board = null;
			IFormatter formatter = new BoardFormatter();
			Stream outStream = new MemoryStream();
			Assert.Throws<ArgumentNullException>(() => formatter.Serialize(outStream, board));
		}

		[Test]
		public void SerializeWithNullStream()
		{
			Board board = new Board();
			IFormatter formatter = new BoardFormatter();
			Stream outStream = null;
			Assert.Throws<ArgumentNullException>(() => formatter.Serialize(outStream, board));
		}

		[Test]
		public void DeserializeWithInvalidMoveInState()
		{
			string boardData = "0,0:0,4|4,6:4,4|0,0:0,4";
			IFormatter formatter = new BoardFormatter();
			Stream stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

			Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
		}

		[Test]
		public void DeserializeWithInvalidMoveDelimiter()
		{
			string boardData = "0,0:0,4|4,0:4,4!0,0:0,4";
			IFormatter formatter = new BoardFormatter();
			Stream stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

			Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
		}

		[Test]
		public void DeserializeWithInvalidCoordinateDelimiter()
		{
			string boardData = "0,0:0,4|4,0:4,4|0,0:0*4";
			IFormatter formatter = new BoardFormatter();
			Stream stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

			Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
		}

		[Test]
		public void DeserializeWithInvalidMovePairDelimiter()
		{
			string boardData = "0,0?0,4|4,0:4,4|0,0:0,4";
			IFormatter formatter = new BoardFormatter();
			Stream stream = new MemoryStream((new ASCIIEncoding()).GetBytes(boardData));

			Assert.Throws<SerializationException>(() => formatter.Deserialize(stream));
		}
	}
}
