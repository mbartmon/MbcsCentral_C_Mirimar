using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
 // ERROR: Not supported in C#: OptionDeclaration
namespace WinGL
{
	internal partial class VendorDetail : System.Windows.Forms.Form
	{
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {

				case 0:
					// SAVE
					if (WinglGlobal.AddFlag) {
						WinglGlobal.tblVendor2.AddNew();
					} else {
						WinglGlobal.tblVendor2.Edit();
					}
					WinGlobal.move_scr_tbl(ref WinglGlobal.tblVendor2, ref this);
					if (lstJournals.ListIndex > -1) {
						lstJournals.Row = lstJournals.ListIndex;
						WinglGlobal.tblVendor2.Fields["Journal"].Value = lstJournals.ColList;
					}
					WinglGlobal.tblVendor2.Update();
					WinglGlobal.GetVendors(Vendors);
					this.Close();
					break;

				case 1:
					// EXIT
					this.Close();
					break;

				case 2:
					// Categories
					My.MyProject.Forms.VendorCategories.Top = 0;
					My.MyProject.Forms.VendorCategories.Left = 0;
					My.MyProject.Forms.VendorCategories.Show();
					break;

			}

		}
		private void VendorDetail_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object i = null;

			System.DateTime sdate = default(System.DateTime);
			System.DateTime edate = default(System.DateTime);
			//UPGRADE_WARNING: Arrays in structure rs may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset rs = null;

			this.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(5340);
			this.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(6225);
			WinglGlobal.GetJournals(ref this);
			if (!WinglGlobal.AddFlag) {
				WinGlobal.move_tbl_scr(ref WinglGlobal.tblVendor2, ref this);
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(WinglGlobal.tblVendor2.Fields["Journal"].Value)) {
					for (i = 0; i <= lstJournals.ListCount - 1; i++) {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						lstJournals.Row = i;
						if (Strings.LCase(WinGlobal.noNulls(ref WinglGlobal.tblVendor2.Fields["Journal"])) == Strings.LCase(lstJournals.ColList)) {
							//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							lstJournals.ListIndex = i;
							break; // TODO: might not be correct. Was : Exit For
						}
					}
				}
				//sdate = DateSerial(Year(Date), 1, 1): edate = DateSerial(Year(Date), 12, 31)
				//xsql = "SELECT sum(Amount) as CheckAmount FROM Apchecks WHERE Sequence <> 0 AND Date BETWEEN #" & sdate & "# AND #" & edate & "#"
				//xsql = xsql & " AND VendorNumber = " & tblVendor2("VendorNumber")
				//Set rs = GLdb.OpenRecordset(xsql)
				//If Not rs.EOF Then
				//    txtCurPurchase.Text = Format(rs("CheckAmount"), "###,###,##0.00")
				//    rs.Close
				//End If
				//sdate = DateSerial(Year(Date) - 1, 1, 1): edate = DateSerial(Year(Date) - 1, 12, 31)
				//xsql = "SELECT sum(Amount) as CheckAmount FROM Apchecks WHERE Sequence <> 0 AND Date BETWEEN #" & sdate & "# AND #" & edate & "#"
				//xsql = xsql & " AND VendorNumber = " & tblVendor2("VendorNumber")
				//Set rs = GLdb.OpenRecordset(xsql)
				//If Not rs.EOF Then
				//    txtPriorPurchase.Text = Format(rs("CheckAmount"), "###,###,##0.00")
				//    rs.Close
				//End If

			} else {
				txtVendors[0].Enabled = false;
			}

		}
	}
}
