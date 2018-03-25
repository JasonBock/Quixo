using System.Drawing;
using System.Threading;
using Quixo.Framework;

namespace Quixo.Engine.Tests
{
	public class BadNullTestEngine
		: BaseEngine
	{
		public override Move GenerateMove(Board board, ManualResetEvent cancel) => null;
	}

	public class BadMoveTestEngine
		: BaseEngine
	{
		public override Move GenerateMove(Board board, ManualResetEvent cancel) => 
			new Move(board.CurrentPlayer, new Point(0, 0), new Point(0, 0));
	}
}
