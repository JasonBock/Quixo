using Quixo.Framework;

namespace Quixo.Engine;

public abstract class BaseEngine
	: IEngine
{
	protected BaseEngine(TextWriter? debugWriter)
		: base() => this.DebugWriter = debugWriter;

	public abstract Move GenerateMove(Board board, ManualResetEvent cancel);

	protected TextWriter? DebugWriter { get; private set; }
}