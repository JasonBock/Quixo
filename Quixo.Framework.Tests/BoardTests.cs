using System;
using System.Drawing;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
	[TestFixture]
	public sealed class BoardTests
	{
		private Board board = null;

		[SetUp]
		public void SetUp() => 
			this.board = new Board();

		[Test]
		public void CreateNewBoard()
		{
			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					Assert.AreEqual(Player.None, this.board.GetPiece(new Point(x, y)),
						 $"The player of the piece at ({x}, {y}) is incorrect.");
				}
			}
		}

		[Test]
		public void GetValidDestinationPieces()
		{
			var nonCornerPoint = new Point(2, 4);
			var cornerPoint = new Point(4, 4);
			var destinationPieces = this.board.GetValidDestinationPieces(nonCornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {nonCornerPoint} is null.");
			Assert.AreEqual(3, destinationPieces.Count,
				 $"The count for point {nonCornerPoint} is invalid.");

			destinationPieces = this.board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {cornerPoint} is null.");
			Assert.AreEqual(2, destinationPieces.Count,
				 $"The count for point {cornerPoint} is invalid.");

			this.board.MovePiece(nonCornerPoint, cornerPoint);

			destinationPieces = this.board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 $"The destination pieces collection for point {cornerPoint} (after the move) is null.");
			Assert.AreEqual(0, destinationPieces.Count,
				 $"The count for point {cornerPoint} (after the move) is invalid.");
		}

		[Test]
		public void GetValidPiecesToMove()
		{
			var validPieces = this.board.GetValidSourcePieces();

			Assert.AreEqual(16, validPieces.Count,
				 "The number of valid pieces to move is incorrect.");
		}

		[Test]
		public void RequestPieceAtInvalidPosition() => 
			Assert.Throws<IndexOutOfRangeException>(() => this.board.GetPiece(new Point(24, -16)));

		[Test]
		public void MakeOutOfTurnMove()
		{
			this.board.MovePiece(new Point(1, 0), new Point(1, 4));
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 4), new Point(1, 0)));
		}

		[Test]
		public void MovePieceToSameSourcePosition() => 
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 0), new Point(1, 0)));

		[Test]
		public void MovePieceToIncorrectDestination() => 
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 0), new Point(4, 2)));

		[Test]
		public void MoveInternalPiece() => 
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(3, 2), new Point(1, 0)));

		[Test]
		public void TryToMoveAfterAWin()
		{
			this.board.MovePiece(new Point(0, 0), new Point(0, 4));
			this.board.MovePiece(new Point(4, 0), new Point(4, 4));
			this.board.MovePiece(new Point(0, 0), new Point(0, 4));
			this.board.MovePiece(new Point(4, 0), new Point(4, 4));
			this.board.MovePiece(new Point(0, 0), new Point(0, 4));
			this.board.MovePiece(new Point(4, 0), new Point(4, 4));
			this.board.MovePiece(new Point(0, 0), new Point(0, 4));
			this.board.MovePiece(new Point(4, 0), new Point(4, 4));
			this.board.MovePiece(new Point(0, 0), new Point(0, 4));

			Assert.AreEqual(Player.X, this.board.WinningPlayer,
				 "The winning player is incorrect.");
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(2, 0), new Point(2, 4)));
		}

		[Test]
		[Ignore("Working on it...")]
		public void CauseOpponentToWin()
		{
			//  Create a game where a player's move causes the other to win.
		}
	}
}
