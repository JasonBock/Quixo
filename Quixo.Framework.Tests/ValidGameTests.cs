using System.Drawing;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
	[TestFixture]
	public sealed class ValidGameTests
	{
		private Board board = null;
		private Player[,] states = new Player[Board.Dimension, Board.Dimension];
		private Player player = Player.X;

		[SetUp]
		public void SetUp()
		{
			this.board = new Board();
		}

		[Test]
		public void CheckUndo()
		{
			var moves = 0;
			// X
			this.MakeMove(new Point(0, 0), new Point(0, 4));
			moves++;

			// O
			this.MakeMove(new Point(4, 2), new Point(0, 2));
			moves++;

			// X
			this.MakeMove(new Point(2, 0), new Point(2, 4));
			moves++;

			// O
			this.MakeMove(new Point(4, 0), new Point(0, 0));
			moves++;

			this.UpdateStates(new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.X, moves);

			// Back to O's turn
			this.board.Undo();
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2) });
			this.Verify(Player.None, Player.O, --moves);

			// Back to O's turn
			this.board.Undo(2);
			this.UpdateStates(new Point[] { new Point(0, 4) },
				 null);
			moves -= 2;
			this.Verify(Player.None, Player.O, moves);

			// Back to the start.
			this.board.Undo();
			this.UpdateStates(null,
				 null);
			this.Verify(Player.None, Player.X, --moves);

			// This should be quietly ignore.
			this.board.Undo();
			this.UpdateStates(null,
				 null);
			this.Verify(Player.None, Player.X, 0);
		}

		[Test]
		public void PlayValidGame()
		{
			var moves = 0;

			// X
			this.MakeMove(new Point(0, 0), new Point(0, 4));
			this.UpdateStates(new Point[] { new Point(0, 4) },
				 null);
			this.Verify(Player.None, Player.O, ++moves);

			// O
			this.MakeMove(new Point(4, 2), new Point(0, 2));
			this.UpdateStates(new Point[] { new Point(0, 4) },
				 new Point[] { new Point(0, 2) });
			this.Verify(Player.None, Player.X, ++moves);

			// X
			this.MakeMove(new Point(2, 0), new Point(2, 4));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2) });
			this.Verify(Player.None, Player.O, ++moves);

			// O
			this.MakeMove(new Point(4, 0), new Point(0, 0));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.X, ++moves);

			// X
			this.MakeMove(new Point(4, 4), new Point(0, 4));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4) },
				 new Point[] { new Point(0, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.O, ++moves);

			// 0
			this.MakeMove(new Point(0, 2), new Point(4, 2));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4) },
				 new Point[] { new Point(4, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.X, ++moves);

			// X
			this.MakeMove(new Point(3, 0), new Point(3, 4));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(3, 3) },
				 new Point[] { new Point(4, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.O, ++moves);

			// O
			this.MakeMove(new Point(4, 3), new Point(0, 3));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 3) },
				 new Point[] { new Point(0, 3), new Point(4, 2), new Point(0, 0) });
			this.Verify(Player.None, Player.X, ++moves);

			// X
			this.MakeMove(new Point(4, 4), new Point(4, 0));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 4), new Point(4, 0) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0) });
			this.Verify(Player.None, Player.O, ++moves);

			// O
			this.MakeMove(new Point(4, 2), new Point(4, 0));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 4), new Point(4, 1) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0), new Point(4, 0) });
			this.Verify(Player.None, Player.X, ++moves);

			// X
			this.MakeMove(new Point(2, 4), new Point(0, 4));
			this.UpdateStates(new Point[] { new Point(0, 4), new Point(1, 4), new Point(2, 4), new Point(3, 4), new Point(4, 4), new Point(4, 1) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0), new Point(4, 0) });
			this.Verify(Player.X, Player.None, ++moves);
		}

		private void Verify(Player expectedWinner, Player currentPlayer, int moveHistoryCount)
		{
			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					Assert.AreEqual(this.states[x, y], board.GetPiece(new Point(x, y)),
						 string.Format("The state of the piece at ({0}, {1}) is incorrect.", x, y));
				}
			}

			Assert.AreEqual(expectedWinner, this.board.WinningPlayer,
				 "The winning player is incorrect.");
			Assert.AreEqual(currentPlayer, this.board.CurrentPlayer,
				 "The current player is incorrect.");
			Assert.AreEqual(moveHistoryCount, this.board.Moves.Count,
				 "The move history count is incorrect.");
		}

		private void UpdateStates(Point[] xPieces, Point[] oPieces)
		{
			int x = 0, y = 0;

			for (x = 0; x < Board.Dimension; x++)
			{
				for (y = 0; y < Board.Dimension; y++)
				{
					this.states[x, y] = Player.None;
				}
			}

			if (xPieces != null)
			{
				for (x = 0; x < xPieces.Length; x++)
				{
					this.states[xPieces[x].X, xPieces[x].Y] = Player.X;
				}
			}

			if (oPieces != null)
			{
				for (y = 0; y < oPieces.Length; y++)
				{
					this.states[oPieces[y].X, oPieces[y].Y] = Player.O;
				}
			}
		}

		private void MakeMove(Point source, Point destination)
		{
			Assert.AreEqual(this.player, this.board.CurrentPlayer,
				 "The current player before the move was made is incorrect.");

			this.board.MovePiece(source, destination);

			this.states[destination.X, destination.Y] = this.player;

			if (this.board.WinningPlayer != Player.None)
			{
				this.player = Player.None;
			}
			else
			{
				if (this.player == Player.X)
				{
					this.player = Player.O;
				}
				else
				{
					this.player = Player.X;
				}
			}

			Assert.AreEqual(this.player, this.board.CurrentPlayer,
				 "The current player after the move was made is incorrect.");
		}
	}
}
