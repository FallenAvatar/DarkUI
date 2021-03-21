using System.Drawing;

namespace DarkUI.Docking {
	internal class DockDropArea {
		#region Property Region

		internal DarkDockPanel DockPanel { get; private set; }

		internal Rectangle DropArea { get; private set; }

		internal Rectangle HighlightArea { get; private set; }

		internal DarkDockRegion? DockRegion { get; private set; }

		internal DarkDockGroup? DockGroup { get; private set; }

		internal DockInsertType InsertType { get; private set; }

		#endregion

		#region Constructor Region

		internal DockDropArea(DarkDockPanel dockPanel, DarkDockRegion region) {
			DockPanel = dockPanel;
			DockRegion = region;
			InsertType = DockInsertType.None;

			BuildAreas();
		}

		internal DockDropArea(DarkDockPanel dockPanel, DarkDockGroup group, DockInsertType insertType) {
			DockPanel = dockPanel;
			DockGroup = group;
			InsertType = insertType;

			BuildAreas();
		}

		#endregion

		#region Method Region

		internal void BuildAreas() {
			if( DockRegion != null )
				BuildRegionAreas();
			else if( DockGroup != null )
				BuildGroupAreas();
		}

		private void BuildRegionAreas() {
			switch( DockRegion?.DockArea ) {
			case DarkDockArea.Left:

				var leftRect = new Rectangle {
					X = DockPanel.PointToScreen(Point.Empty).X,
					Y = DockPanel.PointToScreen(Point.Empty).Y,
					Width = 50,
					Height = DockPanel.Height
				};

				DropArea = leftRect;
				HighlightArea = leftRect;

				break;

			case DarkDockArea.Right:

				var rightRect = new Rectangle {
					X = DockPanel.PointToScreen(Point.Empty).X + DockPanel.Width - 50,
					Y = DockPanel.PointToScreen(Point.Empty).Y,
					Width = 50,
					Height = DockPanel.Height
				};

				DropArea = rightRect;
				HighlightArea = rightRect;

				break;

			case DarkDockArea.Bottom:

				var x = DockPanel.PointToScreen(Point.Empty).X;
				var width = DockPanel.Width;

				if( DockPanel.Regions[DarkDockArea.Left].Visible ) {
					x += DockPanel.Regions[DarkDockArea.Left].Width;
					width -= DockPanel.Regions[DarkDockArea.Left].Width;
				}

				if( DockPanel.Regions[DarkDockArea.Right].Visible ) {
					width -= DockPanel.Regions[DarkDockArea.Right].Width;
				}

				var bottomRect = new Rectangle {
					X = x,
					Y = DockPanel.PointToScreen(Point.Empty).Y + DockPanel.Height - 50,
					Width = width,
					Height = 50
				};

				DropArea = bottomRect;
				HighlightArea = bottomRect;

				break;
			}
		}

		private void BuildGroupAreas() {
			switch( InsertType ) {
			case DockInsertType.None:
				var dropRect = new Rectangle {
					X = DockGroup?.PointToScreen(Point.Empty).X ?? 0,
					Y = DockGroup?.PointToScreen(Point.Empty).Y ?? 0,
					Width = DockGroup?.Width ?? 0,
					Height = DockGroup?.Height ?? 0
				};

				DropArea = dropRect;
				HighlightArea = dropRect;

				break;

			case DockInsertType.Before:
				var beforeDropWidth = DockGroup?.Width ?? 0;
				var beforeDropHeight = DockGroup?.Height ?? 0;

				switch( DockGroup?.DockArea ) {
				case DarkDockArea.Left:
				case DarkDockArea.Right:
					beforeDropHeight = DockGroup.Height / 4;
					break;

				case DarkDockArea.Bottom:
					beforeDropWidth = DockGroup.Width / 4;
					break;
				}

				var beforeDropRect = new Rectangle {
					X = DockGroup?.PointToScreen(Point.Empty).X ?? 0,
					Y = DockGroup?.PointToScreen(Point.Empty).Y ?? 0,
					Width = beforeDropWidth,
					Height = beforeDropHeight
				};

				DropArea = beforeDropRect;
				HighlightArea = beforeDropRect;

				break;

			case DockInsertType.After:
				var afterDropX = DockGroup?.PointToScreen(Point.Empty).X ?? 0;
				var afterDropY = DockGroup?.PointToScreen(Point.Empty).Y ?? 0;
				var afterDropWidth = DockGroup?.Width ?? 0;
				var afterDropHeight = DockGroup?.Height ?? 0;

				switch( DockGroup?.DockArea ) {
				case DarkDockArea.Left:
				case DarkDockArea.Right:
					afterDropHeight = DockGroup.Height / 4;
					afterDropY = DockGroup.PointToScreen(Point.Empty).Y + DockGroup.Height - afterDropHeight;
					break;

				case DarkDockArea.Bottom:
					afterDropWidth = DockGroup.Width / 4;
					afterDropX = DockGroup.PointToScreen(Point.Empty).X + DockGroup.Width - afterDropWidth;
					break;
				}

				var afterDropRect = new Rectangle {
					X = afterDropX,
					Y = afterDropY,
					Width = afterDropWidth,
					Height = afterDropHeight
				};

				DropArea = afterDropRect;
				HighlightArea = afterDropRect;

				break;
			}
		}

		#endregion
	}
}
