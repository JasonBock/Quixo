using System.Globalization;
using QF = Quixo.Framework;

namespace Quixo;

public partial class MainForm : Form
{
	public MainForm()
	{
		this.InitializeComponent();
		this.board.MoveMade += new EventHandler(this.OnMoveMade);
		this.UpdateGameStatus();
	}

	private void OnAboutMenuClick(object sender, EventArgs e)
	{
		using var about = new AboutDialog();
		about.ShowDialog(this);
	}

	private void OnFileOpenMenuItemClick(object sender, EventArgs e)
	{
		using var openDialog = new OpenFileDialog
		{
			Filter = "Quixo files (*.quixo)|*.quixo",
			FilterIndex = 1,
			RestoreDirectory = true
		};

		if (openDialog.ShowDialog() == DialogResult.OK)
		{
			using var quixoStream = openDialog.OpenFile();
			var board = (QF.Board)QF.BoardFormatter.Deserialize(quixoStream);
			this.board.Reset(board, null, null);
			this.debugTextBox.Text = string.Empty;
			this.UpdateGameStatus();
		}
	}

	private void OnFileSaveMenuItemClick(object sender, EventArgs e)
	{
		using var saveDialog = new SaveFileDialog
		{
			Filter = "Quixo files (*.quixo)|*.quixo",
			FilterIndex = 1,
			RestoreDirectory = true
		};

		if (saveDialog.ShowDialog() == DialogResult.OK)
		{
			using var quixoStream = saveDialog.OpenFile();
			QF.BoardFormatter.Serialize(quixoStream, this.board.InternalBoard);
		}
	}

	private void OnGameResetMenuItemClick(object sender, EventArgs e)
	{
		using var options = new ResetOptions
		{
			DebugText = this.debugTextBox
		};

		var result = options.ShowDialog(this);

		if (result == DialogResult.OK)
		{
			this.board.Reset(null, options.PlayerX, options.PlayerO);
			this.debugTextBox.Text = string.Empty;
			this.UpdateGameStatus();
		}
	}

	private void OnUndoToPointContextMenuClick(object sender, EventArgs e)
	{
		if (this.moveHistoryList.SelectedItems.Count == 1)
		{
			this.board.InternalBoard.Undo(this.moveHistoryList.Items.Count - 1 - this.moveHistoryList.SelectedIndices[0]);
			this.board.UpdatePieceStates();
			this.board.RedrawBoard();
			this.UpdateGameStatus();
		}
	}

	private void OnMoveMade(object? sender, EventArgs args) => this.UpdateGameStatus();

	private void UpdateGameStatus()
	{
		this.currentPlayerValueLabel.Text = this.board.InternalBoard.CurrentPlayer.ToString();
		this.winningPlayerValueLabel.Text = this.board.InternalBoard.WinningPlayer.ToString();
		this.playerOValueLabel.Text = this.board.PlayerODescription;
		this.playerXValueLabel.Text = this.board.PlayerXDescription;

		this.moveHistoryList.SuspendLayout();
		this.moveHistoryList.Items.Clear();

		if (this.board.InternalBoard.Moves.Count > 0)
		{
			for (var i = 0; i < this.board.InternalBoard.Moves.Count; i++)
			{
				var move = this.board.InternalBoard.Moves[i];
				var moveItem = new ListViewItem(
					 new string[]
					 {
						 (i + 1).ToString(CultureInfo.CurrentCulture),
						 move.Player.ToString(),
						 move.Source.ToString(),
						 move.Destination.ToString()
					 });
				this.moveHistoryList.Items.Add(moveItem);
			}
		}

		this.moveHistoryList.ResumeLayout();
	}
}
