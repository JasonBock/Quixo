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
		public void SetUp()
		{
			this.board = new Board();
		}

		[Test]
		public void CreateNewBoard()
		{
			for (int x = 0; x < Board.Dimension; x++)
			{
				for (int y = 0; y < Board.Dimension; y++)
				{
					Assert.AreEqual(Player.None, this.board.GetPiece(new Point(x, y)),
						 string.Format("The player of the piece at ({0}, {1}) is incorrect.", x, y));
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
				 string.Format("The destination pieces collection for point {0} is null.", nonCornerPoint));
			Assert.AreEqual(3, destinationPieces.Count,
				 string.Format("The count for point {0} is invalid.", nonCornerPoint));

			destinationPieces = this.board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 string.Format("The destination pieces collection for point {0} is null.", cornerPoint));
			Assert.AreEqual(2, destinationPieces.Count,
				 string.Format("The count for point {0} is invalid.", cornerPoint));

			this.board.MovePiece(nonCornerPoint, cornerPoint);

			destinationPieces = this.board.GetValidDestinationPieces(cornerPoint);

			Assert.IsNotNull(destinationPieces,
				 string.Format("The destination pieces collection for point {0} (after the move) is null.", cornerPoint));
			Assert.AreEqual(0, destinationPieces.Count,
				 string.Format("The count for point {0} (after the move) is invalid.", cornerPoint));
		}

		[Test]
		public void GetValidPiecesToMove()
		{
			var validPieces = this.board.GetValidSourcePieces();

			Assert.AreEqual(16, validPieces.Count,
				 "The number of valid pieces to move is incorrect.");
		}

		[Test]
		public void RequestPieceAtInvalidPosition()
		{
			Assert.Throws<IndexOutOfRangeException>(() => this.board.GetPiece(new Point(24, -16)));
		}

		[Test]
		public void MakeOutOfTurnMove()
		{
			this.board.MovePiece(new Point(1, 0), new Point(1, 4));
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 4), new Point(1, 0)));
		}

		[Test]
		public void MovePieceToSameSourcePosition()
		{
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 0), new Point(1, 0)));
		}

		[Test]
		public void MovePieceToIncorrectDestination()
		{
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(1, 0), new Point(4, 2)));
		}

		[Test]
		public void MoveInternalPiece()
		{
			Assert.Throws<InvalidMoveException>(() => this.board.MovePiece(new Point(3, 2), new Point(1, 0)));
		}

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
