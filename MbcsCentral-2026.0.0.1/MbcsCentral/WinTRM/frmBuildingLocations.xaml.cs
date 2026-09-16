namespace WinTRM
{
	public class frmBuildingLocations
	{

		private BuildingLocation objLocationToEdit;
		private BuildingLocation[] locations;

		System.Windows.Data.CollectionViewSource BuildingLocationViewSource;

		private void frmBuildingLocations_Closed(object sender, System.EventArgs e)
		{
			Utils.saveForm(this);

		}


		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			BuildingLocationViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("BuildingLocationViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//BuildingLocationViewSource.Source = [generic data source]
			locations = BuildingLocation.load();
			BuildingLocationViewSource.Source = locations;

			System.Drawing.Rectangle rect = Utils.getForm(this);
			this.Top = rect.Top;
			this.Left = rect.Left;
			this.Height = rect.Height;
			this.Width = rect.Width;

		}


		private void BuildingLocationDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			objLocationToEdit = (BuildingLocation)BuildingLocationDataGrid.SelectedItem;

		}


		private void cmdDelete_Click(object sender, RoutedEventArgs e)
		{
			if ((objLocationToEdit != null)) {
				objLocationToEdit.delete(objLocationToEdit.ID);
				BuildingLocationViewSource = null;
				locations = BuildingLocation.load();
				BuildingLocationViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("BuildingLocationViewSource");
				BuildingLocationViewSource.Source = locations;
			}

		}


		private void cmdAdd_Click(object sender, RoutedEventArgs e)
		{
			string s = Interaction.InputBox("New building location:");
			if (Interaction.MsgBox("Add <" + s + "> to building locations?", MsgBoxStyle.YesNo)) {
				BuildingLocation l = new BuildingLocation(0, s);
				l.save();
				BuildingLocationViewSource = null;
				locations = BuildingLocation.load();
				BuildingLocationViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("BuildingLocationViewSource");
				BuildingLocationViewSource.Source = locations;

			}
		}


		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();

		}
		public frmBuildingLocations()
		{
			Loaded += Window_Loaded;
			Closed += frmBuildingLocations_Closed;
		}
	}
}
