using Quixo.Framework;
using System.Drawing;

namespace Quixo.Engine.Tests;

public class BadNullTestEngine
	: BaseEngine
{
	public BadNullTestEngine(TextWriter debugWriter)
		: base(debugWriter) { }

	public override Move GenerateMove(Board board, ManualResetEvent cancel) => null!;
}

public class BadMoveTestEngine
	: BaseEngine
{
	public BadMoveTestEngine(TextWriter debugWriter)
		: base(debugWriter) { }

	public override Move GenerateMove(Board board, ManualResetEvent cancel) =>
#pragma warning disable CA1062 // Validate arguments of public methods
		new(board.CurrentPlayer, new Point(0, 0), new Point(0, 0));
#pragma warning restore CA1062 // Validate arguments of public methods
}