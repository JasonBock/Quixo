using NUnit.Framework;
using Quixo.Framework;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace Quixo.SmartEngine.Tests;

// TODO: The .quixo files are saved with the binary formatter,
// so I have to open them with BinaryFormatter. At some
// **very near** point in the future, I'll have to open them, save them
// using BoardFormatter, and then use that.

#pragma warning disable CA2301 // This method is insecure
#pragma warning disable CA2300 // Do not use insecure deserializer BinaryFormatter
#pragma warning disable SYSLIB0011 // Type or member is obsolete
public static class SmartEngineTests
{
	[Test]
	public static void NextMoveShouldNotCauseCrash()
	{
		using var stream = new FileStream(
			Path.Combine(TestContext.CurrentContext.WorkDirectory, "ComputerAsOWillCrashOnNextMove.quixo"), FileMode.Open);
		var formatter = new BinaryFormatter();
		var board = (Board)formatter.Deserialize(stream);

		//  The next move used to cause a crash.
		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);
		board.MovePiece(nextMove.Source, nextMove.Destination);
	}

	[Test]
	public static void CheckDoesNotPreventLoss()
	{
		using var stream = new FileStream(
			Path.Combine(TestContext.CurrentContext.WorkDirectory, "EngineDoesntPreventLoss.quixo"), FileMode.Open);
		var formatter = new BinaryFormatter();
		var board = (Board)formatter.Deserialize(stream);

		board.Undo(4);

		//  At move #23, why doesn't O respond with {0, 1} to {4, 1}?
		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);

		Assert.Multiple(() =>
		{
			Assert.That(nextMove.Source, Is.EqualTo(new Point(0, 1)), "The source is invalid.");
			Assert.That(nextMove.Destination, Is.EqualTo(new Point(4, 1)), "The destination is invalid.");
		});
	}

	[Test]
	public static void CheckMissedWinningMove()
	{
		using var stream = new FileStream(
			Path.Combine(TestContext.CurrentContext.WorkDirectory, "HereEngineMissedWinningMove.quixo"), FileMode.Open);
		var formatter = new BinaryFormatter();
		var board = (Board)formatter.Deserialize(stream);

		board.Undo();

		// For some reason, 0.2.0.3 doesn't catch that (1, 4) to (1, 0) would win...
		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);

		Assert.Multiple(() =>
		{
			Assert.That(nextMove.Source, Is.EqualTo(new Point(1, 4)), "The source is invalid.");
			Assert.That(nextMove.Destination, Is.EqualTo(new Point(1, 0)), "The destination is invalid.");
		});
	}

	[Test]
	public static void CheckBadMove0202()
	{
		using var stream = new FileStream(
			Path.Combine(TestContext.CurrentContext.WorkDirectory, "BadMove0.2.0.2.quixo"), FileMode.Open);
		var formatter = new BinaryFormatter();
		var board = (Board)formatter.Deserialize(stream);
		board.Undo(2);

		// For some reason, 0.2.0.2 thinks that every next move is a losing move... :S
		// and I think it's right - it's in a position that every move would lead to a losing move.
		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);

		Assert.That((nextMove.Source == new Point(2, 0) && nextMove.Destination == new Point(2, 4)), Is.False, "The next move is invalid.");
	}

	[Test]
	public static void CheckBadMove0201()
	{
		using var stream = new FileStream(
			Path.Combine(TestContext.CurrentContext.WorkDirectory, "BadMove0.2.0.1.quixo"), FileMode.Open);
		var formatter = new BinaryFormatter();
		var board = (Board)formatter.Deserialize(stream);
		board.Undo(2);

		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);

		Assert.That((nextMove.Source == new Point(1, 0) && nextMove.Destination == new Point(0, 0)), Is.False, "The next move is invalid.");
	}

	[Test, Ignore("Working on it...")]
	public static void GetEvaluationValueForInProgressGame()
	{
	}

	[Test, Ignore("Working on it...")]
	public static void GetEvaluationValueForInProgressGameAboutToBeLost()
	{
		var board = new Board();

		using var debugWriter = new StringWriter();
		var engine = new AlphaBetaPruningEngine(debugWriter);
		using var cancel = new ManualResetEvent(false);
		var nextMove = engine.GenerateMove((Board)board.Clone(), cancel);

		// The problem is that ABP was doing 0,3 to 4,3 in 0.2.0.0, which causes a loss.
		Assert.That((nextMove.Source == new Point(0, 3) && nextMove.Destination == new Point(4, 3)), Is.False, "The next move is invalid.");
	}

	[Test, Ignore("Working on it...")]
	public static void GetEvaluationValueForLosingGame()
	{
	}

	[Test, Ignore("Working on it...")]
	public static void GetEvaluationValueForWinningGame()
	{
	}

	[Test, Ignore("Working on it...")]
	public static void GetEvaluationValueForWinningAndLosingGame()
	{
	}
}
#pragma warning restore SYSLIB0011 // Type or member is obsolete
#pragma warning restore CA2300 // Do not use insecure deserializer BinaryFormatter
#pragma warning restore CA2301 // This method is insecure