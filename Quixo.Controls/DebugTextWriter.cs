using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Quixo.Controls
{
	internal sealed class DebugTextWriter : TextWriter
	{
		private readonly TextBox debugText = null;

		public DebugTextWriter(TextBox debugText)
			: base() => this.debugText = debugText;

		public override void WriteLine(string value)
		{
			this.debugText.Text += value;
			this.debugText.Text += Environment.NewLine;
			this.debugText.SelectionStart = this.debugText.Text.Length - 1;
			this.debugText.ScrollToCaret();
		}

		public override Encoding Encoding => Encoding.Unicode;
	}
}
