using NUnit.Framework;
using Quixo.Framework;
using System.Drawing;

namespace Quixo.Engine.Tests;

public static class EngineTests
{
	[Test]
	public static void UseGoodEngine()
	{
		using var writer = new StringWriter();
		var goodEngine = new RandomEngine(writer);

		var board = new Board();

		Assert.Multiple(() =>
		{
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.X), "The starting player is invalid."); 
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.O), "The player before the generated move is invalid.");

			using var cancel = new ManualResetEvent(false);
			var engineMove = goodEngine.GenerateMove(board, cancel);

			Assert.That(engineMove.Player, Is.EqualTo(Player.O), "The player after the generated move is invalid.");
			board.MovePiece(engineMove.Source, engineMove.Destination);

			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.X), "The player after the generated move was performed is invalid.");
		});
	}

	[Test]
	public static void UseBadNullEngine()
	{
		using var writer = new StringWriter();
		var badNullEngine = new BadNullTestEngine(writer);

		var board = new Board();

		Assert.Multiple(() =>
		{
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.X), "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.O), "The player before the generated move is invalid.");

			using var cancel = new ManualResetEvent(false);
			var engineMove = badNullEngine.GenerateMove(board, cancel);
			Assert.That(() => board.MovePiece(engineMove.Source, engineMove.Destination), Throws.TypeOf<NullReferenceException>());
		});
	}

	[Test]
	public static void UseBadMoveEngine()
	{
		using var writer = new StringWriter();
		var badMoveEngine = new BadMoveTestEngine(writer);

		var board = new Board();

		Assert.Multiple(() =>
		{
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.X), "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.That(board.CurrentPlayer, Is.EqualTo(Player.O), "The player before the generated move is invalid.");

			using var cancel = new ManualResetEvent(false);
			var engineMove = badMoveEngine.GenerateMove(board, cancel);
			Assert.That(() => board.MovePiece(engineMove.Source, engineMove.Destination), Throws.TypeOf<InvalidMoveException>());
		});
	}
}