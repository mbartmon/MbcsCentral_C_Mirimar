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
	internal partial class ChkRecJrn : System.Windows.Forms.Form
	{
		private void Command1_Click(System.Object eventSender, System.EventArgs eventArgs)
		{

			My.MyProject.Forms.CheckRecon.chkJournal = " ";
			if (lstJournals.ListIndex > -1) {
				lstJournals.Row = lstJournals.ListIndex;
				My.MyProject.Forms.CheckRecon.chkJournal = lstJournals.ColList;
			}
			if (chkBox.CheckState)
				My.MyProject.Forms.CheckRecon.JournalSet = true;
			this.Close();

		}

//UPGRADE_WARNING: Form event ChkRecJrn.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		private void ChkRecJrn_Activated(System.Object eventSender, System.EventArgs eventArgs)
		{

			WinglGlobal.GetJournals(ref this);

		}
	}
}
