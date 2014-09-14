using QF = Quixo.Framework;
using Quixo.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public sealed class Board : UserControl
	{
		private const int PieceHeightTotalSpacing = 48;
		private const int PieceWidthTotalSpacing = 48;
		private const int PieceSpace = 8;

		public EventHandler MoveMade;

		private QF.Board board = null;
		private bool moveGenerationInProgress;
		private BackgroundWorker moveWorker;
		private Piece[,] pieces = new Piece[QF.Board.Dimension, QF.Board.Dimension];
		private IEngine playerX = null;
		private IEngine playerO = null;
		private Piece source = null;

		public Board()
		{
			this.InitializeComponent();
			this.InitializeBoard(null, null, null);
			this.moveWorker = new BackgroundWorker();
			this.moveWorker.DoWork += 
				new DoWorkEventHandler(OnMoveWorkerDoWork);
			this.moveWorker.RunWorkerCompleted += 
				new RunWorkerCompletedEventHandler(OnMoveWorkerRunWorkerCompleted);
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
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(64)), ((System.Byte)(64)));
			this.Name = "Board";
			this.Size = new System.Drawing.Size(600, 504);

		}
		#endregion

		private void InitializeBoard(QF.Board board, IEngine playerX, IEngine playerO)
		{
			int x = 0, y = 0;

			if (this.pieces != null)
			{
				for (x = 0; x < QF.Board.Dimension; x++)
				{
					for (y = 0; y < QF.Board.Dimension; y++)
					{
						if (this.pieces[x, y] != null)
						{
							this.Controls.Remove(this.pieces[x, y]);
						}
					}
				}
			}

			this.source = null;
			this.playerX = playerX;
			this.playerO = playerO;

			if (board == null)
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
					Point point = new Point(x, y);
					Piece newPiece = new Piece(new QF.Piece(point, this.board.GetPiece(point)));
					newPiece.Selected += this.OnPieceSelected;

					this.pieces[x, y] = newPiece;
					this.Controls.Add(newPiece);
				}
			}

			this.UpdatePieceStates();
		}

		public void Reset(QF.Board board, IEngine playerX, IEngine playerO)
		{
			this.InitializeBoard(board, playerX, playerO);
			this.CheckForMoveGeneration();
			this.RedrawBoard();
		}

		internal void UpdatePieceStates()
		{
			List<Point> validPieceChoices = null;

			if (this.board.WinningPlayer == QF.Player.None)
			{
				if (this.source != null)
				{
					validPieceChoices = this.board.GetValidDestinationPieces(this.source.RepresentedPiece.Position);
				}
				else
				{
					validPieceChoices = this.board.GetValidSourcePieces();
				}
			}

			for (int x = 0; x < QF.Board.Dimension; x++)
			{
				for (int y = 0; y < QF.Board.Dimension; y++)
				{
					Point position = new Point(x, y);
					Piece piece = this.pieces[x, y];
					piece.CurrentPlayer = this.board.GetPiece(position);

					if (this.board.WinningPlayer != QF.Player.None)
					{
						piece.CurrentSelectedState = Piece.SelectedState.None;
					}
					else
					{
						if (this.source != null && this.source.RepresentedPiece.Position.Equals(position) == true)
						{
							piece.CurrentSelectedState = Piece.SelectedState.Selected;
						}
						else if (validPieceChoices.Contains(position) == true)
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

		private void OnPieceSelected(object sender, EventArgs args)
		{
			if (!this.moveGenerationInProgress)
			{
				Piece piece = sender as Piece;

				bool wasMoveMade = false;

				if (piece.CurrentSelectedState == Piece.SelectedState.CanBeMoved)
				{
					if (this.source == null)
					{
						this.source = piece;
					}
					else
					{
						this.board.MovePiece(this.source.RepresentedPiece.Position,
							 piece.RepresentedPiece.Position);
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
					if (this.MoveMade != null)
					{
						this.MoveMade(this, EventArgs.Empty);
					}

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
			int newWidth = (this.ClientSize.Width - PieceWidthTotalSpacing) / 5;
			int newHeight = (this.ClientSize.Height - PieceHeightTotalSpacing) / 5;
			Size newSize = new Size(newWidth, newHeight);

			if (this.pieces != null)
			{
				for (int x = 0; x < QF.Board.Dimension; x++)
				{
					for (int y = 0; y < QF.Board.Dimension; y++)
					{
						if (this.pieces[x, y] != null)
						{
							this.pieces[x, y].Size = newSize;
							int xCoord = (newSize.Width * x) + (PieceSpace * (x + 1));
							int yCoord = (newSize.Height * (QF.Board.Dimension - 1 - y)) + (PieceSpace * (QF.Board.Dimension - y));
							Point piecePoint = new Point(xCoord, yCoord);
							this.pieces[x, y].Location = piecePoint;
						}
					}
				}
			}
		}

		private void CheckForMoveGeneration()
		{
			if ((this.board.CurrentPlayer == QF.Player.O && this.playerO != null) ||
				 (this.board.CurrentPlayer == QF.Player.X && this.playerX != null))
			{
				IEngine nextMoveEngine = (this.playerX != null) ? this.playerX : ((this.playerO != null) ? this.playerO : null);

				if (nextMoveEngine != null)
				{
					this.moveGenerationInProgress = true;
					this.moveWorker.RunWorkerAsync(nextMoveEngine);
				}
			}
		}

		private void OnMoveWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			QF.Move nextMove = e.Result as QF.Move;

			// TODO (4/12/2005): Add error handling. If the move is in error, the human automatically wins.
			this.board.MovePiece(nextMove.Source, nextMove.Destination);

			if (this.MoveMade != null)
			{
				this.MoveMade(this, EventArgs.Empty);
			}

			this.UpdatePieceStates();
			this.moveGenerationInProgress = false;
		}


		private void OnMoveWorkerDoWork(object sender, DoWorkEventArgs e)
		{
			IEngine nextMoveEngine = e.Argument as IEngine;
			e.Result = nextMoveEngine.GenerateMove(this.board.Clone() as QF.Board, new ManualResetEvent(false));
		}

		internal QF.Board InternalBoard
		{
			get
			{
				return this.board;
			}
		}

		internal bool IsMoveGenerationInProgress
		{
			get
			{
				return this.moveGenerationInProgress;
			}
		}

		internal string PlayerODescription
		{
			get
			{
				string playerODescription = ResetOptions.Human;

				if (this.playerO != null)
				{
					playerODescription = this.playerO.GetType().FullName;
				}

				return playerODescription;
			}
		}

		internal string PlayerXDescription
		{
			get
			{
				string playerXDescription = ResetOptions.Human;

				if (this.playerX != null)
				{
					playerXDescription = this.playerX.GetType().FullName;
				}

				return playerXDescription;
			}
		}
	}
}
