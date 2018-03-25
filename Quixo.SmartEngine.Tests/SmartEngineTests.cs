using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using NUnit.Framework;
using Quixo.Framework;

namespace Quixo.SmartEngine.Tests
{
	[TestFixture]
	public sealed class SmartEngineTests
	{
		public SmartEngineTests() : base() { }

		[Test]
		public void NextMoveShouldNotCauseCrash()
		{
			Board board = null;

			using (var stream = new FileStream("ComputerAsOWillCrashOnNextMove.quixo", FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				board = (Board)formatter.Deserialize(stream);
			}

			//  The next move causes a crash!
			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));
			board.MovePiece(nextMove.Source, nextMove.Destination);
		}

		[Test]
		public void CheckDoesNotPreventLoss()
		{
			Board board = null;

			using (var stream = new FileStream("EngineDoesntPreventLoss.quixo", FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				board = (Board)formatter.Deserialize(stream);
			}

			board.Undo(4);

			//  At move #23, why doesn't O respond with {0, 1} to {4, 1}?
			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));

			Assert.AreEqual(new Point(0, 1), nextMove.Source, "The source is invalid.");
			Assert.AreEqual(new Point(4, 1), nextMove.Destination, "The destination is invalid.");
		}

		[Test]
		public void CheckMissedWinningMove()
		{
			Board board = null;

			using (var stream = new FileStream("HereEngineMissedWinningMove.quixo", FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				board = (Board)formatter.Deserialize(stream);
			}

			board.Undo();

			// For some reason, 0.2.0.3 doesn't catch that (1, 4) to (1, 0) would win...
			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));

			Assert.AreEqual(new Point(1, 4), nextMove.Source, "The source is invalid.");
			Assert.AreEqual(new Point(1, 0), nextMove.Destination, "The destination is invalid.");
		}

		[Test]
		public void CheckBadMove0202()
		{
			Board board = null;

			using (var stream = new FileStream("BadMove0.2.0.2.quixo", FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				board = (Board)formatter.Deserialize(stream);
			}

			board.Undo(2);

			// For some reason, 0.2.0.2 thinks that every next move is a losing move... :S
			// and I think it's right - it's in a position that every move would lead to a losing move.
			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));

			Assert.IsFalse((nextMove.Source == new Point(2, 0) && nextMove.Destination == new Point(2, 4)), "The next move is invalid.");
		}

		[Test]
		public void CheckBadMove0201()
		{
			Board board = null;

			using (var stream = new FileStream("BadMove0.2.0.1.quixo", FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				board = (Board)formatter.Deserialize(stream);
			}

			board.Undo(2);

			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));

			Assert.IsFalse((nextMove.Source == new Point(1, 0) && nextMove.Destination == new Point(0, 0)), "The next move is invalid.");
		}

		[Test, Ignore("Working on it...")]
		public void GetEvaluationValueForInProgressGame()
		{
		}

		[Test, Ignore("Working on it...")]
		public void GetEvaluationValueForInProgressGameAboutToBeLost()
		{
			var board = new Board();

			var engine = new AlphaBetaPruningEngine();
			var nextMove = engine.GenerateMove(board.Clone() as Board, new ManualResetEvent(false));

			// The problem is that ABP was doing 0,3 to 4,3 in 0.2.0.0, which causes a loss.
			Assert.IsFalse((nextMove.Source == new Point(0, 3) && nextMove.Destination == new Point(4, 3)), "The next move is invalid.");
		}

		[Test, Ignore("Working on it...")]
		public void GetEvaluationValueForLosingGame()
		{
		}

		[Test, Ignore("Working on it...")]
		public void GetEvaluationValueForWinningGame()
		{
		}

		[Test, Ignore("Working on it...")]
		public void GetEvaluationValueForWinningAndLosingGame()
		{
		}
	}
}
