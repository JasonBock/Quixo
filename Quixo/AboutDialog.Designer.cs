namespace Quixo
{
   partial class AboutDialog
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
			this.versionLabel = new System.Windows.Forms.Label();
			this.rulesLabel = new System.Windows.Forms.Label();
			this.rulesLinkLabel = new System.Windows.Forms.LinkLabel();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// versionLabel
			// 
			this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.versionLabel.Location = new System.Drawing.Point(12, 9);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(547, 34);
			this.versionLabel.TabIndex = 1;
			this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rulesLabel
			// 
			this.rulesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rulesLabel.Location = new System.Drawing.Point(12, 61);
			this.rulesLabel.Name = "rulesLabel";
			this.rulesLabel.Size = new System.Drawing.Size(547, 34);
			this.rulesLabel.TabIndex = 3;
			this.rulesLabel.Text = "Rules can be found at:";
			this.rulesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rulesLinkLabel
			// 
			this.rulesLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rulesLinkLabel.Location = new System.Drawing.Point(12, 95);
			this.rulesLinkLabel.Name = "rulesLinkLabel";
			this.rulesLinkLabel.Size = new System.Drawing.Size(547, 34);
			this.rulesLinkLabel.TabIndex = 4;
			this.rulesLinkLabel.TabStop = true;
			this.rulesLinkLabel.Text = "http://www.gigamic.com/regles/anglais/rquixoe.htm";
			this.rulesLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(434, 172);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(125, 35);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "&OK";
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 219);
			this.ControlBox = false;
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.rulesLinkLabel);
			this.Controls.Add(this.rulesLabel);
			this.Controls.Add(this.versionLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Quixo .NET";
			this.ResumeLayout(false);

	  }

	  #endregion

	  private Label versionLabel;
	  private Label rulesLabel;
	  private LinkLabel rulesLinkLabel;
	  private Button okButton;
   }
}