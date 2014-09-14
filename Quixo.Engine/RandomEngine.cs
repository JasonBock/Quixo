using System;
using System.IO;
using System.Threading;
using Quixo.Framework;

namespace Quixo.Engine
{
	public sealed class RandomEngine
		: BaseEngine
	{
		public RandomEngine()
			: base() { }

		public RandomEngine(TextWriter debugWriter)
			: base(debugWriter) { }

		public override Move GenerateMove(Board board, ManualResetEvent cancel)
		{
			var random = new Random();

			var sources = board.GetValidSourcePieces();
			var sourceIndex = random.Next(sources.Count);
			var source = sources[sourceIndex];

			var destinations = board.GetValidDestinationPieces(source);
			var destinationIndex = random.Next(destinations.Count);
			var destination = destinations[destinationIndex];

			return new Move(board.CurrentPlayer, source, destination);
		}
	}
}
