using Quixo.Controls;
using System;
using System.Windows.Forms;

namespace Quixo
{
	sealed class Entry
	{
		private Entry() : base() { }

		[STAThread()]
		static void Main()
		{
			Application.Run(new MainForm());
		}
	}
}
