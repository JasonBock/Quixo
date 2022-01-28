namespace Quixo;

public partial class AboutDialog : Form
{
	public AboutDialog() => this.InitializeComponent();

	private void OnOkButtonClick(object? sender, EventArgs e) => this.Close();
}
