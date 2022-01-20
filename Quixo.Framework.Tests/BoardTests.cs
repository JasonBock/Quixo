using NUnit.Framework;
using System.Drawing;

namespace Quixo.Framework.Tests
{
	public static class BoardTests
	{
		[Test]
		public static void CreateNewBoard()
		{
			var board = new Board();

			Assert.Multiple(() =>
			{
				for (var x = 0; x < Board.Dimension; x++)
				{
					for (var y = 0; y < Board.Dimension; y++)
					{
						Assert.That(board.GetPiece(new Point(x, y)), Is.EqualTo(Player.None),
							$"The player of the piece at ({x}, {y}) is incorrect.");
					}
				}
			});
		}

		[Test]
		public static void GetValidDestinationPieces()
		{
			var board = new Board();
			var nonCornerPoint = new Point(2, 4);
			var cornerPoint = new Point(4, 4);
			var destinationPieces = board.GetValidDestinationPieces(nonCornerPoint);

			Assert.Multiple(() =>
			{
				Assert.That(destinationPieces, Is.Not.Null,
					$"The destination pieces collection for point {nonCornerPoint} is null.");
				Assert.That(destinationPieces.Count, Is.EqualTo(3),
					$"The count for point {nonCornerPoint} is invalid.");

				destinationPieces = board.GetValidDestinationPieces(cornerPoint);

				Assert.That(destinationPieces, Is.Not.Null,
					$"The destination pieces collection for point {cornerPoint} is null.");
				Assert.That(destinationPieces.Count, Is.EqualTo(2),
					$"The count for point {cornerPoint} is invalid.");

				board.MovePiece(nonCornerPoint, cornerPoint);

				destinationPieces = board.GetValidDestinationPieces(cornerPoint);

				Assert.That(destinationPieces, Is.Not.Null,
					$"The destination pieces collection for point {cornerPoint} (after the move) is null.");
				Assert.That(destinationPieces.Count, Is.EqualTo(0),
					$"The count for point {cornerPoint} (after the move) is invalid.");
			});
		}

		[Test]
		public static void GetValidPiecesToMove()
		{
			var validPieces = new Board().GetValidSourcePieces();

			Assert.That(validPieces.Count, Is.EqualTo(16),
				"The number of valid pieces to move is incorrect.");
		}

		[Test]
		public static void RequestPieceAtInvalidPosition() =>
			Assert.That(() => new Board().GetPiece(new Point(24, -16)), Throws.TypeOf<ArgumentOutOfRangeException>());

		[Test]
		public static void MakeOutOfTurnMove()
		{
			var board = new Board();
			board.MovePiece(new Point(1, 0), new Point(1, 4));
			Assert.That(() => board.MovePiece(new Point(1, 4), new Point(1, 0)), Throws.TypeOf<InvalidMoveException>());
		}

		[Test]
		public static void MovePieceToSameSourcePosition() =>
			Assert.That(() => new Board().MovePiece(new Point(1, 0), new Point(1, 0)), Throws.TypeOf<InvalidMoveException>());

		[Test]
		public static void MovePieceToIncorrectDestination() =>
			Assert.That(() => new Board().MovePiece(new Point(1, 0), new Point(4, 2)), Throws.TypeOf<InvalidMoveException>());

		[Test]
		public static void MoveInternalPiece() =>
			Assert.That(() => new Board().MovePiece(new Point(3, 2), new Point(1, 0)), Throws.TypeOf<InvalidMoveException>());

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

			Assert.Multiple(() =>
			{
				Assert.That(board.WinningPlayer, Is.EqualTo(Player.X),
					"The winning player is incorrect.");
				Assert.That(() => board.MovePiece(new Point(2, 0), new Point(2, 4)), Throws.TypeOf<InvalidMoveException>());
			});
		}

		[Test]
		[Ignore("Working on it...")]
		public static void CauseOpponentToWin()
		{
			//  Create a game where a player's move causes the other to win.
		}
	}
}