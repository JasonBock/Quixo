using QF = Quixo.Framework;
using Quixo.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
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
				if (components != null)
				{
					components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			this.gameStatisticsPanel = new System.Windows.Forms.Panel();
			this.playerOValueLabel = new System.Windows.Forms.Label();
			this.playerXValueLabel = new System.Windows.Forms.Label();
			this.playerOLabel = new System.Windows.Forms.Label();
			this.playerXLabel = new System.Windows.Forms.Label();
			this.winningPlayerValueLabel = new System.Windows.Forms.Label();
			this.currentPlayerValueLabel = new System.Windows.Forms.Label();
			this.winningPlayerLabel = new System.Windows.Forms.Label();
			this.currentPlayerLabel = new System.Windows.Forms.Label();
			this.gameStatisticsSplitter = new System.Windows.Forms.Splitter();
			this.debugPanel = new System.Windows.Forms.Panel();
			this.debugTextBox = new System.Windows.Forms.TextBox();
			this.debugSplitter = new System.Windows.Forms.Splitter();
			this.moveHistoryPanel = new System.Windows.Forms.Panel();
			this.moveHistoryList = new System.Windows.Forms.ListView();
			this.moveColumn = new System.Windows.Forms.ColumnHeader();
			this.playerColumn = new System.Windows.Forms.ColumnHeader();
			this.sourceColumn = new System.Windows.Forms.ColumnHeader();
			this.destinationColumn = new System.Windows.Forms.ColumnHeader();
			this.undoContextMenu = new System.Windows.Forms.ContextMenu();
			this.undoToPointContextMenu = new System.Windows.Forms.MenuItem();
			this.moveHistorySplitter = new System.Windows.Forms.Splitter();
			this.boardPanel = new System.Windows.Forms.Panel();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.fileOpenMenuItem = new System.Windows.Forms.MenuItem();
			this.fileSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.separator2MenuItem = new System.Windows.Forms.MenuItem();
			this.printMenuItem = new System.Windows.Forms.MenuItem();
			this.separator1MenuItem = new System.Windows.Forms.MenuItem();
			this.fileExitMenuItem = new System.Windows.Forms.MenuItem();
			this.gameMenuItem = new System.Windows.Forms.MenuItem();
			this.gameResetMenuItem = new System.Windows.Forms.MenuItem();
			this.helpMenu = new System.Windows.Forms.MenuItem();
			this.aboutMenu = new System.Windows.Forms.MenuItem();
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
			this.gameStatisticsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.gameStatisticsPanel.Location = new System.Drawing.Point(0, 0);
			this.gameStatisticsPanel.Name = "gameStatisticsPanel";
			this.gameStatisticsPanel.Size = new System.Drawing.Size(736, 72);
			this.gameStatisticsPanel.TabIndex = 0;
			// 
			// playerOValueLabel
			// 
			this.playerOValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.playerOValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.playerOValueLabel.Location = new System.Drawing.Point(208, 40);
			this.playerOValueLabel.Name = "playerOValueLabel";
			this.playerOValueLabel.Size = new System.Drawing.Size(520, 23);
			this.playerOValueLabel.TabIndex = 7;
			this.playerOValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// playerXValueLabel
			// 
			this.playerXValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.playerXValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.playerXValueLabel.Location = new System.Drawing.Point(208, 8);
			this.playerXValueLabel.Name = "playerXValueLabel";
			this.playerXValueLabel.Size = new System.Drawing.Size(520, 23);
			this.playerXValueLabel.TabIndex = 6;
			this.playerXValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// playerOLabel
			// 
			this.playerOLabel.Location = new System.Drawing.Point(176, 40);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new System.Drawing.Size(24, 23);
			this.playerOLabel.TabIndex = 5;
			this.playerOLabel.Text = "O";
			this.playerOLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// playerXLabel
			// 
			this.playerXLabel.Location = new System.Drawing.Point(176, 8);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new System.Drawing.Size(24, 23);
			this.playerXLabel.TabIndex = 4;
			this.playerXLabel.Text = "X";
			this.playerXLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// winningPlayerValueLabel
			// 
			this.winningPlayerValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.winningPlayerValueLabel.Location = new System.Drawing.Point(104, 40);
			this.winningPlayerValueLabel.Name = "winningPlayerValueLabel";
			this.winningPlayerValueLabel.Size = new System.Drawing.Size(56, 23);
			this.winningPlayerValueLabel.TabIndex = 3;
			this.winningPlayerValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// currentPlayerValueLabel
			// 
			this.currentPlayerValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.currentPlayerValueLabel.Location = new System.Drawing.Point(104, 8);
			this.currentPlayerValueLabel.Name = "currentPlayerValueLabel";
			this.currentPlayerValueLabel.Size = new System.Drawing.Size(56, 23);
			this.currentPlayerValueLabel.TabIndex = 2;
			this.currentPlayerValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// winningPlayerLabel
			// 
			this.winningPlayerLabel.Location = new System.Drawing.Point(8, 40);
			this.winningPlayerLabel.Name = "winningPlayerLabel";
			this.winningPlayerLabel.Size = new System.Drawing.Size(88, 23);
			this.winningPlayerLabel.TabIndex = 1;
			this.winningPlayerLabel.Text = "Winning Player";
			this.winningPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// currentPlayerLabel
			// 
			this.currentPlayerLabel.Location = new System.Drawing.Point(8, 8);
			this.currentPlayerLabel.Name = "currentPlayerLabel";
			this.currentPlayerLabel.Size = new System.Drawing.Size(88, 23);
			this.currentPlayerLabel.TabIndex = 0;
			this.currentPlayerLabel.Text = "Current Player";
			this.currentPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gameStatisticsSplitter
			// 
			this.gameStatisticsSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.gameStatisticsSplitter.Location = new System.Drawing.Point(0, 72);
			this.gameStatisticsSplitter.Name = "gameStatisticsSplitter";
			this.gameStatisticsSplitter.Size = new System.Drawing.Size(736, 3);
			this.gameStatisticsSplitter.TabIndex = 1;
			this.gameStatisticsSplitter.TabStop = false;
			// 
			// debugPanel
			// 
			this.debugPanel.Controls.Add(this.debugTextBox);
			this.debugPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.debugPanel.Location = new System.Drawing.Point(0, 413);
			this.debugPanel.Name = "debugPanel";
			this.debugPanel.Size = new System.Drawing.Size(736, 100);
			this.debugPanel.TabIndex = 2;
			// 
			// debugTextBox
			// 
			this.debugTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.debugTextBox.Location = new System.Drawing.Point(0, 0);
			this.debugTextBox.MaxLength = 0;
			this.debugTextBox.Multiline = true;
			this.debugTextBox.Name = "debugTextBox";
			this.debugTextBox.ReadOnly = true;
			this.debugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.debugTextBox.Size = new System.Drawing.Size(736, 100);
			this.debugTextBox.TabIndex = 0;
			// 
			// debugSplitter
			// 
			this.debugSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.debugSplitter.Location = new System.Drawing.Point(0, 410);
			this.debugSplitter.Name = "debugSplitter";
			this.debugSplitter.Size = new System.Drawing.Size(736, 3);
			this.debugSplitter.TabIndex = 3;
			this.debugSplitter.TabStop = false;
			// 
			// moveHistoryPanel
			// 
			this.moveHistoryPanel.Controls.Add(this.moveHistoryList);
			this.moveHistoryPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.moveHistoryPanel.Location = new System.Drawing.Point(408, 75);
			this.moveHistoryPanel.Name = "moveHistoryPanel";
			this.moveHistoryPanel.Size = new System.Drawing.Size(328, 335);
			this.moveHistoryPanel.TabIndex = 4;
			// 
			// moveHistoryList
			// 
			this.moveHistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moveColumn,
            this.playerColumn,
            this.sourceColumn,
            this.destinationColumn});
			this.moveHistoryList.ContextMenu = this.undoContextMenu;
			this.moveHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.moveHistoryList.FullRowSelect = true;
			this.moveHistoryList.GridLines = true;
			this.moveHistoryList.Location = new System.Drawing.Point(0, 0);
			this.moveHistoryList.MultiSelect = false;
			this.moveHistoryList.Name = "moveHistoryList";
			this.moveHistoryList.Size = new System.Drawing.Size(328, 335);
			this.moveHistoryList.TabIndex = 0;
			this.moveHistoryList.UseCompatibleStateImageBehavior = false;
			this.moveHistoryList.View = System.Windows.Forms.View.Details;
			// 
			// moveColumn
			// 
			this.moveColumn.Text = "Move";
			// 
			// playerColumn
			// 
			this.playerColumn.Text = "Player";
			this.playerColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sourceColumn
			// 
			this.sourceColumn.Text = "Source";
			this.sourceColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.sourceColumn.Width = 100;
			// 
			// destinationColumn
			// 
			this.destinationColumn.Text = "Destination";
			this.destinationColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.destinationColumn.Width = 100;
			// 
			// undoContextMenu
			// 
			this.undoContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.undoToPointContextMenu});
			// 
			// undoToPointContextMenu
			// 
			this.undoToPointContextMenu.Index = 0;
			this.undoToPointContextMenu.Text = "Undo to This Point...";
			this.undoToPointContextMenu.Click += new System.EventHandler(this.OnUndoToPointContextMenuClick);
			// 
			// moveHistorySplitter
			// 
			this.moveHistorySplitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.moveHistorySplitter.Location = new System.Drawing.Point(405, 75);
			this.moveHistorySplitter.Name = "moveHistorySplitter";
			this.moveHistorySplitter.Size = new System.Drawing.Size(3, 335);
			this.moveHistorySplitter.TabIndex = 5;
			this.moveHistorySplitter.TabStop = false;
			// 
			// boardPanel
			// 
			this.boardPanel.Controls.Add(this.board);
			this.boardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.boardPanel.Location = new System.Drawing.Point(0, 75);
			this.boardPanel.Name = "boardPanel";
			this.boardPanel.Size = new System.Drawing.Size(405, 335);
			this.boardPanel.TabIndex = 6;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.gameMenuItem,
            this.helpMenu});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
			this.fileOpenMenuItem.Click += new System.EventHandler(this.OnFileOpenMenuItemClick);
			// 
			// fileSaveMenuItem
			// 
			this.fileSaveMenuItem.Index = 1;
			this.fileSaveMenuItem.Text = "&Save";
			this.fileSaveMenuItem.Click += new System.EventHandler(this.OnFileSaveMenuItemClick);
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
			this.gameMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.gameResetMenuItem});
			this.gameMenuItem.Text = "&Game";
			// 
			// gameResetMenuItem
			// 
			this.gameResetMenuItem.Index = 0;
			this.gameResetMenuItem.Text = "&Reset...";
			this.gameResetMenuItem.Click += new System.EventHandler(this.OnGameResetMenuItemClick);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 2;
			this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenu});
			this.helpMenu.Text = "&Help";
			// 
			// aboutMenu
			// 
			this.aboutMenu.Index = 0;
			this.aboutMenu.Text = "&About...";
			this.aboutMenu.Click += new System.EventHandler(this.OnAboutMenuClick);
			// 
			// board
			// 
			this.board.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.board.Dock = System.Windows.Forms.DockStyle.Fill;
			this.board.Location = new System.Drawing.Point(0, 0);
			this.board.Name = "board";
			this.board.Size = new System.Drawing.Size(405, 335);
			this.board.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(736, 513);
			this.Controls.Add(this.boardPanel);
			this.Controls.Add(this.moveHistorySplitter);
			this.Controls.Add(this.moveHistoryPanel);
			this.Controls.Add(this.debugSplitter);
			this.Controls.Add(this.debugPanel);
			this.Controls.Add(this.gameStatisticsSplitter);
			this.Controls.Add(this.gameStatisticsPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quixo .NET";
			this.gameStatisticsPanel.ResumeLayout(false);
			this.debugPanel.ResumeLayout(false);
			this.debugPanel.PerformLayout();
			this.moveHistoryPanel.ResumeLayout(false);
			this.boardPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnAboutMenuClick(object sender, System.EventArgs e)
		{
			AboutDialog about = new AboutDialog();
			about.ShowDialog(this);
		}

		private void OnFileOpenMenuItemClick(object sender, System.EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();

			openDialog.Filter = "Quixo files (*.quixo)|*.quixo";
			openDialog.FilterIndex = 1;
			openDialog.RestoreDirectory = true;

			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (Stream quixoStream = openDialog.OpenFile())
					{
						IFormatter formatter = new QF.BoardFormatter();
						QF.Board board = (QF.Board)formatter.Deserialize(quixoStream);
						this.board.Reset(board, null, null);
						this.debugTextBox.Text = string.Empty;
						this.UpdateGameStatus();
					}
				}
				catch (ArgumentNullException) { }
				catch (SerializationException) { }
			}
		}

		private void OnFileSaveMenuItemClick(object sender, System.EventArgs e)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();

			saveDialog.Filter = "Quixo files (*.quixo)|*.quixo";
			saveDialog.FilterIndex = 1;
			saveDialog.RestoreDirectory = true;

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (Stream quixoStream = saveDialog.OpenFile())
					{
						IFormatter formatter = new QF.BoardFormatter();
						formatter.Serialize(quixoStream, this.board.InternalBoard);
					}
				}
				catch (ArgumentNullException) { }
				catch (SerializationException) { }
			}
		}

		private void OnGameResetMenuItemClick(object sender, System.EventArgs e)
		{
			ResetOptions options = new ResetOptions();
			options.DebugText = this.debugTextBox;
			DialogResult result = options.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				this.board.Reset(null, options.PlayerX, options.PlayerO);
				this.debugTextBox.Text = string.Empty;
				this.UpdateGameStatus();
			}
		}

		private void OnUndoToPointContextMenuClick(object sender, System.EventArgs e)
		{
			if (this.moveHistoryList.SelectedItems.Count == 1)
			{
				this.board.InternalBoard.Undo(this.moveHistoryList.Items.Count - 1 - this.moveHistoryList.SelectedIndices[0]);
				this.board.UpdatePieceStates();
				this.board.RedrawBoard();
				this.UpdateGameStatus();
			}
		}

		private void OnMoveMade(object sender, EventArgs args)
		{
			this.UpdateGameStatus();
		}

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
				for (int i = 0; i < this.board.InternalBoard.Moves.Count; i++)
				{
					QF.Move move = this.board.InternalBoard.Moves[i];
					ListViewItem moveItem = new ListViewItem(
						 new string[] { (i + 1).ToString(), move.Player.ToString(), move.Source.ToString(), move.Destination.ToString() });
					this.moveHistoryList.Items.Add(moveItem);
				}
			}

			this.moveHistoryList.ResumeLayout();
		}
	}
}
