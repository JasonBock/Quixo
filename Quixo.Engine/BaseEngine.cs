using Quixo.Framework;

namespace Quixo.Engine;

public abstract class BaseEngine
	: IEngine
{
	private readonly TextWriter debugWriter;

	protected BaseEngine(TextWriter debugWriter)
		: base() => this.debugWriter = debugWriter ?? throw new ArgumentNullException(nameof(debugWriter));

	public abstract Move GenerateMove(Board board, ManualResetEvent cancel);
}