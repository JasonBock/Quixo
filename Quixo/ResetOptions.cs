using Quixo.Engine;
using Quixo.SmartEngine;

namespace Quixo;

public partial class ResetOptions
	: Form
{
	public const string Human = "Human";
	private IEngine? playerX;
	private IEngine? playerO;

	public ResetOptions()
	{
		this.InitializeComponent();
		this.InitializeEngineLists();
	}

	private void InitializeEngineLists()
	{
		this.playerX = null;
		this.playerO = null;

		this.playerXList.Items.Add(Human);
		this.playerOList.Items.Add(Human);

		this.playerXList.Items.Add(typeof(RandomEngine).AssemblyQualifiedName!);
		this.playerOList.Items.Add(typeof(RandomEngine).AssemblyQualifiedName!);

		this.playerXList.Items.Add(typeof(AlphaBetaPruningEngine).AssemblyQualifiedName!);
		this.playerOList.Items.Add(typeof(AlphaBetaPruningEngine).AssemblyQualifiedName!);
	}

	private void OnCancelButtonClick(object? sender, EventArgs e)
	{
		this.DialogResult = DialogResult.Cancel;
		this.Close();
	}

	private void OnOkButtonClick(object? sender, EventArgs e)
	{
		if (this.playerXList.SelectedIndex >= 0 && this.playerOList.SelectedIndex >= 0)
		{
			var playerXDescription = (string)this.playerXList.SelectedItem;
			var playerODescription = (string)this.playerOList.SelectedItem;

			if (!playerXDescription.Equals(Human, StringComparison.Ordinal) && 
				!playerODescription.Equals(Human, StringComparison.Ordinal))
			{
				MessageBox.Show("One of the players must be a human.");
			}
			else
			{
				if (!playerXDescription.Equals(Human, StringComparison.Ordinal))
				{
					this.playerX = (IEngine)Activator.CreateInstance(Type.GetType(playerXDescription)!,
						 new object[] { new DebugTextWriter(this.DebugText!) })!;
				}
				else if (!playerODescription.Equals(Human, StringComparison.Ordinal))
				{
					this.playerO = (IEngine)Activator.CreateInstance(Type.GetType(playerODescription)!,
						 new object[] { new DebugTextWriter(this.DebugText!) })!;
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}

	public IEngine? PlayerO => this.playerO;

	public IEngine? PlayerX => this.playerX;

	internal TextBox? DebugText { get; set; }
}
