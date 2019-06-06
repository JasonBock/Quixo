using QF = Quixo.Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public class MainForm : Form
	{
		private Panel gameStatisticsPanel;
		private Splitter gameStatisticsSplitter;
		private Panel debugPanel;
		private Splitter debugSplitter;
		private Panel moveHistoryPanel;
		private Splitter moveHistorySplitter;
		private Panel boardPanel;
		private ContextMenu undoContextMenu;
		private MenuItem undoToPointContextMenu;
		private MainMenu mainMenu;
		private MenuItem fileMenuItem;
		private MenuItem fileOpenMenuItem;
		private MenuItem fileSaveMenuItem;
		private MenuItem separator2MenuItem;
		private MenuItem printMenuItem;
		private MenuItem separator1MenuItem;
		private MenuItem fileExitMenuItem;
		private MenuItem gameMenuItem;
		private MenuItem gameResetMenuItem;
		private MenuItem helpMenu;
		private MenuItem aboutMenu;
		private Quixo.Controls.Board board;
		private ListView moveHistoryList;
		private ColumnHeader moveColumn;
		private ColumnHeader playerColumn;
		private ColumnHeader sourceColumn;
		private ColumnHeader destinationColumn;
		private Label currentPlayerLabel;
		private Label winningPlayerLabel;
		private Label winningPlayerValueLabel;
		private Label currentPlayerValueLabel;
		private Label playerOLabel;
		private Label playerXLabel;
		private Label playerOValueLabel;
		private Label playerXValueLabel;
		private TextBox debugTextBox;
		private IContainer components;

		public MainForm()
		{
			this.InitializeComponent();
			this.board.MoveMade += new EventHandler(this.OnMoveMade);
			this.UpdateGameStatus();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new Container();
			this.gameStatisticsPanel = new Panel();
			this.playerOValueLabel = new Label();
			this.playerXValueLabel = new Label();
			this.playerOLabel = new Label();
			this.playerXLabel = new Label();
			this.winningPlayerValueLabel = new Label();
			this.currentPlayerValueLabel = new Label();
			this.winningPlayerLabel = new Label();
			this.currentPlayerLabel = new Label();
			this.gameStatisticsSplitter = new Splitter();
			this.debugPanel = new Panel();
			this.debugTextBox = new TextBox();
			this.debugSplitter = new Splitter();
			this.moveHistoryPanel = new Panel();
			this.moveHistoryList = new ListView();
			this.moveColumn = new ColumnHeader();
			this.playerColumn = new ColumnHeader();
			this.sourceColumn = new ColumnHeader();
			this.destinationColumn = new ColumnHeader();
			this.undoContextMenu = new ContextMenu();
			this.undoToPointContextMenu = new MenuItem();
			this.moveHistorySplitter = new Splitter();
			this.boardPanel = new Panel();
			this.mainMenu = new MainMenu(this.components);
			this.fileMenuItem = new MenuItem();
			this.fileOpenMenuItem = new MenuItem();
			this.fileSaveMenuItem = new MenuItem();
			this.separator2MenuItem = new MenuItem();
			this.printMenuItem = new MenuItem();
			this.separator1MenuItem = new MenuItem();
			this.fileExitMenuItem = new MenuItem();
			this.gameMenuItem = new MenuItem();
			this.gameResetMenuItem = new MenuItem();
			this.helpMenu = new MenuItem();
			this.aboutMenu = new MenuItem();
			this.board = new Quixo.Controls.Board();
			this.gameStatisticsPanel.SuspendLayout();
			this.debugPanel.SuspendLayout();
			this.moveHistoryPanel.SuspendLayout();
			this.boardPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// gameStatisticsPanel
			// 
			this.gameStatisticsPanel.Controls.Add(this.playerOValueLabel);
			this.gameStatisticsPanel.Controls.Add(this.playerXValueLabel);
			this.gameStatisticsPanel.Controls.Add(this.playerOLabel);
			this.gameStatisticsPanel.Controls.Add(this.playerXLabel);
			this.gameStatisticsPanel.Controls.Add(this.winningPlayerValueLabel);
			this.gameStatisticsPanel.Controls.Add(this.currentPlayerValueLabel);
			this.gameStatisticsPanel.Controls.Add(this.winningPlayerLabel);
			this.gameStatisticsPanel.Controls.Add(this.currentPlayerLabel);
			this.gameStatisticsPanel.Dock = DockStyle.Top;
			this.gameStatisticsPanel.Location = new Point(0, 0);
			this.gameStatisticsPanel.Name = "gameStatisticsPanel";
			this.gameStatisticsPanel.Size = new Size(736, 72);
			this.gameStatisticsPanel.TabIndex = 0;
			// 
			// playerOValueLabel
			// 
			this.playerOValueLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
							| AnchorStyles.Right);
			this.playerOValueLabel.BorderStyle = BorderStyle.FixedSingle;
			this.playerOValueLabel.Location = new Point(208, 40);
			this.playerOValueLabel.Name = "playerOValueLabel";
			this.playerOValueLabel.Size = new Size(520, 23);
			this.playerOValueLabel.TabIndex = 7;
			this.playerOValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// playerXValueLabel
			// 
			this.playerXValueLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
							| AnchorStyles.Right);
			this.playerXValueLabel.BorderStyle = BorderStyle.FixedSingle;
			this.playerXValueLabel.Location = new Point(208, 8);
			this.playerXValueLabel.Name = "playerXValueLabel";
			this.playerXValueLabel.Size = new Size(520, 23);
			this.playerXValueLabel.TabIndex = 6;
			this.playerXValueLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// playerOLabel
			// 
			this.playerOLabel.Location = new Point(176, 40);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new Size(24, 23);
			this.playerOLabel.TabIndex = 5;
			this.playerOLabel.Text = "O";
			this.playerOLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// playerXLabel
			// 
			this.playerXLabel.Location = new Point(176, 8);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new Size(24, 23);
			this.playerXLabel.TabIndex = 4;
			this.playerXLabel.Text = "X";
			this.playerXLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// winningPlayerValueLabel
			// 
			this.winningPlayerValueLabel.BorderStyle = BorderStyle.FixedSingle;
			this.winningPlayerValueLabel.Location = new Point(104, 40);
			this.winningPlayerValueLabel.Name = "winningPlayerValueLabel";
			this.winningPlayerValueLabel.Size = new Size(56, 23);
			this.winningPlayerValueLabel.TabIndex = 3;
			this.winningPlayerValueLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// currentPlayerValueLabel
			// 
			this.currentPlayerValueLabel.BorderStyle = BorderStyle.FixedSingle;
			this.currentPlayerValueLabel.Location = new Point(104, 8);
			this.currentPlayerValueLabel.Name = "currentPlayerValueLabel";
			this.currentPlayerValueLabel.Size = new Size(56, 23);
			this.currentPlayerValueLabel.TabIndex = 2;
			this.currentPlayerValueLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// winningPlayerLabel
			// 
			this.winningPlayerLabel.Location = new Point(8, 40);
			this.winningPlayerLabel.Name = "winningPlayerLabel";
			this.winningPlayerLabel.Size = new Size(88, 23);
			this.winningPlayerLabel.TabIndex = 1;
			this.winningPlayerLabel.Text = "Winning Player";
			this.winningPlayerLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// currentPlayerLabel
			// 
			this.currentPlayerLabel.Location = new Point(8, 8);
			this.currentPlayerLabel.Name = "currentPlayerLabel";
			this.currentPlayerLabel.Size = new Size(88, 23);
			this.currentPlayerLabel.TabIndex = 0;
			this.currentPlayerLabel.Text = "Current Player";
			this.currentPlayerLabel.TextAlign = ContentAlignment.MiddleRight;
			// 
			// gameStatisticsSplitter
			// 
			this.gameStatisticsSplitter.Dock = DockStyle.Top;
			this.gameStatisticsSplitter.Location = new Point(0, 72);
			this.gameStatisticsSplitter.Name = "gameStatisticsSplitter";
			this.gameStatisticsSplitter.Size = new Size(736, 3);
			this.gameStatisticsSplitter.TabIndex = 1;
			this.gameStatisticsSplitter.TabStop = false;
			// 
			// debugPanel
			// 
			this.debugPanel.Controls.Add(this.debugTextBox);
			this.debugPanel.Dock = DockStyle.Bottom;
			this.debugPanel.Location = new Point(0, 413);
			this.debugPanel.Name = "debugPanel";
			this.debugPanel.Size = new Size(736, 100);
			this.debugPanel.TabIndex = 2;
			// 
			// debugTextBox
			// 
			this.debugTextBox.Dock = DockStyle.Fill;
			this.debugTextBox.Location = new Point(0, 0);
			this.debugTextBox.MaxLength = 0;
			this.debugTextBox.Multiline = true;
			this.debugTextBox.Name = "debugTextBox";
			this.debugTextBox.ReadOnly = true;
			this.debugTextBox.ScrollBars = ScrollBars.Both;
			this.debugTextBox.Size = new Size(736, 100);
			this.debugTextBox.TabIndex = 0;
			// 
			// debugSplitter
			// 
			this.debugSplitter.Dock = DockStyle.Bottom;
			this.debugSplitter.Location = new Point(0, 410);
			this.debugSplitter.Name = "debugSplitter";
			this.debugSplitter.Size = new Size(736, 3);
			this.debugSplitter.TabIndex = 3;
			this.debugSplitter.TabStop = false;
			// 
			// moveHistoryPanel
			// 
			this.moveHistoryPanel.Controls.Add(this.moveHistoryList);
			this.moveHistoryPanel.Dock = DockStyle.Right;
			this.moveHistoryPanel.Location = new Point(408, 75);
			this.moveHistoryPanel.Name = "moveHistoryPanel";
			this.moveHistoryPanel.Size = new Size(328, 335);
			this.moveHistoryPanel.TabIndex = 4;
			// 
			// moveHistoryList
			// 
			this.moveHistoryList.Columns.AddRange(new ColumnHeader[] {
            this.moveColumn,
            this.playerColumn,
            this.sourceColumn,
            this.destinationColumn});
			this.moveHistoryList.ContextMenu = this.undoContextMenu;
			this.moveHistoryList.Dock = DockStyle.Fill;
			this.moveHistoryList.FullRowSelect = true;
			this.moveHistoryList.GridLines = true;
			this.moveHistoryList.Location = new Point(0, 0);
			this.moveHistoryList.MultiSelect = false;
			this.moveHistoryList.Name = "moveHistoryList";
			this.moveHistoryList.Size = new Size(328, 335);
			this.moveHistoryList.TabIndex = 0;
			this.moveHistoryList.UseCompatibleStateImageBehavior = false;
			this.moveHistoryList.View = View.Details;
			// 
			// moveColumn
			// 
			this.moveColumn.Text = "Move";
			// 
			// playerColumn
			// 
			this.playerColumn.Text = "Player";
			this.playerColumn.TextAlign = HorizontalAlignment.Center;
			// 
			// sourceColumn
			// 
			this.sourceColumn.Text = "Source";
			this.sourceColumn.TextAlign = HorizontalAlignment.Center;
			this.sourceColumn.Width = 100;
			// 
			// destinationColumn
			// 
			this.destinationColumn.Text = "Destination";
			this.destinationColumn.TextAlign = HorizontalAlignment.Center;
			this.destinationColumn.Width = 100;
			// 
			// undoContextMenu
			// 
			this.undoContextMenu.MenuItems.AddRange(new MenuItem[] {
            this.undoToPointContextMenu});
			// 
			// undoToPointContextMenu
			// 
			this.undoToPointContextMenu.Index = 0;
			this.undoToPointContextMenu.Text = "Undo to This Point...";
			this.undoToPointContextMenu.Click += new EventHandler(this.OnUndoToPointContextMenuClick);
			// 
			// moveHistorySplitter
			// 
			this.moveHistorySplitter.Dock = DockStyle.Right;
			this.moveHistorySplitter.Location = new Point(405, 75);
			this.moveHistorySplitter.Name = "moveHistorySplitter";
			this.moveHistorySplitter.Size = new Size(3, 335);
			this.moveHistorySplitter.TabIndex = 5;
			this.moveHistorySplitter.TabStop = false;
			// 
			// boardPanel
			// 
			this.boardPanel.Controls.Add(this.board);
			this.boardPanel.Dock = DockStyle.Fill;
			this.boardPanel.Location = new Point(0, 75);
			this.boardPanel.Name = "boardPanel";
			this.boardPanel.Size = new Size(405, 335);
			this.boardPanel.TabIndex = 6;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new MenuItem[] {
            this.fileMenuItem,
            this.gameMenuItem,
            this.helpMenu});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new MenuItem[] {
            this.fileOpenMenuItem,
            this.fileSaveMenuItem,
            this.separator2MenuItem,
            this.printMenuItem,
            this.separator1MenuItem,
            this.fileExitMenuItem});
			this.fileMenuItem.Text = "&File";
			// 
			// fileOpenMenuItem
			// 
			this.fileOpenMenuItem.Index = 0;
			this.fileOpenMenuItem.Text = "&Open";
			this.fileOpenMenuItem.Click += new EventHandler(this.OnFileOpenMenuItemClick);
			// 
			// fileSaveMenuItem
			// 
			this.fileSaveMenuItem.Index = 1;
			this.fileSaveMenuItem.Text = "&Save";
			this.fileSaveMenuItem.Click += new EventHandler(this.OnFileSaveMenuItemClick);
			// 
			// separator2MenuItem
			// 
			this.separator2MenuItem.Index = 2;
			this.separator2MenuItem.Text = "-";
			// 
			// printMenuItem
			// 
			this.printMenuItem.Index = 3;
			this.printMenuItem.Text = "&Print";
			// 
			// separator1MenuItem
			// 
			this.separator1MenuItem.Index = 4;
			this.separator1MenuItem.Text = "-";
			// 
			// fileExitMenuItem
			// 
			this.fileExitMenuItem.Index = 5;
			this.fileExitMenuItem.Text = "E&xit";
			// 
			// gameMenuItem
			// 
			this.gameMenuItem.Index = 1;
			this.gameMenuItem.MenuItems.AddRange(new MenuItem[] {
            this.gameResetMenuItem});
			this.gameMenuItem.Text = "&Game";
			// 
			// gameResetMenuItem
			// 
			this.gameResetMenuItem.Index = 0;
			this.gameResetMenuItem.Text = "&Reset...";
			this.gameResetMenuItem.Click += new EventHandler(this.OnGameResetMenuItemClick);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 2;
			this.helpMenu.MenuItems.AddRange(new MenuItem[] {
            this.aboutMenu});
			this.helpMenu.Text = "&Help";
			// 
			// aboutMenu
			// 
			this.aboutMenu.Index = 0;
			this.aboutMenu.Text = "&About...";
			this.aboutMenu.Click += new EventHandler(this.OnAboutMenuClick);
			// 
			// board
			// 
			this.board.BackColor = Color.FromArgb(0, 64, 64);
			this.board.Dock = DockStyle.Fill;
			this.board.Location = new Point(0, 0);
			this.board.Name = "board";
			this.board.Size = new Size(405, 335);
			this.board.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new Size(5, 14);
			this.ClientSize = new Size(736, 513);
			this.Controls.Add(this.boardPanel);
			this.Controls.Add(this.moveHistorySplitter);
			this.Controls.Add(this.moveHistoryPanel);
			this.Controls.Add(this.debugSplitter);
			this.Controls.Add(this.debugPanel);
			this.Controls.Add(this.gameStatisticsSplitter);
			this.Controls.Add(this.gameStatisticsPanel);
			this.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Quixo .NET";
			this.gameStatisticsPanel.ResumeLayout(false);
			this.debugPanel.ResumeLayout(false);
			this.debugPanel.PerformLayout();
			this.moveHistoryPanel.ResumeLayout(false);
			this.boardPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnAboutMenuClick(object sender, EventArgs e)
		{
			var about = new AboutDialog();
			about.ShowDialog(this);
		}

		private void OnFileOpenMenuItemClick(object sender, EventArgs e)
		{
			var openDialog = new OpenFileDialog
			{
				Filter = "Quixo files (*.quixo)|*.quixo",
				FilterIndex = 1,
				RestoreDirectory = true
			};

			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (var quixoStream = openDialog.OpenFile())
					{
						IFormatter formatter = new QF.BoardFormatter();
						var board = (QF.Board)formatter.Deserialize(quixoStream);
						this.board.Reset(board, null, null);
						this.debugTextBox.Text = string.Empty;
						this.UpdateGameStatus();
					}
				}
				catch (ArgumentNullException) { }
				catch (SerializationException) { }
			}
		}

		private void OnFileSaveMenuItemClick(object sender, EventArgs e)
		{
			var saveDialog = new SaveFileDialog
			{
				Filter = "Quixo files (*.quixo)|*.quixo",
				FilterIndex = 1,
				RestoreDirectory = true
			};

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (var quixoStream = saveDialog.OpenFile())
					{
						IFormatter formatter = new QF.BoardFormatter();
						formatter.Serialize(quixoStream, this.board.InternalBoard);
					}
				}
				catch (ArgumentNullException) { }
				catch (SerializationException) { }
			}
		}

		private void OnGameResetMenuItemClick(object sender, EventArgs e)
		{
			var options = new ResetOptions
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

		private void OnMoveMade(object sender, EventArgs args) => this.UpdateGameStatus();

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
						 new string[] { (i + 1).ToString(), move.Player.ToString(), move.Source.ToString(), move.Destination.ToString() });
					this.moveHistoryList.Items.Add(moveItem);
				}
			}

			this.moveHistoryList.ResumeLayout();
		}
	}
}
