namespace WinTRM
{
	public class frmVendors
	{
		Vendors[] vends;
		System.Windows.Data.CollectionViewSource VendorsViewSource;

		long currentId;

		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			System.Drawing.Rectangle rect = Utils.getForm(this);
			this.Top = rect.Top;
			this.Left = rect.Left;
			this.Height = rect.Height;
			this.Width = rect.Width;
			VendorsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("VendorsViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//VendorsViewSource.Source = [generic data source]
			vends = Vendors.load;
			VendorsViewSource.Source = vends;

		}



		private void cmdAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string vendName = Interaction.InputBox("New vendor/mechanic name: ");
			if ((vendName != null)) {
				if (!string.IsNullOrEmpty(vendName.Trim)) {
					Vendors vend = new Vendors();
					vend.VendorName = vendName;
					vend.save();
					VendorsViewSource.Source = null;
					vends = Vendors.load;
					VendorsViewSource.Source = vends;
				}
			}

		}


		private void cmdExit_Click(object sender, RoutedEventArgs e)
		{
			try {
				if (vends(currentId).isChanged) {
					if (Interaction.MsgBox("Save your changes?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
						vends(currentId).save();
					}
					vends(currentId).isChanged = false;
				}
			} catch {
			}
			Utils.saveForm(this);
			this.Close();

		}


		private void VendorsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!this.IsLoaded) {
				try {
					if (vends(currentId).isChanged) {
						if (Interaction.MsgBox("Save your changes?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes) {
							vends(currentId).save();
						}
						vends(currentId).isChanged = false;
					}
				} catch {
				}
			}

			currentId = VendorsListView.SelectedIndex;

		}
		public frmVendors()
		{
			Loaded += Window_Loaded;
		}
	}
}
