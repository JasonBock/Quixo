using System.Globalization;
using System.Text;

namespace Quixo;

internal sealed class DebugTextWriter
	: TextWriter
{
	private readonly TextBox debugText;

	public DebugTextWriter(TextBox debugText)
		: base(CultureInfo.CurrentCulture) =>
			this.debugText = debugText ?? throw new ArgumentNullException(nameof(debugText));

	public override void WriteLine(string? value)
	{
		this.debugText.Text += value;
		this.debugText.Text += Environment.NewLine;
		this.debugText.SelectionStart = this.debugText.Text.Length - 1;
		this.debugText.ScrollToCaret();
	}

	public override Encoding Encoding => Encoding.Unicode;
}