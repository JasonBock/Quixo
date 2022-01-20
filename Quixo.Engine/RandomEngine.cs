using Quixo.Framework;
using System.Security.Cryptography;

namespace Quixo.Engine;

public sealed class RandomEngine
	: BaseEngine
{
	public RandomEngine(TextWriter debugWriter)
		: base(debugWriter) { }

	public override Move GenerateMove(Board board, ManualResetEvent cancel)
	{
		var sources = board.GetValidSourcePieces();
		var sourceIndex = RandomNumberGenerator.GetInt32(sources.Count);
		var source = sources[sourceIndex];

		var destinations = board.GetValidDestinationPieces(source);
		var destinationIndex = RandomNumberGenerator.GetInt32(destinations.Count);
		var destination = destinations[destinationIndex];

		return new Move(board.CurrentPlayer, source, destination);
	}
}