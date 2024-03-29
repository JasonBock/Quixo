namespace Quixo.Framework;

/// <summary>
/// Represents a Quixo board.
/// </summary>
/// <remarks>
/// The co-ordinates of the <see cref="Board"/> work as though the lower-left piece is at (0, 0)
/// and the upper-right piece is at (<see cref="Board.Dimension"/> - 1, <see cref="Board.Dimension"/> - 1).
/// </remarks>
[Serializable]
public sealed class Board
	: ICloneable
{
	private const string ErrorIdenticalPiece = "The source and destination locations are the same.";
	private const string ErrorInternalPiece = "Only the outer pieces can be moved.";

	private delegate void AdjustLoopOperator(ref int sweep);
	private delegate bool CheckLoopOperator(int sweep, int checkPoint);
	private delegate int NextPieceOperator(int position);

	/// <summary>
	/// The dimension of the board.
	/// </summary>
	public const int Dimension = 5;

	private Player winningPlayer = Player.None;
	private Player currentPlayer = Player.X;
	private long pieces;
	private MoveCollection moveHistory = new();

	/// <summary>
	/// Initializes a new instance of the <see cref="Board"/> class
	/// </summary>
	public Board()
		: base() => this.Reset();

	private void Reset()
	{
		this.currentPlayer = Player.X;
		this.winningPlayer = Player.None;
		this.moveHistory.Clear();
		this.pieces = 0;
	}

	/// <summary>
	/// Returns the <see cref="Piece"/> object at the specified co-ordinates.
	/// </summary>
	/// <param name="position">
	/// The position of the piece to get.
	/// </param>
	/// <exception cref="IndexOutOfRangeException">
	/// Thrown if the X and Y values in <i>position</i> are invalid.
	/// </exception>
	/// <returns>
	/// The <see cref="Piece"/> object at the requested position.
	/// </returns>
	public Player GetPiece(Point position)
	{
		if (position.X < 0 || position.X > (Board.Dimension) ||
			position.Y < 0 || position.Y > (Board.Dimension))
		{
			throw new ArgumentOutOfRangeException(nameof(position), $"Point {position} is out of range.");
		}

		var shiftOut = Board.GetShiftOut(position.X, position.Y);
		return (Player)((this.pieces >> shiftOut) & 3L);
	}

	/// <summary>
	/// Returns the <see cref="Piece"/> object at the specified co-ordinates.
	/// </summary>
	/// <param name="x">
	/// The x position of the piece.
	/// </param>
	/// <param name="y">
	/// The y position of the piece.
	/// </param>
	/// <exception cref="IndexOutOfRangeException">
	/// Thrown if the X and Y values in <i>position</i> are invalid.
	/// </exception>
	/// <returns>
	/// The <see cref="Piece"/> object at the requested position.
	/// </returns>
	public Player GetPiece(int x, int y) =>
		this.GetPiece(new Point(x, y));

	private static int GetShiftOut(int x, int y) =>
		10 * x + 2 * y;

	public void Undo(int count)
	{
		for (var i = 0; i < count; i++)
		{
			this.Undo();
		}
	}

	public void Undo()
	{
		if (this.moveHistory.Count > 0)
		{
			var currentMoves = (MoveCollection)this.moveHistory.Clone();

			this.Reset();

			for (var i = 0; i < currentMoves.Count - 1; i++)
			{
				var move = currentMoves[i];
				this.MovePiece(move.Source, move.Destination);
			}
		}
	}

#pragma warning disable CA1002 // Do not expose generic lists
	public List<Point> GetValidDestinationPieces(Point source)
#pragma warning restore CA1002 // Do not expose generic lists
	{
		var points = new List<Point>();

		if (Board.IsOuterPiece(source) && this.CanCurrentPlayerUseSource(source))
		{
			if (source.X == 0)
			{
				points.Add(new Point(Board.Dimension - 1, source.Y));
			}
			else if (source.X == (Dimension - 1))
			{
				points.Add(new Point(0, source.Y));
			}
			else
			{
				points.Add(new Point(Dimension - 1, source.Y));
				points.Add(new Point(0, source.Y));
			}

			if (source.Y == 0)
			{
				points.Add(new Point(source.X, Dimension - 1));
			}
			else if (source.Y == (Dimension - 1))
			{
				points.Add(new Point(source.X, 0));
			}
			else
			{
				points.Add(new Point(source.X, Dimension - 1));
				points.Add(new Point(source.X, 0));
			}
		}

		return points;
	}

	/// <summary>
	/// Returns a collection of <see cref="Piece"/> objects that can be moved
	/// by the current player.
	/// </summary>
	/// <returns></returns>
#pragma warning disable CA1002 // Do not expose generic lists
	public List<Point> GetValidSourcePieces()
#pragma warning restore CA1002 // Do not expose generic lists
	{
		var points = new List<Point>();

		for (var x = 1; x < Dimension; x++)
		{
			if (this.GetPiece(x, 0) == this.currentPlayer ||
				 this.GetPiece(x, 0) == Player.None)
			{
				points.Add(new Point(x, 0));
			}
		}

		for (var y = 1; y < Dimension; y++)
		{
			if (this.GetPiece(Dimension - 1, y) == this.currentPlayer ||
				 this.GetPiece(Dimension - 1, y) == Player.None)
			{
				points.Add(new Point(Dimension - 1, y));
			}
		}

		for (var x = Dimension - 2; x >= 0; x--)
		{
			if (this.GetPiece(x, Dimension - 1) == this.currentPlayer ||
				 this.GetPiece(x, Dimension - 1) == Player.None)
			{
				points.Add(new Point(x, Dimension - 1));
			}
		}

		for (var y = Dimension - 2; y >= 0; y--)
		{
			if (this.GetPiece(0, y) == this.currentPlayer ||
				 this.GetPiece(0, y) == Player.None)
			{
				points.Add(new Point(0, y));
			}
		}

		return points;
	}

	private bool IsLessThan(int sweep, int checkPoint) => sweep < checkPoint;
	private bool IsGreaterThan(int sweep, int checkPoint) => sweep > checkPoint;
	private void Increment(ref int sweep) => sweep++;
	private void Decrement(ref int sweep) => sweep--;
	private int NextPieceBack(int position) => --position;
	private int NextPieceForward(int position) => ++position;

	private void UpdateBoard(Point source, Point destination)
	{
		var newValue = this.currentPlayer;
		var isXFixed = source.X == destination.X;
		var fixedValue = (source.X == destination.X) ? source.X : source.Y;

		AdjustLoopOperator loopOp;
		CheckLoopOperator checkOp;
		NextPieceOperator nextPieceOp;

		if (source.Y > destination.Y || source.X > destination.X)
		{
			loopOp = new AdjustLoopOperator(this.Decrement);
			checkOp = new CheckLoopOperator(this.IsGreaterThan);
			nextPieceOp = new NextPieceOperator(this.NextPieceBack);
		}
		else
		{
			loopOp = new AdjustLoopOperator(this.Increment);
			checkOp = new CheckLoopOperator(this.IsLessThan);
			nextPieceOp = new NextPieceOperator(this.NextPieceForward);
		}

		var startPoint = isXFixed ? source.Y : source.X;
		var endPoint = isXFixed ? destination.Y : destination.X;

		for (var sweep = startPoint; checkOp(sweep, endPoint); loopOp(ref sweep))
		{
			if (isXFixed)
			{
				this.SetPiece(fixedValue, sweep,
					this.GetPiece(fixedValue, nextPieceOp(sweep)));
			}
			else
			{
				this.SetPiece(sweep, fixedValue,
					this.GetPiece(nextPieceOp(sweep), fixedValue));
			}
		}

		this.SetPiece(destination.X, destination.Y, newValue);
	}

	public void SetPiece(int x, int y, Player newValue)
	{
		var shiftOut = Board.GetShiftOut(x, y);
		this.pieces &= ~((long)3 << shiftOut);

		if (newValue != Player.None)
		{
			this.pieces |= ((long)newValue << shiftOut);
		}
	}

	/// <summary>
	/// Moves the piece from <i>source</i> to <i>destination</i>.
	/// </summary>
	/// <param name="source"></param>
	/// <param name="destination"></param>
	public void MovePiece(Point source, Point destination)
	{
		var currentBoard = (Board)this.Clone();

		try
		{
			if (this.winningPlayer != Player.None)
			{
				throw new InvalidMoveException($"The game has been won by {this.winningPlayer} - no more moves can be made.");
			}

			this.CheckPieces(source, destination);
			this.UpdateBoard(source, destination);
			this.CheckWinningLines();

			this.moveHistory.Add(new Move(this.currentPlayer, source, destination));

			this.currentPlayer = this.winningPlayer != Player.None ? Player.None :
				this.currentPlayer == Player.X ? Player.O : Player.X;
		}
		catch (InvalidMoveException)
		{
			this.currentPlayer = currentBoard.currentPlayer;
			this.winningPlayer = currentBoard.winningPlayer;

			for (var x = 0; x < Board.Dimension; x++)
			{
				for (var y = 0; y < Board.Dimension; y++)
				{
					this.SetPiece(x, y, currentBoard.GetPiece(x, y));
				}
			}

			throw;
		}
	}

	private void CheckWinningLines()
	{
		var lines = new WinningLines(this);

		if ((this.currentPlayer == Player.X && lines.OCount > 0) ||
			 (this.currentPlayer == Player.O && lines.OCount > 0 && lines.XCount == 0))
		{
			this.winningPlayer = Player.O;
		}
		else if ((this.currentPlayer == Player.O && lines.XCount > 0) ||
			 (this.currentPlayer == Player.X && lines.XCount > 0 && lines.OCount == 0))
		{
			this.winningPlayer = Player.X;
		}
		else
		{
			this.winningPlayer = Player.None;
		}
	}

	private static int GetEndPoint(Point source, Point destination) =>
		source.X == destination.X ? destination.Y : destination.X;

	private bool CanCurrentPlayerUseSource(Point source)
	{
		var pieceState = this.GetPiece(source);

		return ((this.currentPlayer == Player.X && (pieceState == Player.X || pieceState == Player.None)) ||
			 (this.currentPlayer == Player.O && (pieceState == Player.O || pieceState == Player.None)));
	}

	private void CheckPieces(Point source, Point destination)
	{
		if (source.Equals(destination))
		{
			throw new InvalidMoveException(ErrorIdenticalPiece);
		}

		if (!Board.IsOuterPiece(source) || !Board.IsOuterPiece(destination))
		{
			throw new InvalidMoveException(ErrorInternalPiece);
		}

		if (!this.CanCurrentPlayerUseSource(source))
		{
			throw new InvalidMoveException($"The player {this.currentPlayer} cannot move the piece at position {source}.");
		}

		if (source.X != destination.X && source.Y != destination.Y)
		{
			throw new InvalidMoveException($"The player {this.currentPlayer} cannot move a piece to position {destination}.");
		}

		var endPoint = Board.GetEndPoint(source, destination);

		if (endPoint != 0 && endPoint != (Dimension - 1))
		{
			throw new InvalidMoveException($"The player {this.currentPlayer} cannot move a piece to position {destination}.");
		}
	}

	// TODO: I really don't know why CA1508 is firing here. How would it
	// know that the conditional to (Dimension - 1) are always true? If {2, 3}
	// was given, how would that be true??
#pragma warning disable CA1508 // Avoid dead conditional code
	private static bool IsOuterPiece(Point position) =>
		position.X != 0 || position.X != (Board.Dimension - 1) ||
			position.Y != 0 || position.Y != (Board.Dimension - 1);
#pragma warning restore CA1508 // Avoid dead conditional code

	public Player CurrentPlayer => this.currentPlayer;

	public MoveCollection Moves => this.moveHistory;

	public Player WinningPlayer => this.winningPlayer;

	public object Clone() =>
		new Board
		{
			currentPlayer = this.currentPlayer,
			winningPlayer = this.winningPlayer,
			moveHistory = (MoveCollection)this.moveHistory.Clone(),
			pieces = this.pieces
		};

	private sealed class WinningLines
	{
		private const int WinningLineCount = (Board.Dimension * 2) + 2;

		private readonly Board board;
		private int blankCount;
		private int xCount;
		private int oCount;

		public WinningLines(Board board)
			: base()
		{
			this.board = board;
			this.CalculateWinningCounts();
		}

		private void CalculateWinningCounts()
		{
			this.CalculateHorizontalWinners();
			this.CalculateVerticalWinners();
			this.CalculateDiagonalWinners();
			var winningLineCount = this.xCount + this.oCount + this.blankCount;

			if (winningLineCount != WinningLineCount)
			{
				throw new InvalidOperationException($"The line count should be {WinningLineCount} but it was {winningLineCount}.");
			}
		}

		private void CalculateHorizontalWinners()
		{
			for (var y = 0; y < Board.Dimension; y++)
			{
				var lineState = this.board.GetPiece(0, y);

				for (var x = 1; x < Board.Dimension; x++)
				{
					var currentPiece = this.board.GetPiece(x, y);

					if (currentPiece == Player.None || currentPiece != lineState)
					{
						lineState = Player.None;
						break;
					}
				}

				this.UpdatePlayerWinCount(lineState);
			}
		}

		private void CalculateVerticalWinners()
		{
			for (var x = 0; x < Board.Dimension; x++)
			{
				var lineState = this.board.GetPiece(x, 0);

				for (var y = 1; y < Board.Dimension; y++)
				{
					var currentPiece = this.board.GetPiece(x, y);

					if (currentPiece == Player.None || currentPiece != lineState)
					{
						lineState = Player.None;
						break;
					}
				}

				this.UpdatePlayerWinCount(lineState);
			}
		}

		private void CalculateDiagonalWinners()
		{
			var lineState = this.board.GetPiece(0, 0);

			for (var x = 0; x < Board.Dimension; x++)
			{
				var currentPiece = this.board.GetPiece(x, x);

				if (currentPiece == Player.None || currentPiece != lineState)
				{
					lineState = Player.None;
					break;
				}
			}

			this.UpdatePlayerWinCount(lineState);

			lineState = this.board.GetPiece(0, Board.Dimension - 1);

			for (var x = 0; x < Board.Dimension; x++)
			{
				var currentPiece = this.board.GetPiece(x, Board.Dimension - 1 - x);

				if (currentPiece == Player.None || currentPiece != lineState)
				{
					lineState = Player.None;
					break;
				}
			}

			this.UpdatePlayerWinCount(lineState);
		}

		private void UpdatePlayerWinCount(Player lineWinner)
		{
			if (lineWinner == Player.X)
			{
				this.xCount++;
			}
			else if (lineWinner == Player.O)
			{
				this.oCount++;
			}
			else
			{
				this.blankCount++;
			}
		}

		public int NoneCount => this.blankCount;

		public int XCount => this.xCount;

		public int OCount => this.oCount;
	}
}