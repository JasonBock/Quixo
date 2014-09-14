using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Threading;
using NUnit.Framework;
using Quixo.Framework;

namespace Quixo.Engine.Tests
{
	[TestFixture]
	public sealed class EngineTests
	{
		private ArrayList engines = null;

		[SetUp]
		public void SetUp()
		{
			this.engines = ConfigurationManager.GetSection("QuixoEngines") as ArrayList;
		}

		private IEngine GetEngine(string engineTypeName)
		{
			IEngine engineFound = null;

			foreach (Type engine in this.engines)
			{
				if (engine.FullName.Equals(engineTypeName) == true)
				{
					engineFound = (IEngine)Activator.CreateInstance(engine);
					break;
				}
			}

			return engineFound;
		}

		[Test]
		public void GetAllEngines()
		{
			Assert.IsNotNull(engines, "The engine list is null.");
			Assert.AreEqual(3, engines.Count, "The engine count is incorrect.");

			int i = 0;
			foreach (Type engine in engines)
			{
				Assert.IsNotNull(engine, string.Format("The engine at index {0} is null.", i));
			}
		}

		[Test]
		public void UseGoodEngine()
		{
			IEngine goodEngine = this.GetEngine("Quixo.Engine.RandomEngine");

			Board board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			Move engineMove = goodEngine.GenerateMove(board, new ManualResetEvent(false));

			Assert.AreEqual(Player.O, engineMove.Player, "The player after the generated move is invalid.");
			board.MovePiece(engineMove.Source, engineMove.Destination);

			Assert.AreEqual(Player.X, board.CurrentPlayer, "The player after the generated move was performed is invalid.");
		}

		[Test, ExpectedException(typeof(NullReferenceException))]
		public void UseBadNullEngine()
		{
			IEngine badNullEngine = this.GetEngine("Quixo.Engine.Tests.BadNullTestEngine");

			Board board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			Move engineMove = badNullEngine.GenerateMove(board, new ManualResetEvent(false));
			board.MovePiece(engineMove.Source, engineMove.Destination);
		}

		[Test, ExpectedException(typeof(InvalidMoveException))]
		public void UseBadMoveEngine()
		{
			IEngine badMoveEngine = this.GetEngine("Quixo.Engine.Tests.BadMoveTestEngine");

			Board board = new Board();
			Assert.AreEqual(Player.X, board.CurrentPlayer, "The starting player is invalid.");
			board.MovePiece(new Point(0, 0), new Point(0, 4));
			Assert.AreEqual(Player.O, board.CurrentPlayer, "The player before the generated move is invalid.");
			Move engineMove = badMoveEngine.GenerateMove(board, new ManualResetEvent(false));
			board.MovePiece(engineMove.Source, engineMove.Destination);
		}
	}
}
