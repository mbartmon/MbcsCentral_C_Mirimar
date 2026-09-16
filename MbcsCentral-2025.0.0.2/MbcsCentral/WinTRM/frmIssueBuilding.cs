using System.Windows.Forms;
using System.Drawing;
using System.Net.Mail;
using MBCSsmtp;
namespace WinTRM
{

	public partial class frmIssueBuilding
	{

		private const int startWidth = 638;
		private const int startHeight = 638;
		public Tenants tenant;
		public IssueTenants[] issueTens = null;
		private BuildingLocation[] locations;
		private Vendors[] vends;
		public bool bldOnly = true;
		public Building bld;
		public long issueId = -1;
		long id = 0;
		Issues issue;
		frmTenantMessages frmMsg;
		long oldLeft;

		long oldWidth;

		private void frmNote_Activated(object sender, System.EventArgs e)
		{
			lstLocations.Items.Clear();
			locations = BuildingLocation.load();
			if ((locations != null)) {
				foreach (BuildingLocation location in locations) {
					lstLocations.Items.Add(new ListItem(location.Location, location.ID));
				}
			}
			lstVendors.Items.Clear();
			vends = Vendors.load;
			if ((vends != null)) {
				foreach (Vendors vend in vends) {
					lstVendors.Items.Add(new ListItem(vend.VendorName, vend.ID));
				}
			}
			lstNotify.SelectedItems.Clear();
			if (issueId > 0) {
				issue = Issues.load(issueId);
				int[] ids = issue.getRecipientIds;
				int i = 0;
				ListItem lst = default(ListItem);
				if ((ids != null)) {
					for (i = 0; i <= Information.UBound(ids, 1); i++) {
						int j = 0;
						for (j = 0; j <= lstNotify.Items.Count - 1; j++) {
							lst = (ListItem)lstNotify.Items(j);
							if (lst.ValueItem == ids(i)) {
								lstNotify.SelectedItems.Add(lstNotify.Items(j));
								break; // TODO: might not be correct. Was : Exit For
							}
						}
					}
				}
			}

		}


		private void frmNote_Load(System.Object sender, System.EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
			this.Bounds = Utils.getForm(this);
			oldLeft = this.Left;
			oldWidth = this.Width;
			issueId = issueId;
			if (issueId <= 0 & !GlobalShared.user.hasUserPermission()) {
				Interaction.MsgBox("You do not have permission to create issues");
				Close();
			}
			txtTimeStamp.Text = Strings.Format(Now(), "F");
			Accounts[] notify = Accounts.load;
			lstNotify.TabIndex = 5;
			if ((notify != null) & Information.UBound(notify, 1) > 0) {
				Accounts nt = default(Accounts);
				ArrayList accts = new ArrayList();
				foreach ( nt in notify) {
					accts.Add(new ListItem(nt.getName, nt.getAccountId));
					nt = null;
				}
				lstNotify.DataSource = accts;
				lstNotify.DisplayMember = "DisplayItem";
				lstNotify.ValueMember = "ValueItem";
				//Dim idx As Long = lstNotify.Items.IndexOf(user.getName)
				//If idx >= 0 Then
				//lstNotify.SelectedIndex = idx
				//End If
				//lstNotify.Refresh()
				ArrayList persons = new ArrayList();
				persons.Add(new ListItem("<No One>", -1));
				ListItem lstitem = default(ListItem);
				foreach ( lstitem in accts) {
					persons.Add(lstitem);
				}
				lstResponsible.DataSource = persons;
				lstResponsible.DisplayMember = "DisplayItem";
				lstResponsible.ValueMember = "ValueItem";
				lstResponsible.SelectedIndex = 0;
				lstResponsible.Refresh();
				notify = null;
			}
			getCategories();

			pnlNotes.Controls.Clear();
			lstStatus.SelectedIndex = 0;
			if (issueId >= 0) {
				Issues issue = Issues.load(issueId);
				if ((issue == null)) {
					Interaction.MsgBox("Error retrieving note.");
					Close();
				}
				issueTens = IssueTenants.loadByNote(issueId);
				if ((issueTens == null)) {
					Interaction.MsgBox("Error retrieveing " + (bldOnly ? "building" : "tenant").ToString + " record for issueID " + issueId);
					Close();
				}
				txtTimeStamp.Text = Strings.Format(issue.getTimestamp, "F");
				dtDue.Value = issue.getDuedate;
				dtDue.Checked = (issue.getDuedate() >= "#12/31/2999#" ? false : true);
				setAlertMode(issue);
				lstStatus.SelectedIndex = issue.getStatus;
				txtSubject.Text = issue.getSubject;
				lstCategory.Text = issue.getCategory;
				ListItem itm = default(ListItem);
				for (int i = 0; i <= lstLocations.Items.Count - 1; i++) {
					itm = lstLocations.Items(i);
					if (itm.ValueItem == issue.getLocationId) {
						lstLocations.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				for (int i = 0; i <= lstVendors.Items.Count - 1; i++) {
					itm = lstVendors.Items(i);
					if (itm.ValueItem == issue.getVendorId) {
						lstVendors.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				if (DateValue(issue.getReported) <= "01/01/1900") {
					dtReported.Checked = false;
				} else {
					dtReported.Checked = true;
					dtReported.Value = issue.getReported;
				}
				if (DateValue(issue.getAssigned) <= "01/01/1900") {
					dtAssigned.Checked = false;
				} else {
					dtAssigned.Checked = true;
					dtAssigned.Value = issue.getAssigned;
				}
				try {
					setNotes();
				} catch (System.Exception ex) {
					Interaction.MsgBox("Loading notes: " + ex.Message);
				}

				try {
					int[] s = issue.getRecipientIds;
					if ((s != null)) {
						if (s.Length > 0) {
							for (Int16 i = 0; i <= s.Length - 1; i++) {
								for (Int16 j = 0; j <= lstNotify.Items.Count - 1; j++) {
									itm = lstNotify.Items(j);
									if (itm.ValueItem == s(i)) {
										lstNotify.SetSelected(j, true);
										break; // TODO: might not be correct. Was : Exit For
									}
								}
							}
						}
					}
				} catch (System.Exception ex) {
					Interaction.MsgBox("Loading recipients: " + ex.Message);
				}
				try {
					lstResponsible.SelectedValue = issue.getPersonResponsible;
					chkResponsible.Checked = issue.getNotifyResponsible;
				} catch (System.Exception ex) {
					Interaction.MsgBox("Getting Responsible: " + ex.Message);
				}
				foreach (IssueTenants ten in issueTens) {
					try {
						switch (bldOnly) {
							case true:
								bld = Building.load(ten.buildingNumber);
								if ((bld != null)) {
									lstTenants.Items.Add(new ListItem(string.Format("{0}: {1}", bld.BuildingNumber, bld.Address1), bld.BuildingNumber));
								}
								break;
							case false:
								tenant = Tenants.load(ten.getSequenceNumber);
								if ((tenant != null)) {
									lstTenants.Items.Add(new ListItem(string.Format("{0}{1}: {2}", tenant.getBuildingNumber, tenant.getTenantNumber, tenant.getTenantName), tenant.getSequenceNumber));
								}
								break;
						}
					} catch (System.Exception ex) {
						if (ex.Message.ToLower.Contains("position 0")) {
							Interaction.MsgBox((bldOnly ? "building" : "tenant").ToString + " not found");
						} else {
							Interaction.MsgBox("Loading " + (bldOnly ? "buildings" : "tenants").ToString + ": " + ex.Message);
						}
					}
				}
			} else {
				switch (bldOnly) {
					case true:
						lstTenants.Items.Add(new ListItem(string.Format("{0}: {1}", bld.BuildingNumber, bld.Address1), bld.BuildingNumber));
						break;
					case false:
						lstTenants.Items.Add(new ListItem(string.Format("{0}{1}: {2}", tenant.getBuildingNumber, tenant.getTenantNumber, tenant.getTenantName), tenant.getSequenceNumber));
						break;
				}
				ListItem itm = default(ListItem);
				lstNotify.SelectedIndex = -1;
				for (int i = 0; i <= lstNotify.Items.Count - 1; i++) {
					itm = lstNotify.Items(i);
					if (itm.ValueItem == GlobalShared.user.getAccountId) {
						lstNotify.SelectedIndex = i;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}

			frmMsg = new frmTenantMessages();
			frmMsg.master = this;
			frmMsg.Height = this.Height;
			frmMsg.Visibility = Visibility.Collapsed;

		}


		private void getCategories()
		{
			lstCategory.DataSource = null;
			lstCategory.Items.Clear();
			Categories[] category = Categories.load;
			ArrayList cats = new ArrayList();
			cats.Add(new ListItem(" ", 0));
			if ((category != null)) {
				Categories ct = default(Categories);
				foreach ( ct in category) {
					cats.Add(new ListItem(ct.getCategory, ct.getCategoryId));
					ct = null;
				}
			}
			cats.Add(new ListItem("<New Category>", -1));
			lstCategory.DataSource = cats;
			lstCategory.DisplayMember = "DisplayItem";
			lstCategory.ValueMember = "ValueItem";
			lstCategory.SelectedIndex = 0;
			lstCategory.Refresh();

		}


		private void setCategory(long id)
		{
			foreach (ListItem itm in lstCategory.Items) {
				if (itm.ValueItem == id) {
					lstCategory.SelectedItem = itm;
					break; // TODO: might not be correct. Was : Exit For
				}
			}

		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			if (!GlobalShared.user.hasUserPermission()) {
				Interaction.MsgBox("You do not have permission to save Issues");
				return;
			}
			if (issueId > 0) {
				issue = Issues.load(issueId);
				if ((issue == null)) {
					Interaction.MsgBox("Error retrieving original note.");
					return;
				}
			} else {
				issue = new Issues();
				issue.setTimestamp(Now());
			}
			//note.setsequenceNumber(tenant.getSequenceNumber)
			issue.setAccountId(GlobalShared.user.getAccountId);
			issue.setDuedate((!dtDue.Checked ? "12/31/2999" : dtDue.Value));

			setAlerts(issue);
			//note.setFollowupDate(dtFollowup.Value)
			issue.setStatus(lstStatus.SelectedIndex);
			ListBox.SelectedObjectCollection ids = lstNotify.SelectedItems;
			int i = 0;
			ListItem lst = default(ListItem);
			int[] rcps = new int[ids.Count];
			for (i = 0; i <= ids.Count - 1; i++) {
				lst = ids.Item(i);
				rcps(i) = lst.ValueItem;
			}
			issue.setRecipientIds(rcps);
			issue.setPersonResponsible(lstResponsible.SelectedValue);
			issue.setNotifyResponsible(chkResponsible.Checked);
			issue.setSubject(txtSubject.Text);
			issue.setCategory(lstCategory.Text);
			issue.setLastUpdate(Now);
			ListItem itm = default(ListItem);
			if (lstLocations.SelectedIndex >= 0) {
				itm = lstLocations.Items(lstLocations.SelectedIndex);
				issue.setLocationId(itm.ValueItem);
			}
			if (lstVendors.SelectedIndex >= 0) {
				itm = lstVendors.Items(lstVendors.SelectedIndex);
				issue.setVendorId(itm.ValueItem);
			}
			if (dtReported.Checked) {
				issue.setReported(dtReported.Value);
			} else {
				issue.setReported("#01/01/1900#");
			}
			if (dtAssigned.Checked) {
				issue.setAssigned(dtAssigned.Value);
			} else {
				issue.setAssigned("#01/01/1900#");
			}
			id = issueId;
			issue.Save();
			issueId = issue.getNoteId;
			saveTenants(issueId);
			if (GlobalShared.autoEmail & id <= 0) {
				string msg = "New issue created for tenants->" + Constants.vbCrLf;
				foreach ( itm in lstTenants.Items) {
					msg += itm.DisplayItem + Constants.vbCrLf;
				}
				id = issueId;
				doEmail(txtSubject.Text, eType.general, msg);
			}
			if (id <= 0)
				Close();

		}

		private void setAlertMode(Issues note)
		{
			if (note.alertEveryLogon) {
				chkEveryLogin.Checked = true;
			} else if (note.alertInitialLogon) {
				chkInitialLogin.Checked = true;
			}
			if (note.alertEveryOpenApp) {
				chkEveryApp.Checked = true;
			} else if (note.alertIntialOpenApp) {
				chkInitialApp.Checked = true;
			}
			if (note.alertEveryOpenRecord) {
				chkAlwaysTenant.Checked = true;
			} else if (note.alertInitialOpenRecord) {
				chkInitialTenant.Checked = true;
			}
		}


		private void setAlerts(Issues note)
		{
			note.setAlertMod(0);
			if (chkInitialLogin.Checked)
				note.setAlertModeAdditive(Issues.alerts.initialLogon);
			if (chkEveryLogin.Checked)
				note.setAlertModeAdditive(Issues.alerts.everyLogon);
			if (chkInitialApp.Checked)
				note.setAlertModeAdditive(Issues.alerts.initialOpenApp);
			if (chkEveryApp.Checked)
				note.setAlertModeAdditive(Issues.alerts.everyOpenApp);
			if (chkInitialTenant.Checked)
				note.setAlertModeAdditive(Issues.alerts.intialOpenRecord);
			if (chkAlwaysTenant.Checked)
				note.setAlertModeAdditive(Issues.alerts.everyOpenRecord);

		}


		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			Utils.saveForm(this);
			Close();

		}



		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string dt = Strings.Format(Now(), "F");
			long id = issueId;

			if (issueId <= 0) {
				MsgBoxResult result = Interaction.MsgBox("Issue must be saved before Notes can be added." + Constants.vbCrLf + Constants.vbCrLf + "Save Issue now?", MsgBoxStyle.YesNo);
				switch (result) {
					case MsgBoxResult.No:
						return;

						break;
					case MsgBoxResult.Yes:
						this.cmdSave_Click(sender, e);
						break;
				}
			}

			frmNote frm = new frmNote();

			frm.txtNote.Clear();

			frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			frm.txtNote.SelectionStart = txtNote.TextLength;
			frm.txtNote.Focus();
			frm.ShowDialog();
			if (frm.DialogResult == true) {
				Notes note = new Notes();
				note.setaccount_id(GlobalShared.user.getAccountId);
				note.setissue_id(issueId);
				note.setnote(frm.txtNote.Text.Trim);
				note.settimestamp(Now());
				if (note.save() == true) {
					setNotes();
					if (GlobalShared.autoEmail & id > 0) {
						string msg = "For tenants->" + Constants.vbCrLf;
						foreach (ListItem itm in lstTenants.Items) {
							msg += itm.DisplayItem + Constants.vbCrLf;
						}
						msg += Constants.vbCrLf + "New comment added:" + Constants.vbCrLf + Constants.vbCrLf + frm.txtNote.Text.Trim;
						doEmail("Re: " + txtSubject.Text, eType.note, msg);
					}
				} else {
					Interaction.MsgBox("Error saving this note.");
				}
			}

		}


		private void setNotes()
		{
			pnlNotes.Controls.Clear();
			Notes[] note = Notes.loadByIssue(issueId);
			if ((note != null)) {
				if (note.Length > 0) {
					TextBox txt = null;
					int p = 0;
					Font f = new Font("Times New Roman", 12);
					Accounts acct = default(Accounts);
					bool c = false;
					foreach (Notes ntx in note) {
						txt = new TextBox();
						txt.Top = p;
						txt.Multiline = true;
						txt.MaximumSize = new Size(0, 0);
						txt.Font = f;
						txt.WordWrap = true;
						txt.ReadOnly = true;
						txt.BorderStyle = BorderStyle.None;
						txt.Parent = pnlNotes;
						txt.BackColor = (c ? Color.White : Color.Wheat);
						c = !c;
						acct = Accounts.load(ntx.getaccount_id);
						txt.Text = string.Format("** {0} << {1} >> **{2}{3}{4}{5}", Strings.Format(ntx.gettimestamp, "MM/dd/yyyy hh:mm"), acct.getName, Constants.vbCrLf, Constants.vbCrLf, ntx.getnote, Constants.vbCrLf);
						string[] s = txt.Text.Split(Constants.vbCrLf);
						txt.MinimumSize = new Size(pnlNotes.Width - 10, s.Length * 18 + 18);
						txt.Size = (new Size(pnlNotes.Width - 10, s.Length * 18 + 18));
						txt.Tag = ntx.getnote_id;
						txt.DoubleClick += handleNoteEdit;
						pnlNotes.Controls.Add(txt);
						p += txt.Height;
					}
				}
			}

		}


		public void handleNoteEdit(object sender, EventArgs e)
		{
			TextBox lbl = (System.Windows.Forms.TextBox)sender;
			long s = Conversion.Val(lbl.Tag);
			frmNote frm = new frmNote();
			frm.Title = "Edit a Note";
			frm.cmdDelete.Visibility = Visibility.Visible;
			frm.txtNote.Text = lbl.Text.Substring(lbl.Text.IndexOf(Constants.vbCrLf) + 1).Trim;
			frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			frm.ShowDialog();
			if (frm.DialogResult == true) {
				Notes note = default(Notes);
				note = Notes.load(s);
				if ((note != null)) {
					note.setnote(frm.txtNote.Text.Trim);
					note.save();
					string msg = "For tenants->" + Constants.vbCrLf;
					foreach (ListItem itm in lstTenants.Items) {
						msg += itm.DisplayItem + Constants.vbCrLf;
					}
					msg += Constants.vbCrLf + "A Comment Has Been Edited." + Constants.vbCrLf + "===============" + Constants.vbCrLf + "Original comment:" + Constants.vbCrLf + Constants.vbCrLf + lbl.Text.Trim + Constants.vbCrLf + "===============" + Constants.vbCrLf + "Edited comment:" + Constants.vbCrLf + Constants.vbCrLf + frm.txtNote.Text.Trim;
					doEmail("Re: " + txtSubject.Text, eType.note, msg);
					setNotes();
				}
			}

		}


		private void cmdAddTenant_Click(object sender, System.EventArgs e)
		{
			frmTenantList frm = new frmTenantList {
				Top = Top + 300,
				Left = Left + 100
			};
			frm.bldOnly = bldOnly;
			frm.ShowDialog();
			if ((frm.selectedTenants != null)) {
				for (int i = 0; i <= frm.selectedTenants.Length - 1; i++) {
					lstTenants.Items.Add(frm.selectedTenants(i));
				}
			}
			frm.Dispose();

		}


		private void cmdRemoveTenant_Click(object sender, System.EventArgs e)
		{
			if (lstTenants.SelectedItems.Count == 0) {
				Interaction.MsgBox("No tenants have been selected for removal from the list.");
			} else {
				for (int i = 0; i <= lstTenants.SelectedItems.Count - 1; i++) {
					lstTenants.Items.Remove(lstTenants.SelectedItems(i));
				}
			}

		}


		private void saveTenants(long note)
		{
			IssueTenants[] tens = null;
			bool found = false;

			if (issueId > 0) {
				tens = IssueTenants.loadByNote(note);
				if ((tens != null)) {
					foreach (IssueTenants tenNote in tens) {
						found = false;
						foreach (ListItem itm in lstTenants.Items) {
							if (itm.ValueItem == (bldOnly ? tenNote.buildingNumber : tenNote.getSequenceNumber)) {
								found = true;
								break; // TODO: might not be correct. Was : Exit For
							}
						}
						if (!found) {
							tenNote.delete(tenNote.getId);
						}
					}
				}
			}
			IssueTenants ten = null;
			Tenants tenant = default(Tenants);
			foreach (ListItem itm in lstTenants.Items) {
				IssueTenants tenNote = default(IssueTenants);
				found = false;
				if ((tens != null)) {
					foreach ( tenNote in tens) {
						if ((bldOnly ? tenNote.buildingNumber : tenNote.getSequenceNumber) == itm.ValueItem) {
							found = true;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
					if (!found) {
						switch (bldOnly) {
							case true:
								ten = new IssueTenants(note, -1, Now(), itm.ValueItem, "", "");
								break;
							case false:
								tenant = Tenants.load(itm.ValueItem);
								ten = new IssueTenants(note, itm.ValueItem, Now(), tenant.getBuildingNumber, tenant.getTenantNumber, tenant.getApartment);
								break;
						}
						ten.Save();
					}
				}
			}

		}


		private void frmIssue_Move(object sender, System.EventArgs e)
		{
			try {
				if (frmMsg.Visibility == Visibility.Visible) {
					if (frmMsg.Left == oldLeft + oldWidth) {
						frmMsg.Left = this.Left + this.Width;
					}
				}
				oldLeft = this.Left;
				oldWidth = this.Width;
			} catch {
			}

		}


		private void frmIssue_Resize(object sender, System.EventArgs e)
		{
			if (this.Height < startHeight)
				this.Height = startHeight;
			if (this.Width < startWidth)
				this.Width = startWidth;

			cmdEmail.Left = pnlNotes.Left;
			cmdEmail.Top = this.Height - cmdEmail.Height - 45;
			cmdExit.Top = this.Height - cmdExit.Height - 45;
			cmdExit.Left = this.Width - cmdExit.Width - cmdMessages.Width - 30;
			cmdPrint.Top = this.Height - cmdPrint.Height - 45;
			cmdPrint.Left = cmdExit.Left - cmdExit.Width - 10;
			cmdDelete.Top = this.Height - cmdDelete.Height - 45;
			cmdDelete.Left = cmdPrint.Left - cmdPrint.Width - 10;
			cmdSave.Top = this.Height - cmdSave.Height - 45;
			cmdSave.Left = cmdDelete.Left - cmdSave.Width - 10;
			pnlNotes.Width = this.Width - cmdAdd.Width - cmdMessages.Width - 20;
			pnlNotes.Top = txtSubject.Top + txtSubject.Height + 5;
			pnlNotes.Height = this.Height - pnlNotes.Top - cmdEmail.Height - 50;

			setNotes();

		}


		private void cmdEmail_Click(object sender, System.EventArgs e)
		{
			doEmail(txtSubject.Text, eType.general);

		}

		private void doEmail(string subject, eType emailType, string body = "")
		{
			frmIssueEmail frm = new frmIssueEmail();

			ListItem ten = lstTenants.Items(0);
			if ((ten != null)) {
				frm.tenantId = ten.ValueItem;
			} else {
				frm.tenantId = 0;
			}
			frm.issueId = issueId;
			frm.emailType = emailType;
			frm.tens = lstTenants.Items;
			frm.users = lstNotify.SelectedItems;
			frm.responsible = (lstResponsible.SelectedIndex > 0 ? lstResponsible.SelectedItem : null);
			frm.Title += ": " + txtSubject.Text;
			string msg = "";
			if (issueId <= 0) {
				msg = "New issue created for tenants->" + Constants.vbCrLf;
				foreach (ListItem itm in lstTenants.Items) {
					msg += itm.DisplayItem + Constants.vbCrLf;
				}
				body = msg + body;
			}
			frm.txtSubject.Text = subject;
			if (!string.IsNullOrEmpty(body.Trim())) {
				frm.txtMessage.Text = body;
			}
			frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			try {
				frm.ShowDialog();
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}

		}


		private void cmdMessages_Click(object sender, System.EventArgs e)
		{
			if (frmMsg.Visibility == Visibility.Collapsed | frmMsg.Visibility == Visibility.Hidden) {
				frmMsg.Visibility = Visibility.Visible;
				cmdMessages.Image = WinTRM.My.Resources.Resources.ARW03LT;
			} else {
				frmMsg.Visibility = Visibility.Collapsed;
				cmdMessages.Image = WinTRM.My.Resources.Resources.ARW03RT;
			}
		}


		private void cmdExit_Click1(object sender, System.EventArgs e)
		{
			Utils.saveForm(this);
			if ((frmMsg != null)) {
				try {
					frmMsg.Close();
					frmMsg = null;
				} catch {
				}
			}
			this.Close();

		}


		private void lstCategory_SelectedValueChanged(object sender, System.EventArgs e)
		{
			ListItem itm = lstCategory.SelectedItem;

			if ((itm != null)) {
				if (itm.ValueItem == -1) {
					frmCategory frm = new frmCategory();
					frm.@add = true;
					frm.fromIssue = true;
					frm.ShowDialog();
					if (!string.IsNullOrEmpty(frm.category.Trim)) {
						getCategories();
						lstCategory.Text = frm.category;
					}
				}
			}
		}


		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Interaction.MsgBox("Delete this issue <" + issueId + "><" + issue.getSubject + ">?", MsgBoxStyle.OkCancel) == MsgBoxResult.Ok) {
				Notes[] note = null;
				long c = 0;
				long i = 0;
				note = Notes.loadByIssue(issueId);
				if ((note != null)) {
					foreach (Notes n in note) {
						try {
							n.delete(n.getnote_id);
							c += 1;
						} catch {
						}
					}
				}
				try {
					issue.delete(issueId);
					i = 1;
				} catch {
				}
				Interaction.MsgBox(i + " Issue deleted along with " + c + " notes.");
				this.Close();
			}

		}


		private void LocationLabel_Click(System.Object sender, System.EventArgs e)
		{
		}


		private void cmdPrint_Click(object sender, System.EventArgs e)
		{
			frmReportConfig frm = new frmReportConfig();
			frm.recordKey = issueId;
			frm.reportId = report.BuildingIssue;
			frm.Show();

		}
		public frmIssueBuilding()
		{
			Resize += frmIssue_Resize;
			Move += frmIssue_Move;
			Load += frmNote_Load;
			Activated += frmNote_Activated;
		}
	}
}
