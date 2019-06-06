using System.Drawing;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
	[TestFixture]
	public static class ValidGameTests
	{
		[Test]
		public static void CheckUndo()
		{
			var moves = 0;
			var board = new Board();
			var player = Player.X;
			var states = new Player[Board.Dimension, Board.Dimension];

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(0, 0), new Point(0, 4));
			moves++;

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 2), new Point(0, 2));
			moves++;

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(2, 0), new Point(2, 4));
			moves++;

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 0), new Point(0, 0));
			moves++;

			ValidGameTests.UpdateStates(states, 
				new Point[] { new Point(0, 4), new Point(2, 4) },
				new Point[] { new Point(0, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, moves);

			// Back to O's turn
			board.Undo();
			ValidGameTests.UpdateStates(states, 
				new Point[] { new Point(0, 4), new Point(2, 4) },
				new Point[] { new Point(0, 2) });
			ValidGameTests.Verify(board, states, Player.None, Player.O, --moves);

			// Back to O's turn
			board.Undo(2);
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4) }, null);
			moves -= 2;
			ValidGameTests.Verify(board, states, Player.None, Player.O, moves);

			// Back to the start.
			board.Undo();
			ValidGameTests.UpdateStates(states, null, null);
			ValidGameTests.Verify(board, states, Player.None, Player.X, --moves);

			// This should be quietly ignore.
			board.Undo();
			ValidGameTests.UpdateStates(states, null, null);
			ValidGameTests.Verify(board, states, Player.None, Player.X, 0);
		}

		[Test]
		public static void PlayValidGame()
		{
			var moves = 0;
			var board = new Board();
			var player = Player.X;
			var states = new Player[Board.Dimension, Board.Dimension];

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(0, 0), new Point(0, 4));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4) }, null);
			ValidGameTests.Verify(board, states, Player.None, Player.O, ++moves);

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 2), new Point(0, 2));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4) }, new Point[] { new Point(0, 2) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, ++moves);

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(2, 0), new Point(2, 4));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2) });
			ValidGameTests.Verify(board, states, Player.None, Player.O, ++moves);

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 0), new Point(0, 0));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(2, 4) },
				 new Point[] { new Point(0, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, ++moves);

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 4), new Point(0, 4));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4) },
				 new Point[] { new Point(0, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.O, ++moves);

			// 0
			ValidGameTests.MakeMove(board, ref player, states, new Point(0, 2), new Point(4, 2));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4) },
				 new Point[] { new Point(4, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, ++moves);

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(3, 0), new Point(3, 4));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(3, 3) },
				 new Point[] { new Point(4, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.O, ++moves);

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 3), new Point(0, 3));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 3) },
				 new Point[] { new Point(0, 3), new Point(4, 2), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, ++moves);

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 4), new Point(4, 0));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 4), new Point(4, 0) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.O, ++moves);

			// O
			ValidGameTests.MakeMove(board, ref player, states, new Point(4, 2), new Point(4, 0));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(3, 4), new Point(4, 4), new Point(4, 1) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0), new Point(4, 0) });
			ValidGameTests.Verify(board, states, Player.None, Player.X, ++moves);

			// X
			ValidGameTests.MakeMove(board, ref player, states, new Point(2, 4), new Point(0, 4));
			ValidGameTests.UpdateStates(states, new Point[] { new Point(0, 4), new Point(1, 4), new Point(2, 4), new Point(3, 4), new Point(4, 4), new Point(4, 1) },
				 new Point[] { new Point(0, 3), new Point(4, 3), new Point(0, 0), new Point(4, 0) });
			ValidGameTests.Verify(board, states, Player.X, Player.None, ++moves);
		}

		private static void Verify(Board board, Player[,] states, Player expectedWinner, Player currentPlayer, int moveHistoryCount)
		{
			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					Assert.AreEqual(states[x, y], board.GetPiece(new Point(x, y)),
						 $"The state of the piece at ({x}, {y}) is incorrect.");
				}
			}

			Assert.AreEqual(expectedWinner, board.WinningPlayer,
				 "The winning player is incorrect.");
			Assert.AreEqual(currentPlayer, board.CurrentPlayer,
				 "The current player is incorrect.");
			Assert.AreEqual(moveHistoryCount, board.Moves.Count,
				 "The move history count is incorrect.");
		}

		private static void UpdateStates(Player[,] states, Point[] xPieces, Point[] oPieces)
		{
			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					states[x, y] = Player.None;
				}
			}

			if (xPieces != null)
			{
				for (var x = 0; x < xPieces.Length; x++)
				{
					states[xPieces[x].X, xPieces[x].Y] = Player.X;
				}
			}

			if (oPieces != null)
			{
				for (var y = 0; y < oPieces.Length; y++)
				{
					states[oPieces[y].X, oPieces[y].Y] = Player.O;
				}
			}
		}

		private static void MakeMove(Board board, ref Player player, Player[,] states, Point source, Point destination)
		{
			Assert.AreEqual(player, board.CurrentPlayer,
				"The current player before the move was made is incorrect.");

			board.MovePiece(source, destination);

			states[destination.X, destination.Y] = player;

			if (board.WinningPlayer != Player.None)
			{
				player = Player.None;
			}
			else
			{
				if (player == Player.X)
				{
					player = Player.O;
				}
				else
				{
					player = Player.X;
				}
			}

			Assert.AreEqual(player, board.CurrentPlayer,
				"The current player after the move was made is incorrect.");
		}
	}
}
