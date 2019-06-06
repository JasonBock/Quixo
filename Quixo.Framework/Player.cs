using System;

namespace Quixo.Framework
{
	/// <summary>
	/// This enumeration represent the three valid states that a piece can have.
	/// </summary>
	[Flags]
	public enum Player
	{
		None = 0,
		X = 1,
		O = 2
	}
}