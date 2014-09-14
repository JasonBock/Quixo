using System;
using System.IO;
using System.Threading;
using Quixo.Framework;

namespace Quixo.Engine
{
	public abstract class BaseEngine
		: IEngine
	{
		protected TextWriter debugWriter = null;

		public BaseEngine() : base() { }

		public BaseEngine(TextWriter debugWriter)
			: this()
		{
			if (debugWriter == null)
			{
				throw new ArgumentNullException("debugWriter");
			}

			this.debugWriter = debugWriter;
		}

		public abstract Move GenerateMove(Board board, ManualResetEvent cancel);
	}
}
