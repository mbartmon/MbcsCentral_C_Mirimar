using Microsoft.VisualBasic;
using System;
using MbcsCentral;

namespace MbcsCentral.WinTrm
{
	public partial class frmRecipientMaster
	{

		RecipientMaster[] lists;
		Recipients[] recips;
		WinPM.Building[] buildings;
		Owners[] owners;
		WinPM.Types[] tTypes;
		//Dim changed As Boolean = False
		public long currentId;
		bool loading = true;

		System.Windows.Data.CollectionViewSource RecipientMasterViewSource;

        private string myApp;
        public frmRecipientMaster(string app)
        {
            myApp = app;
            InitializeComponent();
        }

		private void frmRecipientMaster_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( lists != null && lists[currentId].isChanged) {
				switch (Interaction.MsgBox("Do you want to save your changes?", MsgBoxStyle.YesNoCancel)) {
					case MsgBoxResult.Yes:
						if (cmdEditList.Content.ToString().ToLower().Trim().Equals("add")) {
							currentId = lists.Length;
							Array.Resize(ref lists, lists.Length + 2);
							lists[currentId] = new RecipientMaster();
							lists[currentId].listName = ListNameTextBox.Text;
						}

						setActive();
						lists[currentId].accountId = (int)MbcsCentral.GlobalShared.user.Account.accountId;
						lists[currentId].save();
						break;
					case MsgBoxResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if (!e.Cancel) {
				Main.saveForm(this);
			}

		}


		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			Main.getForm(this);

			this.Cursor = System.Windows.Input.Cursors.AppStarting;
			RecipientMasterViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientMasterViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//RecipientMasterViewSource.Source = [generic data source]

			lists = RecipientMaster.load();
			RecipientMasterViewSource.Source = lists;
			loading = false;

			System.Windows.Data.CollectionViewSource BuildingViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("BuildingViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//BuildingViewSource.Source = [generic data source]

			buildings = WinPM.Building.load();
			BuildingViewSource.Source = buildings;

			System.Windows.Data.CollectionViewSource OwnerTableViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("OwnerTableViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//OwnerTableViewSource.Source = [generic data source]

			owners = Owners.load();
			OwnerTableViewSource.Source = owners;

			System.Windows.Data.CollectionViewSource typesViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("typesViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//typesViewSource.Source = [generic data source]

			tTypes = WinPM.Types.loadAll();
			typesViewSource.Source = tTypes;

			fillActive();
			this.Cursor  = System.Windows.Input.Cursors.Arrow;

		}


		private void ComboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!loading) {
				try {
					if (lists[currentId].isChanged) {
						switch (Interaction.MsgBox("Do you want to save changed to previous list?", MsgBoxStyle.YesNo)) {
							case MsgBoxResult.Yes:
								if (cmdEditList.Content.ToString().ToLower().Trim().Equals("add")) {
									currentId = lists.Length;
									Array.Resize(ref lists, lists.Length + 2);
									lists[currentId] = new RecipientMaster();
									lists[currentId].listName = ListNameTextBox.Text;
								}
								setActive();
								lists[currentId].accountId = (int)MbcsCentral.GlobalShared.user.Account.accountId;
								lists[currentId].save();
								break;
							case MsgBoxResult.No:
								break;
							//lists[currentId] = RecipientMaster.load(lists[currentId].listId)
						}
						lists[currentId].isChanged = false;
					}
				} catch {
					currentId = 0;
				}
                cmdEditList.Visibility = System.Windows.Visibility.Collapsed;
			}
			currentId = (lstLists.SelectedIndex >= 0 ? lstLists.SelectedIndex : 0);
			System.Windows.Data.CollectionViewSource RecipientsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientsViewSource");
			RecipientsViewSource.Source = null;
			recips = Recipients.loadByList(lists[currentId].listId);
			RecipientsViewSource.Source = recips;

			fillActive();

		}


		private void fillActive()
		{
            // Fill in the Active listboxes
            if (lists == null)
                return;
			lstActiveOwners.Items.Clear();
			if (!string.IsNullOrEmpty(lists[currentId].companyIds.Trim())) {
				string[] s = lists[currentId].companyIds.Split(',');
				if ((s != null)) {
					foreach (string x in s) {
						foreach (Owners o in lstOwners.Items) {
							if (Conversion.Val(o.OwnerNumber) == Conversion.Val(x)) {
								lstActiveOwners.Items.Add(o);
							}
						}
					}
				}
			}
			lstActiveBuildings.Items.Clear();
			if (!string.IsNullOrEmpty(lists[currentId].buildingIds.Trim())) {
				string[] s = lists[currentId].buildingIds.Split(',');
				if ((s != null)) {
					foreach (string x in s) {
						foreach (WinPM.Building b in lstBuildings.Items) {
							try {
								if (Conversion.Val(b.BuildingNumber) == Conversion.Val(x)) {
									lstActiveBuildings.Items.Add(b);
								}
							} catch {
							}
						}
					}
				}
			}
			lstActiveTypes.Items.Clear();
			if (!string.IsNullOrEmpty(lists[currentId].tenantTypes.Trim())) {
				string[] s = lists[currentId].tenantTypes.Split(',');
				if ((s != null)) {
					foreach (string x in s) {
						foreach (WinPM.Types t in lstTypes.Items) {
							if (t.Type.ToLower().Trim() == x.ToLower().Trim()) {
								lstActiveTypes.Items.Add(t);
							}
						}
					}
				}
			}

		}


		private void cmdEditList_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			setActive();
			lists[currentId].accountId = (int)MbcsCentral.GlobalShared.user.Account.accountId;
			lists[currentId].save();
			lists[currentId].isChanged = false;
            cmdEditList.Visibility = System.Windows.Visibility.Collapsed;

		}


		private void setActive()
		{
			string s = "";
			foreach (Owners o in lstActiveOwners.Items) {
				s += (s.Length > 0 ? "," : "").ToString() + o.OwnerNumber.ToString();
			}
			lists[currentId].companyIds = s;
			s = "";
			foreach (WinPM.Building b in lstActiveBuildings.Items) {
				s += (s.Length > 0 ? "," : "").ToString() + b.BuildingNumber;
			}
			lists[currentId].buildingIds = s;
			s = "";
			foreach (WinPM.Types t in lstActiveTypes.Items) {
				s += (s.Length > 0 ? "," : "").ToString() + t.Type;
			}
			lists[currentId].tenantTypes = s;

		}


		private void ListNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (!loading) {
				try {
					cmdEditList.Content = "SAVE";
					cmdEditList.Visibility = System.Windows.Visibility.Visible;
				} catch {
				}
			}

		}

		private void cmdExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}


		private void cmdAddOwner_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstOwners.SelectedItems.Count > 0) {
				foreach (Owners l in lstOwners.SelectedItems) {
					if (!lstActiveOwners.Items.Contains(l)) {
						lstActiveOwners.Items.Add(l);
					}
				}
				try {
					cmdEditList.Content = "SAVE";
					cmdEditList.Visibility = System.Windows.Visibility.Visible;
				} catch {
				}
				lists[currentId].isChanged = true;
			}

		}


		private void cmdRemoveOwner_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			for (int i = 0; i <= lstActiveOwners.SelectedItems.Count - 1; i++) {
				lstActiveOwners.Items.Remove(lstActiveOwners.SelectedItems[0]);
			}
			try {
				cmdEditList.Content = "SAVE";
				cmdEditList.Visibility = System.Windows.Visibility.Visible;
			} catch {
			}
			lists[currentId].isChanged = true;

		}


		private void cmdAddBuilding_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstBuildings.SelectedItems.Count > 0) {
				foreach (WinPM.Building l in lstBuildings.SelectedItems) {
					if (!lstActiveBuildings.Items.Contains(l)) {
						lstActiveBuildings.Items.Add(l);
					}
				}
				try {
					cmdEditList.Content = "SAVE";
					cmdEditList.Visibility = System.Windows.Visibility.Visible;
				} catch {
				}
				lists[currentId].isChanged = true;
			}

		}


		private void cmdRemoveBuilding_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			for (int i = 0; i <= lstActiveBuildings.SelectedItems.Count - 1; i++) {
				lstActiveBuildings.Items.Remove(lstActiveBuildings.SelectedItems[0]);
			}
			try {
				cmdEditList.Content = "SAVE";
				cmdEditList.Visibility = System.Windows.Visibility.Visible;
			} catch {
			}
			lists[currentId].isChanged = true;

		}


		private void cmdAddType_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (lstTypes.SelectedItems.Count > 0) {
				foreach (WinPM.Types l in lstTypes.SelectedItems) {
					if (!lstActiveTypes.Items.Contains(l)) {
						lstActiveTypes.Items.Add(l);
					}
				}
				try {
					cmdEditList.Content = "SAVE";
					cmdEditList.Visibility = System.Windows.Visibility.Visible;
				} catch {
				}
				lists[currentId].isChanged = true;
			}

		}


		private void cmdRemoveType_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			for (int i = 0; i <= lstActiveTypes.SelectedItems.Count - 1; i++) {
				lstActiveTypes.Items.Remove(lstActiveTypes.SelectedItems[0]);
			}
			try {
				cmdEditList.Content = "SAVE";
				cmdEditList.Visibility = System.Windows.Visibility.Visible;
			} catch {
			}
			lists[currentId].isChanged = true;

		}


		private void cmdAddList_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			clearAll();
			stackList.Visibility = System.Windows.Visibility.Collapsed;
			stackIdName.Visibility = System.Windows.Visibility.Collapsed;
			stackAddList.Visibility = System.Windows.Visibility.Visible;

		}

		private void clearAll()
		{
			lstActiveBuildings.Items.Clear();
			lstActiveOwners.Items.Clear();
			lstActiveTypes.Items.Clear();
			ListNameTextBox.Text = "";
			ListIdTextBox.Text = "";
			datRecipients.Visibility = System.Windows.Visibility.Collapsed;

		}


		private void cmdNewListAdd_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			currentId = lists.Length;
			Array.Resize(ref lists, lists.Length + 2);
			lists[currentId] = new RecipientMaster();
			lists[currentId].listName = NewListName.Text;
			setActive();
			lists[currentId].accountId = (int)MbcsCentral.GlobalShared.user.Account.accountId;
			lists[currentId].save();
			stackAddList.Visibility = System.Windows.Visibility.Collapsed;
			stackList.Visibility = System.Windows.Visibility.Visible;
			stackIdName.Visibility = System.Windows.Visibility.Visible;
			loading = true;
			RecipientMasterViewSource.Source = null;
			lists = null;
			lists = RecipientMaster.load();
			RecipientMasterViewSource.Source = lists;
			datRecipients.Visibility = System.Windows.Visibility.Visible;
			loading = false;

		}


		private void cmdNewListCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			stackList.Visibility = System.Windows.Visibility.Visible;
			stackIdName.Visibility = System.Windows.Visibility.Visible;
			stackAddList.Visibility = System.Windows.Visibility.Collapsed;
			datRecipients.Visibility = System.Windows.Visibility.Visible;
			NewListName.Focus();

		}


		private void cmdDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Interaction.MsgBox("Are you sure that you want to delete this list and all of its recipients?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
				int cR = 0;
				int cL = 0;

				if (recips.Length > 0) {
					for (int i = 0; i <= recips.Length - 1; i++) {
						if (recips[i].delete()) {
							cR += 1;
						}
					}
				}
				if (lists[currentId].delete()) {
					cL = 1;
				}
				Interaction.MsgBox("Deleted " + cL + " master list (" + lists[currentId].listName + ") and " + cR + " recipients");
				loading = true;
				currentId = 0;
				RecipientMasterViewSource.Source = null;
				lists = null;
				lists = RecipientMaster.load();
				RecipientMasterViewSource.Source = lists;
				loading = false;
			}

		}


		private void cmdDeleteRecipients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (recips.Length <= 0) {
				Interaction.MsgBox("There are no recipients in this list to delete.");
				return;
			}
			if (Interaction.MsgBox("Are you SURE that you want to DELETE all recipients for this list?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
				if (Interaction.MsgBox("You will not be able to restore these recipients once deleted.  Delete any way?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
					int c = 0;
					for (int i = 0; i <= recips.Length - 1; i++) {
						if (recips[i].delete()) {
							c += 1;
						}
					}
					Interaction.MsgBox(c + " recipients deleted.");
				}
			}

		}


		private void cmdEditRecipients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!MbcsCentral.GlobalShared.user.hasModifyPermission(myApp))
            {
                MessageBoxCustom.Show("You do not have permission to edit lists.");
                return;
            }
			if (lstLists.SelectedIndex >= 0) {
				frmRecipientEdit frm = new frmRecipientEdit();
				frm.listId = lists[currentId].listId;
				frm.Owner = this;
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
				frm.ShowDialog();
				System.Windows.Data.CollectionViewSource RecipientsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("RecipientsViewSource");
				RecipientsViewSource.Source = null;
				recips = Recipients.loadByList(lists[currentId].listId);
				RecipientsViewSource.Source = recips;
			}

		}


		private void Button_Click(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			Help frm = new Help();
			try {
				frm.Source = new System.Uri("HelpFilters.xaml", UriKind.Relative);
			} catch (UriFormatException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "URI Exception: " + ex.Message);
				return;
			}
			try {
				frm.Show();
				frm.BringIntoView();
				frm.Focus();
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}

		}
		public frmRecipientMaster()
		{
			Loaded += Window_Loaded;
			Closing += frmRecipientMaster_Closing;
		}
	}
}
