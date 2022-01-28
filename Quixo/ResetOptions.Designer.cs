namespace Quixo;

partial class ResetOptions
{
   /// <summary>
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.IContainer components = null;

   /// <summary>
   /// Clean up any resources being used.
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
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {
			this.playerXLabel = new System.Windows.Forms.Label();
			this.playerXList = new System.Windows.Forms.ListBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.playerOList = new System.Windows.Forms.ListBox();
			this.playerOLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// playerXLabel
			// 
			this.playerXLabel.AutoSize = true;
			this.playerXLabel.Location = new System.Drawing.Point(12, 9);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new System.Drawing.Size(118, 37);
			this.playerXLabel.TabIndex = 0;
			this.playerXLabel.Text = "Player X:";
			// 
			// playerXList
			// 
			this.playerXList.FormattingEnabled = true;
			this.playerXList.ItemHeight = 37;
			this.playerXList.Location = new System.Drawing.Point(12, 49);
			this.playerXList.Name = "playerXList";
			this.playerXList.Size = new System.Drawing.Size(599, 559);
			this.playerXList.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(1065, 625);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(169, 52);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(875, 625);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(169, 52);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
			// 
			// playerOList
			// 
			this.playerOList.FormattingEnabled = true;
			this.playerOList.ItemHeight = 37;
			this.playerOList.Location = new System.Drawing.Point(637, 49);
			this.playerOList.Name = "playerOList";
			this.playerOList.Size = new System.Drawing.Size(599, 559);
			this.playerOList.TabIndex = 1;
			// 
			// playerOLabel
			// 
			this.playerOLabel.AutoSize = true;
			this.playerOLabel.Location = new System.Drawing.Point(637, 9);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new System.Drawing.Size(122, 37);
			this.playerOLabel.TabIndex = 4;
			this.playerOLabel.Text = "Player O:";
			// 
			// ResetOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1246, 689);
			this.Controls.Add(this.playerOList);
			this.Controls.Add(this.playerOLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.playerXList);
			this.Controls.Add(this.playerXLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResetOptions";
			this.Text = "ResetOption";
			this.ResumeLayout(false);
			this.PerformLayout();

   }

   #endregion

   private Label playerXLabel;
   private ListBox playerXList;
   private Button okButton;
   private Button cancelButton;
   private ListBox playerOList;
   private Label playerOLabel;
}
