using System.Threading;

namespace Quixo.Engine
{
   public interface IEngine
	{
		Move GenerateMove(Board board, ManualResetEvent cancel);
	}
}
