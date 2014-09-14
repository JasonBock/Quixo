using System.Drawing;
using NUnit.Framework;

namespace Quixo.Framework.Tests
{
	[TestFixture]
	public sealed class MoveTests
	{
		[Test]
		public void GetBoardHistory()
		{
			var board = new Board();
			board.MovePiece(new Point(0, 2), new Point(4, 2));
			board.MovePiece(new Point(0, 1), new Point(4, 1));
			board.MovePiece(new Point(2, 0), new Point(2, 4));
			board.MovePiece(new Point(3, 4), new Point(3, 0));
			board.MovePiece(new Point(1, 4), new Point(4, 4));

			var history = board.Moves;

			Assert.IsNotNull(history, "The move history is null.");
			Assert.AreEqual(5, history.Count, "The history count is incorrect.");

			var move1 = history[0];
			Assert.AreEqual(Player.X, move1.Player, "The player for move 1 is incorrect.");
			Assert.AreEqual(new Point(0, 2), move1.Source, "The source point for move 1 is incorrect.");
			Assert.AreEqual(new Point(4, 2), move1.Destination, "The destination point for move 1 is incorrect.");

			var move2 = history[1];
			Assert.AreEqual(Player.O, move2.Player, "The player for move 2 is incorrect.");
			Assert.AreEqual(new Point(0, 1), move2.Source, "The source point for move 2 is incorrect.");
			Assert.AreEqual(new Point(4, 1), move2.Destination, "The destination point for move 2 is incorrect.");

			var move3 = history[2];
			Assert.AreEqual(Player.X, move3.Player, "The player for move 3 is incorrect.");
			Assert.AreEqual(new Point(2, 0), move3.Source, "The source point for move 3 is incorrect.");
			Assert.AreEqual(new Point(2, 4), move3.Destination, "The destination point for move 3 is incorrect.");

			var move4 = history[3];
			Assert.AreEqual(Player.O, move4.Player, "The player for move 4 is incorrect.");
			Assert.AreEqual(new Point(3, 4), move4.Source, "The source point for move 4 is incorrect.");
			Assert.AreEqual(new Point(3, 0), move4.Destination, "The destination point for move 4 is incorrect.");

			var move5 = history[4];
			Assert.AreEqual(Player.X, move5.Player, "The player for move 5 is incorrect.");
			Assert.AreEqual(new Point(1, 4), move5.Source, "The source point for move 5 is incorrect.");
			Assert.AreEqual(new Point(4, 4), move5.Destination, "The destination point for move 5 is incorrect.");
		}
	}
}
