using Quixo.Engine;
using Quixo.Framework;
using System.IO;
using System.Threading;

namespace Quixo.SmartEngine
{
	public sealed class AlphaBetaPruningEngine : BaseEngine
	{
		//  NOTE: This may need to be "configurable"...
		private const int DepthLimit = 4;
		private const int LosingLine = int.MinValue;
		private const int WinningLine = int.MaxValue;

		public AlphaBetaPruningEngine(TextWriter debugWriter)
			: base(debugWriter) { }

		public override Move GenerateMove(Board board, ManualResetEvent cancel)
		{
			var bestValue = int.MinValue;
			Move generatedMove = null;

			foreach(var source in board.GetValidSourcePieces())
			{
				foreach(var destination in board.GetValidDestinationPieces(source))
				{
					var nextMoveBoard = ((Board)board.Clone());
					nextMoveBoard.MovePiece(source, destination);

					var possibleBestValue = this.MinimaxAB(nextMoveBoard, board.CurrentPlayer, false, 1,
						int.MinValue, int.MaxValue);

					if(possibleBestValue > bestValue || (possibleBestValue >= bestValue && generatedMove == null))
					{
						bestValue = possibleBestValue;
						generatedMove = new Move(board.CurrentPlayer, source, destination);
					}
				}
			}

			if(this.debugWriter != null)
			{
				this.debugWriter.WriteLine(
					 string.Format("Best value for move {0}: {1} ", generatedMove.Print(), bestValue));
				var nextMoveBoard = (Board)board.Clone();
				nextMoveBoard.MovePiece(generatedMove.Source, generatedMove.Destination);

				this.debugWriter.WriteLine(
					 string.Format("Evaluation of board for move {0}: {1} ", generatedMove.Print(),
					 this.Evaluate(nextMoveBoard, board.CurrentPlayer) * -1));
			}

			return generatedMove;
		}

		private int MinimaxAB(Board board, Player currentPlayer, bool isMax, int depth, int alpha, int beta)
		{
			var evaluation = 0;

			if(depth >= DepthLimit || board.WinningPlayer != Player.None)
			{
				evaluation = this.Evaluate(board, currentPlayer);

				if(board.CurrentPlayer != Player.None && board.CurrentPlayer != currentPlayer)
				{
					evaluation *= -1;
				}
				else if(board.CurrentPlayer == Player.None && board.WinningPlayer != currentPlayer)
				{
					evaluation *= -1;
				}
			}
			else
			{
				var nextEvaluation = 0;

				foreach(var source in board.GetValidSourcePieces())
				{
					foreach(var destination in board.GetValidDestinationPieces(source))
					{
						var nextMoveBoard = (Board)board.Clone();
						nextMoveBoard.MovePiece(source, destination);

						var newDepth = depth;
						nextEvaluation = this.MinimaxAB(nextMoveBoard, currentPlayer, !isMax, ++newDepth, alpha, beta);

						if(alpha > beta)
						{
							break;
						}
						else if(isMax == false && nextEvaluation < beta)
						{
							beta = nextEvaluation;
						}
						else if(isMax == true && nextEvaluation > alpha)
						{
							alpha = nextEvaluation;
						}
					}

					if(alpha > beta)
					{
						break;
					}
				}

				if(isMax == true)
				{
					evaluation = alpha;
				}
				else
				{
					evaluation = beta;
				}
			}

			return evaluation;
		}

		private int Evaluate(Board board, Player currentPlayer)
		{
			int evaluation;
			if (board.WinningPlayer != Player.None)
			{
				if(board.WinningPlayer == currentPlayer)
				{
					evaluation = int.MaxValue;
				}
				else
				{
					evaluation = int.MinValue;
				}
			}
			else
			{
				evaluation = this.Evaluate(board);
			}

			return evaluation;
		}

		public int Evaluate(Board board)
		{
			var evaluation = 0;

			if(board.WinningPlayer != Player.None)
			{
				if(board.Moves.Count > 0)
				{
					var lastMove = board.Moves[board.Moves.Count - 1];

					if(lastMove.Player == board.WinningPlayer)
					{
						evaluation = WinningLine;
					}
					else
					{
						evaluation = LosingLine;
					}
				}
			}
			else
			{
				evaluation = this.EvaluateHorizontalLines(board, evaluation);

				if(evaluation != LosingLine && evaluation != WinningLine)
				{
					evaluation = this.EvaluateVerticalLines(board, evaluation);
				}

				if(evaluation != LosingLine && evaluation != WinningLine)
				{
					evaluation = this.EvaluateDiagonalLines(board, evaluation);
				}
			}

			return evaluation;
		}

		// TODO - this is wrong!
		private int UpdateContinuation(int currentContinuationValue) =>
			(currentContinuationValue ^ 2) * 4;

		private int EvaluateHorizontalLines(Board board, int evaluation)
		{
			var horizontalEvaluation = evaluation;
			bool hasWinningLine = false, hasLosingLine = false;

			for(var y = 0; y < Board.Dimension; y++)
			{
				var lineState = board.GetPiece(0, y);

				if(lineState == board.CurrentPlayer)
				{
					horizontalEvaluation++;
				}
				else if(lineState != Player.None)
				{
					horizontalEvaluation--;
				}

				var continuationFactor = 1;

				for(var x = 1; x < Board.Dimension; x++)
				{
					var currentPiece = board.GetPiece(x, y);

					if(currentPiece == board.CurrentPlayer)
					{
						horizontalEvaluation++;
					}
					else if(currentPiece != Player.None)
					{
						horizontalEvaluation--;
					}

					if(currentPiece == board.GetPiece(x - 1, y))
					{
						continuationFactor = this.UpdateContinuation(continuationFactor);

						if(currentPiece == board.CurrentPlayer)
						{
							horizontalEvaluation += continuationFactor;
						}
						else if(currentPiece != Player.None)
						{
							horizontalEvaluation -= continuationFactor;
						}
					}
					else
					{
						lineState = Player.None;
						continuationFactor = 1;
					}
				}

				if(lineState == board.CurrentPlayer)
				{
					hasWinningLine = true;
					break;
				}
				else if(lineState != Player.None)
				{
					hasLosingLine = true;
					break;
				}
			}

			if(hasWinningLine == true && hasLosingLine == false)
			{
				horizontalEvaluation = WinningLine;
			}
			else if(hasLosingLine == true)
			{
				horizontalEvaluation = LosingLine;
			}

			return horizontalEvaluation;
		}

		private int EvaluateVerticalLines(Board board, int evaluation)
		{
			var verticalEvaluation = evaluation;
			bool hasWinningLine = false, hasLosingLine = false;

			for(var x = 0; x < Board.Dimension; x++)
			{
				var lineState = board.GetPiece(x, 0);

				if(lineState == board.CurrentPlayer)
				{
					verticalEvaluation++;
				}
				else if(lineState != Player.None)
				{
					verticalEvaluation--;
				}

				var continuationFactor = 1;

				for(var y = 1; y < Board.Dimension; y++)
				{
					var currentPiece = board.GetPiece(x, y);

					if(currentPiece == board.CurrentPlayer)
					{
						verticalEvaluation++;
					}
					else if(currentPiece != Player.None)
					{
						verticalEvaluation--;
					}

					if(currentPiece == board.GetPiece(x, y - 1))
					{
						continuationFactor = this.UpdateContinuation(continuationFactor);

						if(currentPiece == board.CurrentPlayer)
						{
							verticalEvaluation += continuationFactor;
						}
						else if(currentPiece != Player.None)
						{
							verticalEvaluation -= continuationFactor;
						}
					}
					else
					{
						lineState = Player.None;
						continuationFactor = 1;
					}
				}

				if(lineState == board.CurrentPlayer)
				{
					hasWinningLine = true;
					break;
				}
				else if(lineState != Player.None)
				{
					hasLosingLine = true;
					break;
				}
			}

			if(hasWinningLine == true && hasLosingLine == false)
			{
				verticalEvaluation = WinningLine;
			}
			else if(hasLosingLine == true)
			{
				verticalEvaluation = LosingLine;
			}

			return verticalEvaluation;
		}

		private int EvaluateDiagonalLines(Board board, int evaluation)
		{
			var diagonalEvaluation = evaluation;

			var lineState = board.GetPiece(0, 0);

			if(lineState == board.CurrentPlayer)
			{
				diagonalEvaluation++;
			}
			else if(lineState != Player.None)
			{
				diagonalEvaluation--;
			}

			var continuationFactor = 1;

			for(var x = 1; x < Board.Dimension; x++)
			{
				var currentPiece = board.GetPiece(x, x);

				if(currentPiece == board.CurrentPlayer)
				{
					diagonalEvaluation++;
				}
				else if(currentPiece != Player.None)
				{
					diagonalEvaluation--;
				}

				if(currentPiece == board.GetPiece(x - 1, x - 1))
				{
					continuationFactor = this.UpdateContinuation(continuationFactor);

					if(currentPiece == board.CurrentPlayer)
					{
						diagonalEvaluation += continuationFactor;
					}
					else if(currentPiece != Player.None)
					{
						diagonalEvaluation -= continuationFactor;
					}
				}
				else
				{
					lineState = Player.None;
					continuationFactor = 1;
				}
			}

			if(lineState != Player.None && lineState != board.CurrentPlayer)
			{
				diagonalEvaluation = LosingLine;
			}
			else
			{
				if(lineState == board.CurrentPlayer)
				{
					diagonalEvaluation = WinningLine;
				}
				else
				{
					lineState = board.GetPiece(0, Board.Dimension - 1);

					if(lineState == board.CurrentPlayer)
					{
						diagonalEvaluation++;
					}
					else if(lineState != Player.None)
					{
						diagonalEvaluation--;
					}

					for(var x = 1; x < Board.Dimension; x++)
					{
						var currentPiece = board.GetPiece(x, Board.Dimension - 1 - x);

						if(currentPiece == board.CurrentPlayer)
						{
							diagonalEvaluation++;
						}
						else if(currentPiece != Player.None)
						{
							diagonalEvaluation--;
						}

						if(currentPiece == board.GetPiece(x - 1, Board.Dimension - x))
						{
							continuationFactor = this.UpdateContinuation(continuationFactor);

							if(currentPiece == board.CurrentPlayer)
							{
								diagonalEvaluation += continuationFactor;
							}
							else if(currentPiece != Player.None)
							{
								diagonalEvaluation -= continuationFactor;
							}
						}
						else
						{
							lineState = Player.None;
							continuationFactor = 1;
						}
					}

					if(lineState == board.CurrentPlayer)
					{
						diagonalEvaluation = WinningLine;
					}
					else if(lineState != Player.None)
					{
						diagonalEvaluation = LosingLine;
					}
				}
			}

			return diagonalEvaluation;
		}
	}
}
