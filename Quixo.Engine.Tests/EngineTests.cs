using System;
using System.Drawing;
using System.Threading;
using NUnit.Framework;
using Quixo.Framework;

namespace Quixo.Engine.Tests
{
	[TestFixture]
	public sealed class EngineTests
	{
		[Test]
		public void UseGoodEngine()
		{
			var goodEngine = new RandomEngine();

			var board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			var engineMove = goodEngine.GenerateMove(board, new ManualResetEvent(false));

			Assert.AreEqual(Player.O, engineMove.Player, "The player after the generated move is invalid.");
			board.MovePiece(engineMove.Source, engineMove.Destination);

			Assert.AreEqual(Player.X, board.CurrentPlayer, "The player after the generated move was performed is invalid.");
		}

		[Test]
		public void UseBadNullEngine()
		{
			var badNullEngine = new BadNullTestEngine();

			var board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			var engineMove = badNullEngine.GenerateMove(board, new ManualResetEvent(false));
			Assert.That(() => board.MovePiece(engineMove.Source, engineMove.Destination), Throws.TypeOf<NullReferenceException>());
		}

		[Test]
		public void UseBadMoveEngine()
		{
			var badMoveEngine = new BadMoveTestEngine();

			var board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			var engineMove = badMoveEngine.GenerateMove(board, new ManualResetEvent(false));
			Assert.That(() => board.MovePiece(engineMove.Source, engineMove.Destination), Throws.TypeOf<InvalidMoveException>());
		}
	}
}
