using QF = Quixo.Framework;
using Quixo.Engine;
using System.ComponentModel;

namespace Quixo;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
public sealed class Board
	: UserControl
{
	private const int PieceHeightTotalSpacing = 48;
	private const int PieceWidthTotalSpacing = 48;
	private const int PieceSpace = 8;

	public EventHandler? MoveMade { get; set; }

	private QF.Board? board;
	private bool moveGenerationInProgress;
	private readonly BackgroundWorker moveWorker;
	private Piece[,] pieces = new Piece[QF.Board.Dimension, QF.Board.Dimension];
	private IEngine? playerX;
	private IEngine? playerO;
	private Piece? source;

	public Board()
	{
		this.InitializeComponent();
		this.InitializeBoard(null, null, null);
		this.moveWorker = new BackgroundWorker();
		this.moveWorker.DoWork +=
			new DoWorkEventHandler(this.OnMoveWorkerDoWork);
		this.moveWorker.RunWorkerCompleted +=
			new RunWorkerCompletedEventHandler(this.OnMoveWorkerRunWorkerCompleted);
	}

	#region Component Designer generated code
	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		// 
		// Board
		// 
		this.BackColor = Color.FromArgb(0, 64, 64);
		this.Name = "Board";
		this.Size = new Size(600, 504);

	}
	#endregion

	private void InitializeBoard(QF.Board? board, IEngine? playerX, IEngine? playerO)
	{
		int x = 0, y = 0;

		if (this.pieces is not null)
		{
			for (x = 0; x < QF.Board.Dimension; x++)
			{
				for (y = 0; y < QF.Board.Dimension; y++)
				{
					if (this.pieces[x, y] is not null)
					{
						this.Controls.Remove(this.pieces[x, y]);
					}
				}
			}
		}

		this.source = null;
		this.playerX = playerX;
		this.playerO = playerO;

		if (board is null)
		{
			this.board = new QF.Board();
		}
		else
		{
			this.board = board;
		}

		this.pieces = new Piece[QF.Board.Dimension, QF.Board.Dimension];

		for (x = 0; x < QF.Board.Dimension; x++)
		{
			for (y = 0; y < QF.Board.Dimension; y++)
			{
				var point = new Point(x, y);
				var newPiece = new Piece(new QF.Piece(point, this.board.GetPiece(point)));
				newPiece.Selected += this.OnPieceSelected;

				this.pieces[x, y] = newPiece;
				this.Controls.Add(newPiece);
			}
		}

		this.UpdatePieceStates();
	}

	public void Reset(QF.Board? board, IEngine? playerX, IEngine? playerO)
	{
		this.InitializeBoard(board, playerX, playerO);
		this.CheckForMoveGeneration();
		this.RedrawBoard();
	}

	internal void UpdatePieceStates()
	{
		List<Point>? validPieceChoices = null;

		if (this.board!.WinningPlayer == QF.Player.None)
		{
			if (this.source is not null)
			{
				validPieceChoices = this.board.GetValidDestinationPieces(this.source.RepresentedPiece!.Position);
			}
			else
			{
				validPieceChoices = this.board.GetValidSourcePieces();
			}
		}

		for (var x = 0; x < QF.Board.Dimension; x++)
		{
			for (var y = 0; y < QF.Board.Dimension; y++)
			{
				var position = new Point(x, y);
				var piece = this.pieces[x, y];
				piece.CurrentPlayer = this.board.GetPiece(position);

				if (this.board.WinningPlayer != QF.Player.None)
				{
					piece.CurrentSelectedState = Piece.SelectedState.None;
				}
				else
				{
					if (this.source is not null && this.source.RepresentedPiece!.Position.Equals(position) == true)
					{
						piece.CurrentSelectedState = Piece.SelectedState.Selected;
					}
					else if (validPieceChoices!.Contains(position) == true)
					{
						piece.CurrentSelectedState = Piece.SelectedState.CanBeMoved;
					}
					else
					{
						piece.CurrentSelectedState = Piece.SelectedState.None;
					}
				}
			}
		}
	}

	private void OnPieceSelected(object? sender, EventArgs args)
	{
		if (!this.moveGenerationInProgress)
		{
			var piece = sender as Piece;

			var wasMoveMade = false;

			if (piece!.CurrentSelectedState == Piece.SelectedState.CanBeMoved)
			{
				if (this.source is null)
				{
					this.source = piece;
				}
				else
				{
					this.board!.MovePiece(this.source.RepresentedPiece!.Position,
						piece.RepresentedPiece!.Position);
					wasMoveMade = true;
					this.source = null;
				}
			}
			else
			{
				this.source = null;
			}

			if (wasMoveMade == true)
			{
				this.MoveMade?.Invoke(this, EventArgs.Empty);
				this.CheckForMoveGeneration();
			}

			this.UpdatePieceStates();
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		this.RedrawBoard();
	}

	public void RedrawBoard()
	{
		var newWidth = (this.ClientSize.Width - PieceWidthTotalSpacing) / 5;
		var newHeight = (this.ClientSize.Height - PieceHeightTotalSpacing) / 5;
		var newSize = new Size(newWidth, newHeight);

		if (this.pieces is not null)
		{
			for (var x = 0; x < QF.Board.Dimension; x++)
			{
				for (var y = 0; y < QF.Board.Dimension; y++)
				{
					if (this.pieces[x, y] is not null)
					{
						this.pieces[x, y].Size = newSize;
						var xCoord = (newSize.Width * x) + (PieceSpace * (x + 1));
						var yCoord = (newSize.Height * (QF.Board.Dimension - 1 - y)) + (PieceSpace * (QF.Board.Dimension - y));
						var piecePoint = new Point(xCoord, yCoord);
						this.pieces[x, y].Location = new System.Drawing.Point(piecePoint.X, piecePoint.Y);
					}
				}
			}
		}
	}

	private void CheckForMoveGeneration()
	{
		if ((this.board!.CurrentPlayer == QF.Player.O && this.playerO is not null) ||
			 (this.board.CurrentPlayer == QF.Player.X && this.playerX is not null))
		{
			var nextMoveEngine = this.playerX ?? (this.playerO ?? null);

			if (nextMoveEngine is not null)
			{
				this.moveGenerationInProgress = true;
				this.moveWorker.RunWorkerAsync(nextMoveEngine);
			}
		}
	}

	private void OnMoveWorkerRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
	{
		var nextMove = (QF.Move)e.Result!;

		// TODO (4/12/2005): Add error handling. If the move is in error, the human automatically wins.
		this.board!.MovePiece(nextMove.Source, nextMove.Destination);

		this.MoveMade?.Invoke(this, EventArgs.Empty);

		this.UpdatePieceStates();
		this.moveGenerationInProgress = false;
	}


	private void OnMoveWorkerDoWork(object? sender, DoWorkEventArgs e)
	{
		var nextMoveEngine = (IEngine)e.Argument!;
		using var cancel = new ManualResetEvent(false);
		e.Result = nextMoveEngine.GenerateMove((QF.Board)this.board!.Clone(), cancel);
	}

	internal QF.Board InternalBoard => this.board!;

	internal bool IsMoveGenerationInProgress => this.moveGenerationInProgress;

	internal string PlayerODescription
	{
		get
		{
			var playerODescription = ResetOptions.Human;

			if (this.playerO is not null)
			{
				playerODescription = this.playerO.GetType().FullName!;
			}

			return playerODescription;
		}
	}

	internal string PlayerXDescription
	{
		get
		{
			var playerXDescription = ResetOptions.Human;

			if (this.playerX is not null)
			{
				playerXDescription = this.playerX.GetType().FullName!;
			}

			return playerXDescription;
		}
	}
}
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional