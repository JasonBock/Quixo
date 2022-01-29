using System.Diagnostics;
using System.Reflection;

namespace Quixo;

public partial class AboutDialog 
	: Form
{
	public AboutDialog()
	{
		this.InitializeComponent();
		this.versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
	}

	private void OnOkButtonClick(object? sender, EventArgs e) => this.Close();
}