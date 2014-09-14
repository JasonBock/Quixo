using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public sealed class AboutDialog : Form
	{
		private Label versionLabel;
		private Label rulesLabel;
		private Button okButton;
		private LinkLabel rulesLinkLabel;

		public AboutDialog()
		{
			this.InitializeComponent();

			this.versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.versionLabel = new System.Windows.Forms.Label();
			this.rulesLinkLabel = new System.Windows.Forms.LinkLabel();
			this.rulesLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// versionLabel
			// 
			this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				 | System.Windows.Forms.AnchorStyles.Right)));
			this.versionLabel.Location = new System.Drawing.Point(8, 8);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(384, 23);
			this.versionLabel.TabIndex = 0;
			this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rulesLinkLabel
			// 
			this.rulesLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				 | System.Windows.Forms.AnchorStyles.Right)));
			this.rulesLinkLabel.Location = new System.Drawing.Point(8, 72);
			this.rulesLinkLabel.Name = "rulesLinkLabel";
			this.rulesLinkLabel.Size = new System.Drawing.Size(384, 23);
			this.rulesLinkLabel.TabIndex = 1;
			this.rulesLinkLabel.TabStop = true;
			this.rulesLinkLabel.Text = "http://www.gigamic.com/regles/anglais/rquixoe.htm";
			this.rulesLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rulesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnRulesLinkLabelLinkClicked);
			// 
			// rulesLabel
			// 
			this.rulesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				 | System.Windows.Forms.AnchorStyles.Right)));
			this.rulesLabel.Location = new System.Drawing.Point(8, 40);
			this.rulesLabel.Name = "rulesLabel";
			this.rulesLabel.Size = new System.Drawing.Size(384, 23);
			this.rulesLabel.TabIndex = 2;
			this.rulesLabel.Text = "Rules can be found at:";
			this.rulesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(320, 106);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
			this.ClientSize = new System.Drawing.Size(400, 135);
			this.ControlBox = false;
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.rulesLabel);
			this.Controls.Add(this.rulesLinkLabel);
			this.Controls.Add(this.versionLabel);
			this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Quixo .NET";
			this.ResumeLayout(false);

		}
		#endregion

		private void OnOkButtonClick(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void OnRulesLinkLabelLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(this.rulesLinkLabel.Text);
			}
			catch { }
		}
	}
}
