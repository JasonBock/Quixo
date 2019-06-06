using System;
using System.Diagnostics;
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
			this.versionLabel = new Label();
			this.rulesLinkLabel = new LinkLabel();
			this.rulesLabel = new Label();
			this.okButton = new Button();
			this.SuspendLayout();
			// 
			// versionLabel
			// 
			this.versionLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
				 | AnchorStyles.Right);
			this.versionLabel.Location = new Point(8, 8);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new Size(384, 23);
			this.versionLabel.TabIndex = 0;
			this.versionLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// rulesLinkLabel
			// 
			this.rulesLinkLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
				 | AnchorStyles.Right);
			this.rulesLinkLabel.Location = new Point(8, 72);
			this.rulesLinkLabel.Name = "rulesLinkLabel";
			this.rulesLinkLabel.Size = new Size(384, 23);
			this.rulesLinkLabel.TabIndex = 1;
			this.rulesLinkLabel.TabStop = true;
			this.rulesLinkLabel.Text = "http://www.gigamic.com/regles/anglais/rquixoe.htm";
			this.rulesLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
			this.rulesLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.OnRulesLinkLabelLinkClicked);
			// 
			// rulesLabel
			// 
			this.rulesLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
				 | AnchorStyles.Right);
			this.rulesLabel.Location = new Point(8, 40);
			this.rulesLabel.Name = "rulesLabel";
			this.rulesLabel.Size = new Size(384, 23);
			this.rulesLabel.TabIndex = 2;
			this.rulesLabel.Text = "Rules can be found at:";
			this.rulesLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// okButton
			// 
			this.okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.okButton.Location = new Point(320, 106);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.Click += new EventHandler(this.OnOkButtonClick);
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new Size(6, 16);
			this.ClientSize = new Size(400, 135);
			this.ControlBox = false;
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.rulesLabel);
			this.Controls.Add(this.rulesLinkLabel);
			this.Controls.Add(this.versionLabel);
			this.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "About Quixo .NET";
			this.ResumeLayout(false);

		}
		#endregion

		private void OnOkButtonClick(object sender, EventArgs e) => this.Close();

		private void OnRulesLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(this.rulesLinkLabel.Text);
			}
			catch { }
		}
	}
}
