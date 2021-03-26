using System.Windows.Forms;

using DarkUI.Renderers;

namespace DarkUI.Controls {
	public class DarkContextMenu : ContextMenuStrip {
		#region Constructor Region

		public DarkContextMenu() {
			Renderer = new DarkMenuRenderer();
		}

		#endregion
	}
}
