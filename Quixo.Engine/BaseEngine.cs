using System;
using System.IO;
using System.Threading;

namespace Quixo.Engine
{
   public abstract class BaseEngine
		: IEngine
	{
		protected TextWriter debugWriter = null;

		protected BaseEngine() : base() { }

		public BaseEngine(TextWriter debugWriter)
			: this() => this.debugWriter = debugWriter ?? throw new ArgumentNullException(nameof(debugWriter));

		public abstract Move GenerateMove(Board board, ManualResetEvent cancel);
	}
}
