using Quixo.Engine;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public sealed class ResetOptions : System.Windows.Forms.Form
	{
		public const string Human = "Human";
		private ArrayList engines = null;
		private IEngine playerX = null;
		private IEngine playerO = null;
		private TextBox debugText = null;

		private System.Windows.Forms.Label playerXLabel;
		private System.Windows.Forms.ListBox playerXList;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ListBox playerOList;
		private System.Windows.Forms.Label playerOLabel;
		private System.ComponentModel.Container components = null;

		public ResetOptions()
		{
			this.InitializeComponent();
			this.InitializeEngineLists();
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
			this.playerXLabel = new System.Windows.Forms.Label();
			this.playerXList = new System.Windows.Forms.ListBox();
			this.playerOList = new System.Windows.Forms.ListBox();
			this.playerOLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// playerXLabel
			// 
			this.playerXLabel.Location = new System.Drawing.Point(8, 8);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new System.Drawing.Size(56, 16);
			this.playerXLabel.TabIndex = 0;
			this.playerXLabel.Text = "Player &X:";
			// 
			// playerXList
			// 
			this.playerXList.IntegralHeight = false;
			this.playerXList.Location = new System.Drawing.Point(8, 24);
			this.playerXList.Name = "playerXList";
			this.playerXList.ScrollAlwaysVisible = true;
			this.playerXList.Size = new System.Drawing.Size(240, 232);
			this.playerXList.TabIndex = 1;
			// 
			// playerOList
			// 
			this.playerOList.IntegralHeight = false;
			this.playerOList.Location = new System.Drawing.Point(256, 24);
			this.playerOList.Name = "playerOList";
			this.playerOList.ScrollAlwaysVisible = true;
			this.playerOList.Size = new System.Drawing.Size(240, 232);
			this.playerOList.TabIndex = 3;
			// 
			// playerOLabel
			// 
			this.playerOLabel.Location = new System.Drawing.Point(256, 8);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new System.Drawing.Size(56, 16);
			this.playerOLabel.TabIndex = 2;
			this.playerOLabel.Text = "Player &O:";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(424, 264);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 5;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(344, 264);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
			// 
			// ResetOptions
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(504, 293);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.playerOList);
			this.Controls.Add(this.playerOLabel);
			this.Controls.Add(this.playerXList);
			this.Controls.Add(this.playerXLabel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResetOptions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Reset Options";
			this.ResumeLayout(false);

		}
		#endregion

		private void InitializeEngineLists()
		{
			this.playerX = null;
			this.playerO = null;

			this.playerXList.Items.Add(Human);
			this.playerOList.Items.Add(Human);

			this.engines = ConfigurationManager.GetSection("QuixoEngines") as ArrayList;

			if (this.engines != null)
			{
				foreach (Type engineType in this.engines)
				{
					this.playerXList.Items.Add(engineType.FullName);
					this.playerOList.Items.Add(engineType.FullName);
				}
			}
		}

		private void OnCancelButtonClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOkButtonClick(object sender, System.EventArgs e)
		{
			if (this.playerXList.SelectedIndex >= 0 && this.playerOList.SelectedIndex >= 0)
			{
				string playerXDescription = this.playerXList.SelectedItem as string;
				string playerODescription = this.playerOList.SelectedItem as string;

				if (playerXDescription.Equals(Human) == false && playerODescription.Equals(Human) == false)
				{
					MessageBox.Show("One of the players must be a human.");
				}
				else
				{
					if (playerXDescription.Equals(Human) == false && this.engines != null)
					{
						this.playerX = (IEngine)Activator.CreateInstance(this.engines[this.playerXList.SelectedIndex - 1] as Type,
							 new object[] { new DebugTextWriter(this.debugText) });
					}
					else if (playerODescription.Equals(Human) == false && this.engines != null)
					{
						this.playerO = (IEngine)Activator.CreateInstance(this.engines[this.playerOList.SelectedIndex - 1] as Type,
							 new object[] { new DebugTextWriter(this.debugText) });
					}

					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
		}

		public IEngine PlayerO
		{
			get
			{
				return this.playerO;
			}
		}

		public IEngine PlayerX
		{
			get
			{
				return this.playerX;
			}
		}

		internal TextBox DebugText
		{
			get
			{
				return this.debugText;
			}
			set
			{
				this.debugText = value;
			}
		}
	}
}