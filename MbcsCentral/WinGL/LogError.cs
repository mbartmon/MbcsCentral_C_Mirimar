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
	static class LogError
	{
		private static object[] errStack;
		private static short errPtr;
//UPGRADE_NOTE: MsgBox was upgraded to MsgBox_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		public static short MsgBox_Renamed(string sText, short iButton = 0, string sTitle = "")
		{

			short nResult = 0;

			if (Err().Number != 0) {
				var _with1 = Information.Err;
				sText = sText + Constants.vbCrLf + Constants.vbCrLf + "Error " + Information.Err().Number + " '" + Information.Err().Description + "' occurred in " + WinglGlobal.sProcID + (Information.Erl() != 0 ? " at line " + Convert.ToString(Information.Erl()) + "." : ".");
			}

			nResult = Interaction.MsgBox(WinGlobal.noNulls(ref sText), iButton, sTitle);

			//UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
			if ((iButton != null)) {
				if ((iButton & MsgBoxStyle.Critical) == MsgBoxStyle.Critical) {
					LogError(WinGlobal.noNulls(ref sText), nResult, WinglGlobal.sProcID);
				}
			}

			return nResult;

		}
		public static void LogError(string sText, ref short nResult, ref string sProcName, ref string filename = "")
		{

			//UPGRADE_WARNING: Arrays in structure tblLog may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Database dbErr = null;
			DAO.Recordset tblLog = null;
			object errx = null;
			object erlx = null;
			object errorx = null;

			short fi = 0;
			if (!string.IsNullOrEmpty(filename)) {
				fi = 1;
				FileSystem.FileOpen(fi, filename, OpenMode.Append);
				FileSystem.WriteLine(fi, Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Now, "yyyy-m-dd hh:mm:ss") + " : " + sText);
				FileSystem.FileClose(fi);
				return;
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object erlx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			erlx = Information.Erl();
			//UPGRADE_WARNING: Couldn't resolve default property of object errx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			errx = Err().Number;
			//UPGRADE_WARNING: Couldn't resolve default property of object errorx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			errorx = Conversion.ErrorToString();
			 // ERROR: Not supported in C#: OnErrorStatement

			tblLog = WinglGlobal.PMSdb.OpenRecordset("Error Log");

			tblLog.AddNew();
			tblLog.Fields["Application Name"].Value = WinGL.My.MyProject.Application.Info.Title;
			tblLog.Fields["Module/Procedure"].Value = sProcName;
			//UPGRADE_WARNING: Couldn't resolve default property of object erlx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblLog.Fields["Line Number"].Value = erlx;
			//UPGRADE_WARNING: Couldn't resolve default property of object errx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblLog.Fields["Error Number"].Value = errx;
			tblLog.Fields["Application Message"].Value = WinGlobal.noNulls(ref sText);
			//UPGRADE_WARNING: Couldn't resolve default property of object errorx. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			tblLog.Fields["VB Error Message"].Value = errorx;
			tblLog.Fields["UserID"].Value = WinglGlobal.userid;
			tblLog.Fields["Result"].Value = nResult;
			tblLog.Update();
			tblLog.Close();
			LogError_exit:

			Err().Clear();

		}
		public static void errPush(ref string s)
		{

			short i = 0;

			errPtr = errPtr + 1;
			Array.Resize(ref errStack, errPtr + 1);
			if (errPtr > 1) {
				for (i = errPtr; i >= 2; i += -1) {
					//UPGRADE_WARNING: Couldn't resolve default property of object errStack(i - 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object errStack(i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					errStack[i] = errStack[i - 1];
				}
			}
			//UPGRADE_WARNING: Couldn't resolve default property of object errStack(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			errStack[1] = s;

		}
		public static string errPop()
		{
			string functionReturnValue = null;

			short i = 0;

			if (errPtr > 0) {
				//UPGRADE_WARNING: Couldn't resolve default property of object errStack(1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				functionReturnValue = errStack[1];
				for (i = 1; i <= errPtr - 1; i++) {
					//UPGRADE_WARNING: Couldn't resolve default property of object errStack(i + 1). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object errStack(i). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					errStack[i] = errStack[i + 1];
				}
				errPtr = errPtr - 1;
				Array.Resize(ref errStack, errPtr + 1);
			} else {
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				//errPop = System.DBNull.Value
			}
			return "";
			return functionReturnValue;
		}
	}
}
