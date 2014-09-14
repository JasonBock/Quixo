using System.Threading;
using Quixo.Framework;

namespace Quixo.Engine
{
	public interface IEngine
	{
		Move GenerateMove(Board board, ManualResetEvent cancel);
	}
}
