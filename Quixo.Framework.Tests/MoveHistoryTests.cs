using NUnit.Framework;

namespace Quixo.Framework.Tests;

public static class MoveTests
{
	[Test]
	public static void GetBoardHistory()
	{
		var board = new Board();
		board.MovePiece(new Point(0, 2), new Point(4, 2));
		board.MovePiece(new Point(0, 1), new Point(4, 1));
		board.MovePiece(new Point(2, 0), new Point(2, 4));
		board.MovePiece(new Point(3, 4), new Point(3, 0));
		board.MovePiece(new Point(1, 4), new Point(4, 4));

		var history = board.Moves;

		Assert.Multiple(() =>
		{
			Assert.That(history, Is.Not.Null, "The move history is null.");
			Assert.That(history.Count, Is.EqualTo(5), "The history count is incorrect.");

			var move1 = history[0];
			Assert.That(move1.Player, Is.EqualTo(Player.X), "The player for move 1 is incorrect.");
			Assert.That(move1.Source, Is.EqualTo(new Point(0, 2)), "The source point for move 1 is incorrect.");
			Assert.That(move1.Destination, Is.EqualTo(new Point(4, 2)), "The destination point for move 1 is incorrect.");

			var move2 = history[1];
			Assert.That(move2.Player, Is.EqualTo(Player.O), "The player for move 2 is incorrect.");
			Assert.That(move2.Source, Is.EqualTo(new Point(0, 1)), "The source point for move 2 is incorrect.");
			Assert.That(move2.Destination, Is.EqualTo(new Point(4, 1)), "The destination point for move 2 is incorrect.");

			var move3 = history[2];
			Assert.That(move3.Player, Is.EqualTo(Player.X), "The player for move 3 is incorrect.");
			Assert.That(move3.Source, Is.EqualTo(new Point(2, 0)), "The source point for move 3 is incorrect.");
			Assert.That(move3.Destination, Is.EqualTo(new Point(2, 4)), "The destination point for move 3 is incorrect.");

			var move4 = history[3];
			Assert.That(move4.Player, Is.EqualTo(Player.O), "The player for move 4 is incorrect.");
			Assert.That(move4.Source, Is.EqualTo(new Point(3, 4)), "The source point for move 4 is incorrect.");
			Assert.That(move4.Destination, Is.EqualTo(new Point(3, 0)), "The destination point for move 4 is incorrect.");

			var move5 = history[4];
			Assert.That(move5.Player, Is.EqualTo(Player.X), "The player for move 5 is incorrect.");
			Assert.That(move5.Source, Is.EqualTo(new Point(1, 4)), "The source point for move 5 is incorrect.");
			Assert.That(move5.Destination, Is.EqualTo(new Point(4, 4)), "The destination point for move 5 is incorrect.");
		});
	}
}