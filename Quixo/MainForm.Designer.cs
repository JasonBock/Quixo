namespace Quixo
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gameStatisticsPanel = new System.Windows.Forms.Panel();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.moveHistorySplitter = new System.Windows.Forms.Splitter();
			this.boardPanel = new System.Windows.Forms.Panel();
			this.board = new Quixo.Board();
			this.gameStatisticsPanel.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.debugPanel.SuspendLayout();
			this.moveHistoryPanel.SuspendLayout();
			this.boardPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// gameStatisticsPanel
			// 
			this.gameStatisticsPanel.Controls.Add(this.mainMenuStrip);
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
			this.gameStatisticsPanel.Size = new System.Drawing.Size(2135, 217);
			this.gameStatisticsPanel.TabIndex = 0;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(36, 36);
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(2135, 45);
			this.mainMenuStrip.TabIndex = 8;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.printToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(80, 41);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(232, 48);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OnFileOpenMenuItemClick);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(232, 48);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnFileSaveMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(229, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new System.Drawing.Size(232, 48);
			this.printToolStripMenuItem.Text = "Print";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(232, 48);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(109, 41);
			this.gameToolStripMenuItem.Text = "&Game";
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(248, 48);
			this.resetToolStripMenuItem.Text = "Reset...";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.OnGameResetMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(95, 41);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(403, 48);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutMenuClick);
			// 
			// playerOValueLabel
			// 
			this.playerOValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.playerOValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.playerOValueLabel.Location = new System.Drawing.Point(506, 134);
			this.playerOValueLabel.Name = "playerOValueLabel";
			this.playerOValueLabel.Size = new System.Drawing.Size(1617, 49);
			this.playerOValueLabel.TabIndex = 7;
			this.playerOValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// playerXValueLabel
			// 
			this.playerXValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.playerXValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.playerXValueLabel.Location = new System.Drawing.Point(506, 65);
			this.playerXValueLabel.Name = "playerXValueLabel";
			this.playerXValueLabel.Size = new System.Drawing.Size(1617, 49);
			this.playerXValueLabel.TabIndex = 6;
			this.playerXValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// playerOLabel
			// 
			this.playerOLabel.Location = new System.Drawing.Point(429, 134);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new System.Drawing.Size(58, 49);
			this.playerOLabel.TabIndex = 5;
			this.playerOLabel.Text = "O";
			this.playerOLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// playerXLabel
			// 
			this.playerXLabel.Location = new System.Drawing.Point(429, 65);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new System.Drawing.Size(58, 49);
			this.playerXLabel.TabIndex = 4;
			this.playerXLabel.Text = "X";
			this.playerXLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// winningPlayerValueLabel
			// 
			this.winningPlayerValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.winningPlayerValueLabel.Location = new System.Drawing.Point(257, 134);
			this.winningPlayerValueLabel.Name = "winningPlayerValueLabel";
			this.winningPlayerValueLabel.Size = new System.Drawing.Size(134, 49);
			this.winningPlayerValueLabel.TabIndex = 3;
			this.winningPlayerValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// currentPlayerValueLabel
			// 
			this.currentPlayerValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.currentPlayerValueLabel.Location = new System.Drawing.Point(257, 65);
			this.currentPlayerValueLabel.Name = "currentPlayerValueLabel";
			this.currentPlayerValueLabel.Size = new System.Drawing.Size(134, 49);
			this.currentPlayerValueLabel.TabIndex = 2;
			this.currentPlayerValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// winningPlayerLabel
			// 
			this.winningPlayerLabel.Location = new System.Drawing.Point(26, 134);
			this.winningPlayerLabel.Name = "winningPlayerLabel";
			this.winningPlayerLabel.Size = new System.Drawing.Size(211, 49);
			this.winningPlayerLabel.TabIndex = 1;
			this.winningPlayerLabel.Text = "Winning Player";
			this.winningPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// currentPlayerLabel
			// 
			this.currentPlayerLabel.Location = new System.Drawing.Point(26, 65);
			this.currentPlayerLabel.Name = "currentPlayerLabel";
			this.currentPlayerLabel.Size = new System.Drawing.Size(211, 49);
			this.currentPlayerLabel.TabIndex = 0;
			this.currentPlayerLabel.Text = "Current Player";
			this.currentPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gameStatisticsSplitter
			// 
			this.gameStatisticsSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.gameStatisticsSplitter.Location = new System.Drawing.Point(0, 217);
			this.gameStatisticsSplitter.Name = "gameStatisticsSplitter";
			this.gameStatisticsSplitter.Size = new System.Drawing.Size(2135, 7);
			this.gameStatisticsSplitter.TabIndex = 1;
			this.gameStatisticsSplitter.TabStop = false;
			// 
			// debugPanel
			// 
			this.debugPanel.Controls.Add(this.debugTextBox);
			this.debugPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.debugPanel.Location = new System.Drawing.Point(0, 1251);
			this.debugPanel.Name = "debugPanel";
			this.debugPanel.Size = new System.Drawing.Size(2135, 214);
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
			this.debugTextBox.Size = new System.Drawing.Size(2135, 214);
			this.debugTextBox.TabIndex = 0;
			// 
			// debugSplitter
			// 
			this.debugSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.debugSplitter.Location = new System.Drawing.Point(0, 1245);
			this.debugSplitter.Name = "debugSplitter";
			this.debugSplitter.Size = new System.Drawing.Size(2135, 6);
			this.debugSplitter.TabIndex = 3;
			this.debugSplitter.TabStop = false;
			// 
			// moveHistoryPanel
			// 
			this.moveHistoryPanel.Controls.Add(this.moveHistoryList);
			this.moveHistoryPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.moveHistoryPanel.Location = new System.Drawing.Point(1348, 224);
			this.moveHistoryPanel.Name = "moveHistoryPanel";
			this.moveHistoryPanel.Size = new System.Drawing.Size(787, 1021);
			this.moveHistoryPanel.TabIndex = 4;
			// 
			// moveHistoryList
			// 
			this.moveHistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.moveColumn,
            this.playerColumn,
            this.sourceColumn,
            this.destinationColumn});
			this.moveHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.moveHistoryList.FullRowSelect = true;
			this.moveHistoryList.GridLines = true;
			this.moveHistoryList.Location = new System.Drawing.Point(0, 0);
			this.moveHistoryList.MultiSelect = false;
			this.moveHistoryList.Name = "moveHistoryList";
			this.moveHistoryList.Size = new System.Drawing.Size(787, 1021);
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
			// moveHistorySplitter
			// 
			this.moveHistorySplitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.moveHistorySplitter.Location = new System.Drawing.Point(1341, 224);
			this.moveHistorySplitter.Name = "moveHistorySplitter";
			this.moveHistorySplitter.Size = new System.Drawing.Size(7, 1021);
			this.moveHistorySplitter.TabIndex = 5;
			this.moveHistorySplitter.TabStop = false;
			// 
			// boardPanel
			// 
			this.boardPanel.Controls.Add(this.board);
			this.boardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.boardPanel.Location = new System.Drawing.Point(0, 224);
			this.boardPanel.Name = "boardPanel";
			this.boardPanel.Size = new System.Drawing.Size(1341, 1021);
			this.boardPanel.TabIndex = 6;
			// 
			// board
			// 
			this.board.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.board.Dock = System.Windows.Forms.DockStyle.Fill;
			this.board.Location = new System.Drawing.Point(0, 0);
			this.board.MoveMade = null;
			this.board.Name = "board";
			this.board.Size = new System.Drawing.Size(1341, 1021);
			this.board.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(12, 30);
			this.ClientSize = new System.Drawing.Size(2135, 1465);
			this.Controls.Add(this.boardPanel);
			this.Controls.Add(this.moveHistorySplitter);
			this.Controls.Add(this.moveHistoryPanel);
			this.Controls.Add(this.debugSplitter);
			this.Controls.Add(this.debugPanel);
			this.Controls.Add(this.gameStatisticsSplitter);
			this.Controls.Add(this.gameStatisticsPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quixo .NET";
			this.gameStatisticsPanel.ResumeLayout(false);
			this.gameStatisticsPanel.PerformLayout();
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.debugPanel.ResumeLayout(false);
			this.debugPanel.PerformLayout();
			this.moveHistoryPanel.ResumeLayout(false);
			this.boardPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Panel gameStatisticsPanel;
		private Splitter gameStatisticsSplitter;
		private Panel debugPanel;
		private Splitter debugSplitter;
		private Panel moveHistoryPanel;
		private Splitter moveHistorySplitter;
		private Panel boardPanel;
		private Quixo.Board board;
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
		private MenuStrip mainMenuStrip;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem openToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem printToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem exitToolStripMenuItem;
		private ToolStripMenuItem gameToolStripMenuItem;
		private ToolStripMenuItem resetToolStripMenuItem;
		private ToolStripMenuItem helpToolStripMenuItem;
		private ToolStripMenuItem aboutToolStripMenuItem;
	}
}