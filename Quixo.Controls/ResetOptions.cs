using Quixo.Engine;
using Quixo.SmartEngine;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public sealed class ResetOptions : Form
	{
		public const string Human = "Human";
		private IEngine playerX = null;
		private IEngine playerO = null;
		private TextBox debugText = null;

		private Label playerXLabel;
		private ListBox playerXList;
		private Button okButton;
		private Button cancelButton;
		private ListBox playerOList;
		private Label playerOLabel;
		private readonly Container components = null;

		public ResetOptions()
		{
			this.InitializeComponent();
			this.InitializeEngineLists();
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
			this.playerXLabel = new Label();
			this.playerXList = new ListBox();
			this.playerOList = new ListBox();
			this.playerOLabel = new Label();
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.SuspendLayout();
			// 
			// playerXLabel
			// 
			this.playerXLabel.Location = new Point(8, 8);
			this.playerXLabel.Name = "playerXLabel";
			this.playerXLabel.Size = new Size(56, 16);
			this.playerXLabel.TabIndex = 0;
			this.playerXLabel.Text = "Player &X:";
			// 
			// playerXList
			// 
			this.playerXList.IntegralHeight = false;
			this.playerXList.Location = new Point(8, 24);
			this.playerXList.Name = "playerXList";
			this.playerXList.ScrollAlwaysVisible = true;
			this.playerXList.Size = new Size(240, 232);
			this.playerXList.TabIndex = 1;
			// 
			// playerOList
			// 
			this.playerOList.IntegralHeight = false;
			this.playerOList.Location = new Point(256, 24);
			this.playerOList.Name = "playerOList";
			this.playerOList.ScrollAlwaysVisible = true;
			this.playerOList.Size = new Size(240, 232);
			this.playerOList.TabIndex = 3;
			// 
			// playerOLabel
			// 
			this.playerOLabel.Location = new Point(256, 8);
			this.playerOLabel.Name = "playerOLabel";
			this.playerOLabel.Size = new Size(56, 16);
			this.playerOLabel.TabIndex = 2;
			this.playerOLabel.Text = "Player &O:";
			// 
			// okButton
			// 
			this.okButton.Location = new Point(424, 264);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 5;
			this.okButton.Text = "&OK";
			this.okButton.Click += new EventHandler(this.OnOkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(344, 264);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new EventHandler(this.OnCancelButtonClick);
			// 
			// ResetOptions
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new Size(5, 14);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new Size(504, 293);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.playerOList);
			this.Controls.Add(this.playerOLabel);
			this.Controls.Add(this.playerXList);
			this.Controls.Add(this.playerXLabel);
			this.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResetOptions";
			this.StartPosition = FormStartPosition.CenterParent;
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

			this.playerXList.Items.Add(typeof(RandomEngine).AssemblyQualifiedName);
			this.playerOList.Items.Add(typeof(RandomEngine).AssemblyQualifiedName);

			this.playerXList.Items.Add(typeof(AlphaBetaPruningEngine).AssemblyQualifiedName);
			this.playerOList.Items.Add(typeof(AlphaBetaPruningEngine).AssemblyQualifiedName);
		}

		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void OnOkButtonClick(object sender, EventArgs e)
		{
			if (this.playerXList.SelectedIndex >= 0 && this.playerOList.SelectedIndex >= 0)
			{
				var playerXDescription = this.playerXList.SelectedItem as string;
				var playerODescription = this.playerOList.SelectedItem as string;

				if (playerXDescription.Equals(Human) == false && playerODescription.Equals(Human) == false)
				{
					MessageBox.Show("One of the players must be a human.");
				}
				else
				{
					if (!playerXDescription.Equals(Human))
					{
						this.playerX = (IEngine)Activator.CreateInstance(Type.GetType(playerXDescription),
							 new object[] { new DebugTextWriter(this.debugText) });
					}
					else if (!playerODescription.Equals(Human))
					{
						this.playerO = (IEngine)Activator.CreateInstance(Type.GetType(playerODescription),
							 new object[] { new DebugTextWriter(this.debugText) });
					}

					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
		}

		public IEngine PlayerO => this.playerO;

		public IEngine PlayerX => this.playerX;

		internal TextBox DebugText
		{
			get => this.debugText;
			set => this.debugText = value;
		}
	}
}