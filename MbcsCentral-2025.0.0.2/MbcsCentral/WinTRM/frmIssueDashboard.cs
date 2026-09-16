using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Control;
namespace WinTRM
{

	public partial class frmIssueDashboard
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


		private void getIssues()
		{
			try {
				string xsql = string.Format("{0}, {1}.{2}, {1}.{3} FROM {4}, {5} WHERE ", Issues.SQL_LOAD_DASHBOARD, IssueTenants.TABLE, IssueTenants.SQL_COL_SEQUENCE_NUMBER, IssueTenants.SQL_COL_BUILDING_NUMBER, Issues.TABLE, IssueTenants.TABLE);
				// Want a specific tenant?
				if (lstTenants.SelectedIndex > 0) {
					ListItem lst = lstTenants.SelectedItem;
					xsql += string.Format(" {0}.{1} = {2} AND ", IssueTenants.TABLE, IssueTenants.SQL_COL_SEQUENCE_NUMBER, lst.ValueItem);
				}
				xsql += string.Format(" {0}.{1} = {2}.{3}", IssueTenants.TABLE, IssueTenants.SQL_COL_ISSUE_ID, Issues.TABLE, Issues.SQL_COL_ISSUE_ID);
				string ordSql = string.Format(" ORDER BY {0};", Issues.orderBy(Issues.issueOrder.byCreateDate));
				xsql += getSelections() + ordSql;
				GlobalShared.CN.Open();
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

					DataTable tbl = dsIssues.Tables("Display");
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
				daIssues.Dispose();
				daAccounts.Dispose();
			}

		}

		private string getSelections()
		{

			string xsql = "";
			ListItem lst = null;

			try {
				// Date range
				if (dtFrom.Checked & dtTo.Checked) {
					xsql = string.Format(" AND (" + Issues.SQL_COL_TIMESTAMP + " >= #{0}# AND " + Issues.SQL_COL_TIMESTAMP + " <=#{1}#)", Strings.Format(dtFrom.Value, "MM/dd/yyyy"), Strings.Format(dtTo.Value, "MM/dd/yyyy"));
				} else if (dtFrom.Checked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_TIMESTAMP + " >= #{0}#", Strings.Format(dtFrom.Value, "MM/ddyyyy"));
				} else if (dtTo.Checked) {
					xsql = string.Format(" AND " + Issues.SQL_COL_DUE_DATE + " <= #{0}#", Strings.Format(dtTo.Value, "MM/dd/yyyy"));
				}

				// Created by
				if (lstCreatedby.SelectedIndex > 0) {
					lst = lstCreatedby.Items(lstCreatedby.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_ACCOUNT_ID + " = {0}", lst.ValueItem);
				}

				// Responsible
				if (lstResponsible.SelectedIndex > 0) {
					lst = lstResponsible.Items(lstResponsible.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PERSON_RESPONSIBLE + " = {0}", lst.ValueItem);
				}

				// Status
				if (lstStatus.SelectedIndex > 0) {
					lst = lstStatus.Items(lstStatus.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_STATUS + " = {0}", lst.ValueItem);
				}

				// Priority
				if (lstPriority.SelectedIndex > 0) {
					lst = lstPriority.Items(lstPriority.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_PRIORITY + " = {0}", lst.ValueItem);
				}

				// Category
				if (lstCategory.SelectedIndex > 0) {
					lst = lstCategory.Items(lstCategory.SelectedIndex);
					xsql += string.Format(" AND " + Issues.SQL_COL_CATEGORY + " = '{0}'", lst.DisplayItem);
				}
			} catch (System.Exception ex) {
				Interaction.MsgBox(ex.Message);
			}
			return xsql;

		}


		private void colsGrid()
		{
			dsIssues.Tables("Display").Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Tenant", System.Type.GetType("System.String")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Category", System.Type.GetType("System.String")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Created By", System.Type.GetType("System.String")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Date", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Due", System.Type.GetType("System.DateTime")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Responsible", System.Type.GetType("System.String")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Status", System.Type.GetType("System.String")));
			dsIssues.Tables("Display").Columns.Add(new DataColumn("Priority", System.Type.GetType("System.String")));

		}


		private void getTenants()
		{
			Tenants[] tens = Tenants.load(true, (optName.Checked ? true : false));
			lstTenants.Items.Clear();
			lstTenants.Items.Add(new ListItem("<ALL>", 0));
			if ((tens != null) & tens.Length > 0) {
				foreach (Tenants ten in tens) {
					lstTenants.Items.Add(new ListItem(ten.getBuildingNumber + ten.getTenantNumber + ": " + ten.getTenantName, ten.getSequenceNumber));
				}
			}
			lstTenants.SelectedIndex = 0;

		}

		private void frmIssueDashboard_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			Utils.saveForm(this);

		}


		private void frmIssueDashboard_Load(object sender, System.EventArgs e)
		{
			System.Drawing.Rectangle rect = Utils.getForm(this);

			this.Location = new System.Drawing.Point(rect.Left, rect.Top);
			this.Size = new System.Drawing.Size(rect.Width, rect.Height);
			cmdAssign.Visible = (m_email ? true : false);
			getTenants();
			Accounts[] accts = Accounts.load;
			lstCreatedby.Items.Add(new ListItem("<ALL>", 0));
			lstResponsible.Items.Add(new ListItem("<ALL>", 0));
			if ((accts != null) & accts.Length > 0) {
				foreach (Accounts acct in accts) {
					lstCreatedby.Items.Add(new ListItem(acct.getName, acct.getAccountId));
					lstResponsible.Items.Add(new ListItem(acct.getName, acct.getAccountId));
				}
			}
			lstCreatedby.SelectedIndex = 0;
			lstResponsible.SelectedIndex = 0;
			lstStatus.Items.Add(new ListItem("<ALL>", 0));
			for (Int16 i = 0; i <= Issues.issueStatus.Length - 1; i++) {
				lstStatus.Items.Add(new ListItem(Issues.issueStatus(i), i));
			}
			lstStatus.SelectedIndex = 0;
			lstPriority.Items.Add(new ListItem("<ALL>", 0));
			for (Int16 i = 0; i <= Issues.issuePriority.Length - 1; i++) {
				lstPriority.Items.Add(new ListItem(Issues.issuePriority(i), i));
			}
			lstPriority.SelectedIndex = 0;
			lstCategory.Items.Add(new ListItem("<ALL>", 0));
			Categories[] cats = Categories.load;
			if ((cats != null)) {
				foreach (Categories cat in cats) {
					lstCategory.Items.Add(new ListItem(cat.getCategory, cat.getCategoryId));
				}
			}
			lstCategory.SelectedIndex = 0;
			dsIssues.Tables.Add("Display");
			dsIssues.Tables.Add("Issues");
			dsIssues.Tables.Add("Accounts");
			dsIssues.Tables.Add("Tenants");
			dsIssues.Tables.Add("Buildings");
			colsGrid();

			getIssues();

			grdIssues.DataSource = dsIssues.Tables("Display");

			loading = false;

		}

		private void opt_Click(object sender, System.EventArgs e)
		{
			getTenants();
		}


		private void lstX_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!loading) {
				grdIssues.DataSource = null;
				getIssues();
				grdIssues.DataSource = dsIssues.Tables("Display");
			}

		}


		private void grdIssues_DoubleClick(object sender, System.EventArgs e)
		{
			DataGridView grd = (DataGridView)sender;
			long id = grd.Item(0, grd.CurrentRow.Index).Value;
			if (id > 0) {
				if (grd.Item(1, grd.CurrentRow.Index).Value.ToString.StartsWith("BLD:")) {
					frmIssueBuilding frm = new frmIssueBuilding();
					frm.issueId = id;
					frm.ShowDialog();
					frm.BringToFront();
				} else {
					frmIssue frm = new frmIssue();
					//frm.bldOnly = IIf(grd.Item(1, grd.CurrentRow.Index).Value.ToString.StartsWith("BLD:"), True, False)
					frm.issueId = id;
					frm.ShowDialog();
					frm.BringToFront();
				}
				getIssues();
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
			DataGridView grd = (DataGridView)sender;
			try {
				long id = grd.Item(0, grd.CurrentRow.Index).Value;
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
		public frmIssueDashboard()
		{
			Load += frmIssueDashboard_Load;
			FormClosing += frmIssueDashboard_FormClosing;
		}
	}
}
