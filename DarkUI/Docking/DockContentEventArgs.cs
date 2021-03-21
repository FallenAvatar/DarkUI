using System;

namespace DarkUI.Docking {
	public class DockContentEventArgs : EventArgs {
		public DarkDockContent Content { get; }

		public DockContentEventArgs(DarkDockContent content) {
			Content = content;
		}
	}
}
