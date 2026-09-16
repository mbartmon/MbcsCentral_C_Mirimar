using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Control;
namespace WinTRM
{

	public class frmIssuesDashboard
	{

		private bool loading = true;
		private OleDbDataAdapter daIssues;
		private OleDbDataAdapter daTenants;
		private OleDbDataAdapter daAccounts;
		private OleDbDataAdapter daBuildings;
		private DataSet dsIssues = new DataSet("Issues");
		private bool m_email = false;
		private long m_issueId = -1;
		/// <summary>
		/// Setting this property to true prior to calling the show method puts this form into Email-Assign mode.
		/// This will cause the display of an ASSIGN button which will put the issueId of the currently selected issue 
		/// in m_issueid for use by the calling form.
		/// </summary>
		/// <value>True/False</value>
		/// <remarks></remarks>
		public bool email {
			set { m_email = value; }
		}

		/// <summary>
		/// This property will contain the issueId of the currently selected issue if m_email is set to true.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public long issueId {
			get { return m_issueId; }
		}


		private void frmIssuesDashboard_Closed(object sender, System.EventArgs e)
		{
			Utils.saveForm(this);

		}


		private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Data.CollectionViewSource AccountsViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("AccountsViewSource");
			//Load data by setting the CollectionViewSource.Source property:
			//AccountsViewSource.Source = [generic data source]
			Accounts[] accts = null;
			accts = Accounts.load;
			AccountsViewSource.Source = accts;

		}


		private void getTenantIssues()
		{
			try {
				string xsql = string.Format("{0}, {1}.{2}, {1}.{3} FROM {4}, {5} WHERE {1}.{2} >0 AND ", Issues.SQL_LOAD_DASHBOARD, IssueTenants.TABLE, IssueTenants.SQL_COL_SEQUENCE_NUMBER, IssueTenants.SQL_COL_BUILDING_NUMBER, Issues.TABLE, IssueTenants.TABLE);
				// Want a specific tenant?
				if (lstTenants.SelectedIndex > 0) {
					ListItem lst = lstTenants.SelectedItem;
					xsql += string.Format(" {0}.{1} = {2} AND ", IssueTenants.TABLE, IssueTenants.SQL_COL_SEQUENCE_NUMBER, lst.ValueItem);
				}
				xsql += string.Format(" {0}.{1} = {2}.{3}", IssueTenants.TABLE, IssueTenants.SQL_COL_ISSUE_ID, Issues.TABLE, Issues.SQL_COL_ISSUE_ID);
				string ordSql = string.Format(" ORDER BY {0};", Issues.orderBy(Issues.issueOrder.byCreateDate));
				xsql += getTenantSelections() + ordSql;
				GlobalShared.CN.Open();
				if ((daIssues != null)) {
					try {
						daIssues.Dispose();
					} catch {
					}
				}
				daIssues = new OleDbDataAdapter(xsql, GlobalShared.CN.ConnectionString);
				dsIssues.Clear();
				daIssues.Fill(dsIssues.Tables("Issues"));
				GlobalShared.CN.Close();
				if (dsIssues.Tables("Issues").Rows.Count > 0) {
					// Accounts
					string aSql = string.Format("SELECT DISTINCT {0}, {1} FROM {2} WHERE {0} IN (", Accounts.SQL_COL_ACCOUNT_ID, Accounts.SQL_COL_NAME, Accounts.TABLE);
					bool first = true;
					foreach (DataRow dr in dsIssues.Tables("Issues").Rows) {
						if (first) {
							first = false;
						} else {
							aSql += ", ";
						}
						aSql += dr(Issues.SQL_COL_ACCOUNT_ID).ToString + (!string.IsNullOrEmpty(dr("Responsible").ToString.Trim) ? ", " + dr("Responsible").ToString : "").ToString;
					}
					aSql += ");";
					GlobalShared.CN.Open();
					daAccounts = new OleDbDataAdapter(aSql, GlobalShared.CN.ConnectionString);
					daAccounts.Fill(dsIssues.Tables("Accounts"));
					GlobalShared.CN.Close();
					// Tenants
					string tSql = string.Format("SELECT DISTINCT [{0}], [{1}], {2}, [{3}], [{4}] FROM {5} WHERE [{6}] IN (", Tenants.SQL_COL_BUILDING_NUMBER, Tenants.SQL_COL_TENANT_NUMBER, Tenants.SQL_COL_APARTMENT, Tenants.SQL_COL_TENANT_NAME, Tenants.SQL_COL_ID, Tenants.TABLE, Tenants.SQL_COL_ID);
					first = true;
					foreach (DataRow dr in dsIssues.Tables("Issues").Rows) {
						if (first) {
							first = false;
						} else {
							tSql += ", ";
						}
						tSql += dr(IssueTenants.SQL_COL_SEQUENCE_NUMBER).ToString;
					}
					tSql += ");";
					GlobalShared.pmCN.Open();
					daTenants = new OleDbDataAdapter(tSql, GlobalShared.pmCN.ConnectionString);
					daTenants.Fill(dsIssues.Tables("Tenants"));
					//Dim dataRel As New DataRelation("ForTenant", dsIssues.Tables("Tenants").Columns(Tenants.SQL_COL_ID), dsIssues.Tables("Issues").Columns(IssueTenants.SQL_COL_SEQUENCE_NUMBER), False)
					//dsIssues.Relations.Add(dataRel)
					GlobalShared.pmCN.Close();
					// Buildings
					string bSql = string.Format("SELECT [{0}], [{1}] FROM {2} ORDER BY [{0}];", Building.SQL_COL_BUILDING_NUMBER, Building.SQL_COL_ADDRESS_1, Building.TABLE, Building.SQL_COL_BUILDING_NUMBER);
					GlobalShared.pmCN.Open();
					daBuildings = new OleDbDataAdapter(bSql, GlobalShared.pmCN.ConnectionString);
					daBuildings.Fill(dsIssues.Tables("Buildings"));
					GlobalShared.pmCN.Close();

					DataTable tbl = dsIssues.Tables("DisplayTenants");
					DataRow ndr = default(DataRow);
					DataRow[] tdr = null;
					foreach (DataRow dr in dsIssues.Tables("Issues").Rows) {
						ndr = tbl.NewRow;
						ndr("ID") = dr("ID");
						if (Conversion.Val(dr(Issues.SQL_COL_SEQUENCE_NUMBER).ToString) <= 0) {
							tdr = dsIssues.Tables("Buildings").Select("[" + Building.SQL_COL_BUILDING_NUMBER + "] = '" + dr(IssueTenants.SQL_COL_BUILDING_NUMBER).ToString + "'");
							if ((tdr != null) & tdr.Length > 0) {
								ndr("Tenant") = "BLD: " + tdr(0).Item(Building.SQL_COL_BUILDING_NUMBER).ToString + ": " + tdr(0).Item(Building.SQL_COL_ADDRESS_1).ToString;
							} else {
								ndr("Tenant") = "<<Building record not found>> " + dr(IssueTenants.SQL_COL_BUILDING_NUMBER).ToString;
							}
						} else {
							tdr = dsIssues.Tables("Tenants").Select("[" + Tenants.SQL_COL_ID + "] = " + dr(Issues.SQL_COL_SEQUENCE_NUMBER).ToString);
							if ((tdr != null) & tdr.Length > 0) {
								ndr("Tenant") = tdr(0).Item(Tenants.SQL_COL_BUILDING_NUMBER).ToString + tdr(0).Item(Tenants.SQL_COL_TENANT_NUMBER).ToString + " : " + tdr(0).Item(Tenants.SQL_COL_APARTMENT).ToString + " : " + tdr(0).Item(Tenants.SQL_COL_TENANT_NAME).ToString.Trim;
							} else {
								ndr("Tenant") = string.Format("<<Tenant record not found>> [{0}]", dr(Issues.SQL_COL_SEQUENCE_NUMBER));
							}
						}
						ndr("Category") = dr(Issues.SQL_COL_CATEGORY).ToString;
						if ((dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr(Issues.SQL_COL_ACCOUNT_ID).ToString) != null)) {
							try {
								ndr("Created By") = dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr(Issues.SQL_COL_ACCOUNT_ID).ToString)(0).Item(Accounts.SQL_COL_NAME);
							} catch {
							}
						}
						ndr("Date") = dr("Created");
						ndr("Due") = dr("Due Date");
						if ((dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr("Responsible").ToString) != null)) {
							try {
								ndr("Responsible") = dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr("Responsible").ToString)(0).Item(Accounts.SQL_COL_NAME);
							} catch {
							}
						}
						if (Convert.ToInt32(dr("Issue Status")) >= 0) {
							ndr("Status") = Issues.issueStatus(dr("Issue Status"));
						} else {
							ndr("Status") = "";
						}
						ndr("Priority") = (Convert.ToInt32(dr("Issue Priority")) >= 0 ? Issues.issuePriority(dr("Issue Priority")) : "");
						tbl.Rows.Add(ndr);
					}
				}
			} catch (OleDbException oEx) {
				Interaction.MsgBox(oEx.Message);
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			} finally {
				daTenants.Dispose();
				daAccounts.Dispose();
				daIssues.Dispose();
			}

		}


		private void getBuildingIssues()
		{
			try {
				string xsql = string.Format("{0}, {1}.{2} FROM {3}, {4} WHERE {1}.{5} = -1 AND ", Issues.SQL_LOAD_DASHBOARD_BUILDING, IssueTenants.TABLE, IssueTenants.SQL_COL_BUILDING_NUMBER, Issues.TABLE, IssueTenants.TABLE, IssueTenants.SQL_COL_SEQUENCE_NUMBER);
				// Want a specific building?
				if (lstBuildings.SelectedIndex > 0) {
					ListItem lst = lstBuildings.SelectedItem;
					xsql += string.Format(" {0}.{1} = '{2}' AND ", IssueTenants.TABLE, IssueTenants.SQL_COL_BUILDING_NUMBER, lst.ValueItem);
				}
				xsql += string.Format(" {0}.{1} = {2}.{3}", IssueTenants.TABLE, IssueTenants.SQL_COL_ISSUE_ID, Issues.TABLE, Issues.SQL_COL_ISSUE_ID);
				string ordSql = string.Format(" ORDER BY {0};", Issues.orderBy(Issues.issueOrder.byCreateDate));
				xsql += getBuildingSelections() + ordSql;
				GlobalShared.CN.Open();
				daIssues = new OleDbDataAdapter(xsql, GlobalShared.CN.ConnectionString);
				dsIssues.Tables("Issues").Clear();
				daIssues.Fill(dsIssues.Tables("Issues"));
				GlobalShared.CN.Close();
				if (dsIssues.Tables("Issues").Rows.Count > 0) {
					// Accounts
					string aSql = string.Format("SELECT DISTINCT {0}, {1} FROM {2} WHERE {0} IN (", Accounts.SQL_COL_ACCOUNT_ID, Accounts.SQL_COL_NAME, Accounts.TABLE);
					bool first = true;
					foreach (DataRow dr in dsIssues.Tables("Issues").Rows) {
						if (first) {
							first = false;
						} else {
							aSql += ", ";
						}
						aSql += dr(Issues.SQL_COL_ACCOUNT_ID).ToString + (!string.IsNullOrEmpty(dr("Responsible").ToString.Trim) ? ", " + dr("Responsible").ToString : "").ToString;
					}
					aSql += ");";
					GlobalShared.CN.Open();
					daAccounts = new OleDbDataAdapter(aSql, GlobalShared.CN.ConnectionString);
					daAccounts.Fill(dsIssues.Tables("Accounts"));
					GlobalShared.CN.Close();
					// Buildings
					string bSql = string.Format("SELECT [{0}], [{1}] FROM {2} ORDER BY [{0}];", Building.SQL_COL_BUILDING_NUMBER, Building.SQL_COL_ADDRESS_1, Building.TABLE, Building.SQL_COL_BUILDING_NUMBER);
					GlobalShared.pmCN.Open();
					daBuildings = new OleDbDataAdapter(bSql, GlobalShared.pmCN.ConnectionString);
					daBuildings.Fill(dsIssues.Tables("Buildings"));
					GlobalShared.pmCN.Close();

					dsIssues.Tables("DisplayBuildings").Clear();
					DataTable tbl = dsIssues.Tables("DisplayBuildings");
					DataRow ndr = default(DataRow);
					DataRow[] tdr = null;
					BuildingLocation location = null;
					Vendors vendor = default(Vendors);
					foreach (DataRow dr in dsIssues.Tables("Issues").Rows) {
						ndr = tbl.NewRow;
						ndr("ID") = dr("ID");
						tdr = dsIssues.Tables("Buildings").Select("[" + Building.SQL_COL_BUILDING_NUMBER + "] = '" + dr(IssueTenants.SQL_COL_BUILDING_NUMBER).ToString + "'");
						if ((tdr != null) & tdr.Length > 0) {
							ndr("Building") = "BLD: " + tdr(0).Item(Building.SQL_COL_BUILDING_NUMBER).ToString + ": " + tdr(0).Item(Building.SQL_COL_ADDRESS_1).ToString;
						} else {
							ndr("Building") = "<<Building record not found>> " + dr(IssueTenants.SQL_COL_BUILDING_NUMBER).ToString;
						}
						ndr("Category") = dr(Issues.SQL_COL_CATEGORY).ToString;
						if ((dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr(Issues.SQL_COL_ACCOUNT_ID).ToString) != null)) {
							try {
								ndr("Created By") = dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr(Issues.SQL_COL_ACCOUNT_ID).ToString)(0).Item(Accounts.SQL_COL_NAME);
							} catch {
							}
						}
						ndr("Reported") = dr("reported");
						ndr("Assigned") = dr("assigned");
						if ((dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr("Responsible").ToString) != null)) {
							try {
								ndr("Responsible") = dsIssues.Tables("Accounts").Select(Accounts.SQL_COL_ACCOUNT_ID + " = " + dr("Responsible").ToString)(0).Item(Accounts.SQL_COL_NAME);
							} catch {
							}
						}
						if (Convert.ToInt32(dr("Issue Status")) >= 0) {
							ndr("Status") = Issues.issueStatus(dr("Issue Status"));
						} else {
							ndr("Status") = "";
						}
						ndr("Priority") = (Convert.ToInt32(dr("Issue Priority")) >= 0 ? Issues.issuePriority(dr("Issue Priority")) : "");
						if (!Information.IsDBNull(dr(Issues.SQL_COL_LOCATION_ID))) {
							if (Convert.ToInt64(dr(Issues.SQL_COL_LOCATION_ID)) > 0) {
								location = BuildingLocation.load(dr(Issues.SQL_COL_LOCATION_ID));
								if ((location != null)) {
									ndr("Location") = location.Location;
									location = null;
								}
							}
						}
						if (!Information.IsDBNull(dr(Issues.SQL_COL_VENDOR_ID))) {
							if (Convert.ToInt64(dr(Issues.SQL_COL_VENDOR_ID)) > 0) {
								vendor = Vendors.load(dr(Issues.SQL_COL_VENDOR_ID));
								ndr("Vendor") = vendor.VendorName;
								vendor = null;
							}
						}

						tbl.Rows.Add(ndr);
					}
				}
			} catch (OleDbException oEx) {
				Interaction.MsgBox(oEx.Message);
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			} finally {
				daTenants.Dispose();
				daIssues.Dispose();
				daAccounts.Dispose();
			}

		}

		private string getTenantSelections()
		{

			string xsql = "";
			ListItem lst = null;

			try {
				// Date range
				if (chkTenantFrom.IsChecked & chkTenantTo.IsChecked) {
					xsql = string.Format(" AND (" + Issues.SQL_COL_TIMESTAMP + " >= #{0}# AND " + Issues.SQL_COL_TIMESTAMP + " <=#{1}#)", dtTenantFrom.Text + " 00:00", dtTenantTo.Text + " 23:59");
				} else if (chkTenantFrom.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_TIMESTAMP + " >= #{0}#", dtTenantFrom.Text + " 00:00");
				} else if (chkTenantTo.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_TIMESTAMP + " <= #{0}#", dtTenantTo.Text + " 23:59");
				}

				// Created by
				if (lstTenantCreated.SelectedIndex > 0) {
					lst = lstTenantCreated.Items(lstTenantCreated.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_ACCOUNT_ID + " = {0}", lst.ValueItem);
				}

				// Responsible
				if (lstTenantResponsible.SelectedIndex > 0) {
					lst = lstTenantResponsible.Items(lstTenantResponsible.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PERSON_RESPONSIBLE + " = {0}", lst.ValueItem);
				}

				// Status
				if (lstTenantStatus.SelectedIndex > 0) {
					lst = lstTenantStatus.Items(lstTenantStatus.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_STATUS + " = {0}", lst.ValueItem);
				}

				// Priority
				if (lstTenantPriority.SelectedIndex > 0) {
					lst = lstTenantPriority.Items(lstTenantPriority.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PRIORITY + " = {0}", lst.ValueItem);
				}

				// Category
				if (lstTenantCategory.SelectedIndex > 0) {
					lst = lstTenantCategory.Items(lstTenantCategory.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_CATEGORY + " = '{0}'", lst.DisplayItem);
				}

			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}
			return xsql;

		}

		private string getBuildingSelections()
		{

			string xsql = "";
			ListItem lst = null;

			try {
				// Date range
				if (chkReportedFrom.IsChecked & chkReportedTo.IsChecked) {
					xsql = string.Format(" AND (" + Issues.SQL_COL_REPORTED + " >= #{0}# AND " + Issues.SQL_COL_REPORTED + " <=#{1}#)", dtReportedFrom.Text + " 00:00", dtReportedTo.Text + " 23:59");
				} else if (chkReportedFrom.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_REPORTED + " >= #{0}#", dtReportedFrom.Text + " 00:00");
				} else if (chkReportedTo.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_REPORTED + " <= #{0}#", dtAssignedTo.Text + " 23:59");
				}
				if (chkAssignedFrom.IsChecked & chkAssignedTo.IsChecked) {
					xsql = string.Format(" AND (" + Issues.SQL_COL_ASSIGNED + " >= #{0}# AND " + Issues.SQL_COL_ASSIGNED + " <=#{1}#)", dtAssignedFrom.Text + " 00:00", dtAssignedTo.Text + " 23:59");
				} else if (chkAssignedFrom.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_ASSIGNED + " >= #{0}#", dtAssignedFrom.Text + " 00:00");
				} else if (chkAssignedTo.IsChecked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_ASSIGNED + " <= #{0}#", dtAssignedTo.Text + " 23:59");
				}

				// Created by
				if (lstBuildingCreated.SelectedIndex > 0) {
					lst = lstBuildingCreated.Items(lstBuildingCreated.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_ACCOUNT_ID + " = {0}", lst.ValueItem);
				}

				// Responsible
				if (lstBuildingResponsible.SelectedIndex > 0) {
					lst = lstBuildingResponsible.Items(lstBuildingResponsible.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PERSON_RESPONSIBLE + " = {0}", lst.ValueItem);
				}

				// Status
				if (lstBuildingStatus.SelectedIndex > 0) {
					lst = lstBuildingStatus.Items(lstBuildingStatus.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_STATUS + " = {0}", lst.ValueItem);
				}

				// Priority
				if (lstBuildingPriority.SelectedIndex > 0) {
					lst = lstBuildingPriority.Items(lstBuildingPriority.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PRIORITY + " = {0}", lst.ValueItem);
				}

				// Category
				if (lstBuildingCategory.SelectedIndex > 0) {
					lst = lstBuildingCategory.Items(lstBuildingCategory.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_CATEGORY + " = '{0}'", lst.DisplayItem);
				}

				// Building Location
				if (lstBuildingLocation.SelectedIndex > 0) {
					lst = lstBuildingLocation.SelectedItem;
					xsql += string.Format(" AND " + Issues.SQL_COL_LOCATION_ID + " = {0}", lst.ValueItem);
				}

				// Vendor/Mechanic
				if (lstBuildingVendor.SelectedIndex > 0) {
					lst = lstBuildingVendor.SelectedItem;
					xsql += string.Format("  AND " + Issues.SQL_COL_VENDOR_ID + " = {0}", lst.ValueItem);
				}

			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}
			return xsql;

		}


		private void colsGrid()
		{
			// Tenants
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Tenant", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Category", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Created By", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Date", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Due", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Responsible", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Status", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayTenants").Columns.Add(new DataColumn("Priority", System.Type.GetType("System.String")));

			// Buildings
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Building", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Location", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Vendor", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Category", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Created By", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Reported", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Assigned", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Responsible", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Status", System.Type.GetType("System.String")));
			dsIssues.Tables("DisplayBuildings").Columns.Add(new DataColumn("Priority", System.Type.GetType("System.String")));



		}


		private void getTenants()
		{
			Tenants[] tens = Tenants.load(true, (optName.IsChecked ? true : false));
			lstTenants.Items.Clear();
			lstTenants.Items.Add(new ListItem("<ALL>", 0));
			if ((tens != null) & tens.Length > 0) {
				foreach (Tenants ten in tens) {
					lstTenants.Items.Add(new ListItem(ten.getBuildingNumber + ten.getTenantNumber + ": " + ten.getTenantName, ten.getSequenceNumber));
				}
			}
			lstTenants.SelectedIndex = 0;

		}


		private void getBuildings()
		{
			Building[] blds = Building.load;
			lstBuildings.Items.Clear();
			lstBuildings.Items.Add(new ListItem("<ALL>", 0));
			if ((blds != null) & blds.Length > 0) {
				foreach (Building bld in blds) {
					lstBuildings.Items.Add(new ListItem(bld.BuildingNumber + ": " + bld.Address1, bld.BuildingNumber));
				}
			}
			lstBuildings.SelectedIndex = 0;

		}


		private void frmIssueDashboard_Load(object sender, System.EventArgs e)
		{
			System.Drawing.Rectangle rect = Utils.getForm(this);

			this.Top = rect.Left;
			this.Left = rect.Top;
			this.Width = rect.Width;
			this.Height = rect.Height;
			cmdAssignEmail.Visibility = (m_email ? Windows.Visibility.Visible : Windows.Visibility.Collapsed);
			getTenants();
			getBuildings();
			Accounts[] accts = Accounts.load;
			try {
				lstTenantCreated.Items.Add(new ListItem("<ALL>", 0));
			} catch (System.Exception ex) {
				string s = ex.Message;
			}
			lstBuildingCreated.Items.Add(new ListItem("<ALL>", 0));
			lstTenantResponsible.Items.Add(new ListItem("<ALL>", 0));
			lstBuildingResponsible.Items.Add(new ListItem("<ALL>", 0));
			if ((accts != null) & accts.Length > 0) {
				foreach (Accounts acct in accts) {
					lstTenantCreated.Items.Add(new ListItem(acct.getName, acct.getAccountId));
					lstBuildingCreated.Items.Add(new ListItem(acct.getName, acct.getAccountId));
					lstTenantResponsible.Items.Add(new ListItem(acct.getName, acct.getAccountId));
					lstBuildingResponsible.Items.Add(new ListItem(acct.getName, acct.getAccountId));
				}
			}
			lstTenantCreated.SelectedIndex = 0;
			lstBuildingCreated.SelectedIndex = 0;
			lstTenantResponsible.SelectedIndex = 0;
			lstBuildingResponsible.SelectedIndex = 0;
			lstTenantStatus.Items.Add(new ListItem("<ALL>", 0));
			lstBuildingStatus.Items.Add(new ListItem("<ALL>", 0));
			for (Int16 i = 0; i <= Issues.issueStatus.Length - 1; i++) {
				lstTenantStatus.Items.Add(new ListItem(Issues.issueStatus(i), i));
				lstBuildingStatus.Items.Add(new ListItem(Issues.issueStatus(i), i));
			}
			lstTenantStatus.SelectedIndex = 0;
			lstBuildingStatus.SelectedIndex = 0;
			lstTenantPriority.Items.Add(new ListItem("<ALL>", 0));
			lstBuildingPriority.Items.Add(new ListItem("<ALL>", 0));
			for (Int16 i = 0; i <= Issues.issuePriority.Length - 1; i++) {
				lstTenantPriority.Items.Add(new ListItem(Issues.issuePriority(i), i));
				lstBuildingPriority.Items.Add(new ListItem(Issues.issuePriority(i), i));
			}
			lstTenantPriority.SelectedIndex = 0;
			lstBuildingPriority.SelectedIndex = 0;
			lstTenantCategory.Items.Add(new ListItem("<ALL>", 0));
			lstBuildingCategory.Items.Add(new ListItem("<ALL>", 0));
			Categories[] cats = Categories.load;
			if ((cats != null)) {
				foreach (Categories cat in cats) {
					lstTenantCategory.Items.Add(new ListItem(cat.getCategory, cat.getCategoryId));
					lstBuildingCategory.Items.Add(new ListItem(cat.getCategory, cat.getCategoryId));
				}
			}
			lstTenantCategory.SelectedIndex = 0;
			lstBuildingCategory.SelectedIndex = 0;
			lstBuildingLocation.Items.Add(new ListItem("<ALL>", 0));
			BuildingLocation[] locs = BuildingLocation.load();
			if ((locs != null)) {
				foreach (BuildingLocation lc in locs) {
					lstBuildingLocation.Items.Add(new ListItem(lc.Location, lc.ID));
				}
			}
			lstBuildingLocation.SelectedIndex = 0;
			lstBuildingVendor.Items.Add(new ListItem("<ALL>", 0));
			Vendors[] vends = Vendors.load;
			if ((vends != null)) {
				foreach (Vendors vend in vends) {
					lstBuildingVendor.Items.Add(new ListItem(vend.VendorName, vend.ID));
				}
			}
			lstBuildingVendor.SelectedIndex = 0;

			dsIssues.Tables.Add("DisplayTenants");
			dsIssues.Tables.Add("DisplayBuildings");
			dsIssues.Tables.Add("Issues");
			dsIssues.Tables.Add("Accounts");
			dsIssues.Tables.Add("Tenants");
			dsIssues.Tables.Add("Buildings");
			colsGrid();

			getTenantIssues();
			try {
				grdTenantIssues.DataContext = dsIssues.Tables("DisplayTenants");
			} catch (System.Exception ex) {
			}

			getBuildingIssues();
			try {
				grdBuildingIssues.DataContext = dsIssues.Tables("DisplayBuildings");
			} catch (System.Exception ex) {
			}

			loading = false;

		}

		private void opt_Click(object sender, System.EventArgs e)
		{
			getTenants();
		}


		private void lst_TenantsSelectionChanged(object sender, System.EventArgs e)
		{
			if (!loading) {
				grdTenantIssues.DataContext = null;
				getTenantIssues();
				getBuildingIssues();
				grdTenantIssues.DataContext = dsIssues.Tables("DisplayTenants");
			}

		}


		private void lst_BuildingsSelectionChanged(object sender, System.EventArgs e)
		{
			if (!loading) {
				grdBuildingIssues.DataContext = null;
				getTenantIssues();
				getBuildingIssues();
				grdBuildingIssues.DataContext = dsIssues.Tables("Displaybuildings");
			}
		}


		private void grdTenantIssues_DoubleClick(object sender, System.EventArgs e)
		{
			System.Windows.Controls.DataGrid grd = (System.Windows.Controls.DataGrid)sender;
			DataRowView dr = (DataRowView)grd.SelectedItem;
			long id = Convert.ToInt64(dr("ID"));
			if (id > 0) {
				frmIssue frm = new frmIssue();
				frm.issueId = id;
				frm.BringToFront();
				frm.ShowDialog();
				getTenantIssues();
				getBuildingIssues();
			}

		}


		private void grdBuildingIssues_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			System.Windows.Controls.DataGrid grd = (System.Windows.Controls.DataGrid)sender;
			DataRowView dr = (DataRowView)grd.SelectedItem;
			long id = Convert.ToInt64(dr("ID"));
			if (id > 0) {
				frmIssueBuilding frm = new frmIssueBuilding();
				frm.issueId = id;
				frm.ShowDialog();
				getTenantIssues();
				getBuildingIssues();
			}

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			if (m_email) {
				m_issueId = -1;
			}
			this.Close();
		}


		private void grdIssues_SelectionChanged(object sender, System.EventArgs e)
		{
			System.Windows.Controls.DataGrid grd = (System.Windows.Controls.DataGrid)sender;
			try {
				DataRowView dr = (DataRowView)grd.SelectedItem;
				long id = Convert.ToInt64(dr("ID"));
				if (id > 0) {
					if (m_email) {
						m_issueId = id;
					}
				}
			} catch {
			}

		}


		private void cmdAssign_Click(object sender, System.EventArgs e)
		{
			if (m_issueId <= 0) {
				switch (Interaction.MsgBox("You have not assigned the email to an issue.  Do you wish to do so?", MsgBoxStyle.YesNo)) {
					case MsgBoxResult.Yes:
						return;

						break;
					case MsgBoxResult.No:
						break;
				}
			}
			this.Close();

		}

		private void dtTenantFrom_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			chkTenantFrom.IsChecked = true;
		}

		private void dtTenantTo_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			chkTenantTo.IsChecked = true;
		}

		private void dtReportedFrom_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			chkReportedFrom.IsChecked = true;
		}

		private void dtReportedTo_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			chkReportedTo.IsChecked = true;
		}

		private void dtAssignedFrom_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			chkAssignedFrom.IsChecked = true;
		}

		private void dtAssignedTo_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
		{
			chkAssignedTo.IsChecked = true;
		}


		private void grdBuildingIssues_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			System.Windows.Controls.DataGrid grd = (System.Windows.Controls.DataGrid)sender;
			try {
				DataRowView dr = (DataRowView)grd.SelectedItem;
				long id = Convert.ToInt64(dr("ID"));
				if (id > 0) {
					if (m_email) {
						m_issueId = id;
					}
				}
			} catch {
			}

		}
		public frmIssuesDashboard()
		{
			Loaded += frmIssueDashboard_Load;
			Loaded += Window_Loaded;
			Closed += frmIssuesDashboard_Closed;
		}
	}
}
