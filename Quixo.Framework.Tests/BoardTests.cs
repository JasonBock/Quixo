using System.Drawing;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
   [TestFixture]
	public static class BoardTests
	{
		[Test]
		public static void CreateNewBoard()
		{
			var board = new Board();

			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					Assert.AreEqual(Player.None, board.GetPiece(new Point(x, y)),
						 $"The player of the piece at ({x}, {y}) is incorrect.");
				}
			}
		}

		[Test]
		public static void GetValidDestinationPieces()
		{
			var board = new Board();
			var nonCornerPoint = new Point(2, 4);
			var cornerPoint = new Point(4, 4);
			var destinationPieces = board.GetValidDestinationPieces(nonCornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {nonCornerPoint} is null.");
			Assert.AreEqual(3, destinationPieces.Count,
				 $"The count for point {nonCornerPoint} is invalid.");

			destinationPieces = board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {cornerPoint} is null.");
			Assert.AreEqual(2, destinationPieces.Count,
				 $"The count for point {cornerPoint} is invalid.");

			board.MovePiece(nonCornerPoint, cornerPoint);

			destinationPieces = board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {cornerPoint} (after the move) is null.");
			Assert.AreEqual(0, destinationPieces.Count,
				 $"The count for point {cornerPoint} (after the move) is invalid.");
		}

		[Test]
		public static void GetValidPiecesToMove()
		{
			var validPieces = new Board().GetValidSourcePieces();

			Assert.AreEqual(16, validPieces.Count,
				 "The number of valid pieces to move is incorrect.");
		}

		[Test]
		public static void RequestPieceAtInvalidPosition() => 
			Assert.Throws<ArgumentOutOfRangeException>(() => new Board().GetPiece(new Point(24, -16)));

		[Test]
		public static void MakeOutOfTurnMove()
		{
			var board = new Board();
			board.MovePiece(new Point(1, 0), new Point(1, 4));
			Assert.Throws<InvalidMoveException>(() => board.MovePiece(new Point(1, 4), new Point(1, 0)));
		}

		[Test]
		public static void MovePieceToSameSourcePosition() => 
			Assert.Throws<InvalidMoveException>(() => new Board().MovePiece(new Point(1, 0), new Point(1, 0)));

		[Test]
		public static void MovePieceToIncorrectDestination() => 
			Assert.Throws<InvalidMoveException>(() => new Board().MovePiece(new Point(1, 0), new Point(4, 2)));

		[Test]
		public static void MoveInternalPiece() => 
			Assert.Throws<InvalidMoveException>(() => new Board().MovePiece(new Point(3, 2), new Point(1, 0)));

		[Test]
		public static void TryToMoveAfterAWin()
		{
			var board = new Board();
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			board.MovePiece(new Point(4, 0), new Point(4, 4));
			board.MovePiece(new Point(0, 0), new Point(0, 4));

			Assert.AreEqual(Player.X, board.WinningPlayer,
				 "The winning player is incorrect.");
			Assert.Throws<InvalidMoveException>(() => board.MovePiece(new Point(2, 0), new Point(2, 4)));
		}

		[Test]
		[Ignore("Working on it...")]
		public static void CauseOpponentToWin()
		{
			//  Create a game where a player's move causes the other to win.
		}
	}
}
