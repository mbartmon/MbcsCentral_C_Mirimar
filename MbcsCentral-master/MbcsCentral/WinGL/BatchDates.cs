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
	internal partial class BatchDates : System.Windows.Forms.Form
	{
		private void Cmd_Click(System.Object eventSender, System.EventArgs eventArgs)
		{
			short Index = Cmd.GetIndex(eventSender);

			switch (Index) {
				case 0:
					// SAVE
					//UPGRADE_WARNING: Couldn't resolve default property of object dtStart.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.bStartDate = dtStart.Value;
					//UPGRADE_WARNING: Couldn't resolve default property of object dtEnd.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WinglGlobal.bEndDate = dtEnd.Value;
					WinglGlobal.bMonth = lstMonth.SelectedIndex + 1;
					WinglGlobal.bYear = Convert.ToInt16(Microsoft.VisualBasic.Compatibility.VB6.Support.GetItemString(lstYear, lstYear.SelectedIndex));
					break;
				case 1:
					// CANCEL
					WinglGlobal.BatchDateAdj = false;
					break;
			}

			this.Close();

		}
		private void BatchDates_Load(System.Object eventSender, System.EventArgs eventArgs)
		{
			object ni = null;
			object i = null;
			object j = null;

			dtStart.Value = DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Today), 1, 1);
			dtEnd.Value = DateAndTime.Today;
			lstMonth.SelectedIndex = DateAndTime.Month(DateAndTime.Today) - 1;
			//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			j = 0;
			lstYear.Items.Clear();
			for (i = DateAndTime.Year(DateAndTime.Today) - 25; i <= DateAndTime.Year(DateAndTime.Today) + 1; i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lstYear.Items.Add(i);
				//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Couldn't resolve default property of object ni. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				if (i == DateAndTime.Year(DateAndTime.Today))
					ni = j;
				//UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				j = j + 1;
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object ni. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lstYear.SelectedIndex = ni;
			WinglGlobal.RecallForm(ref this);

		}
		private void BatchDates_FormClosed(System.Object eventSender, System.Windows.Forms.FormClosedEventArgs eventArgs)
		{

			WinglGlobal.StoreForm(ref this);

		}
	}
}
