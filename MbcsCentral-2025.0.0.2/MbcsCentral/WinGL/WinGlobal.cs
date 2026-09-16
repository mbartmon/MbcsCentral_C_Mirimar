using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
 // ERROR: Not supported in C#: OptionDeclaration
using VB = Microsoft.VisualBasic;
namespace WinGL
{
	static class WinGlobal
	{
		[DllImport("advapi32.dll", EntryPoint = "GetUserNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int GetUserName(string lpbuffer, ref int nSize);
		[DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int WaitForSingleObject(int hHandle, int dwMilliseconds);
		[DllImport("Kernel32", EntryPoint = "CreateProcessA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
//UPGRADE_WARNING: Structure PROCESS_INFORMATION may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
//UPGRADE_WARNING: Structure STARTUPINFO may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
		public static extern int CreateProcess(int lpApplicationName, string lpCommandLine, int lpProcessAttributes, int lpThreadAttributes, int bInheritHandles, int dwCreationFlags, int lpEnvironment, int lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, ref PROCESS_INFORMATION lpProcessInformation);
		[DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int CloseHandle(int hObject);
		[DllImport("Kernel32", EntryPoint = "GetTempPathA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int GetTempPath(int nBufferLength, string lpbuffer);
		public const int NORMAL_PRIORITY_CLASS = 0x20;
		public const short INFINITE = -1;

		public struct STARTUPINFO
		{
			public int cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public int dwX;
			public int dwY;
			public int dwXSize;
			public int dwYSize;
			public int dwXCountChars;
			public int dwYCountChars;
			public int dwFillAttribute;
			public int dwFlags;
			public short wShowWindow;
			public short cbReserved2;
			public int lpReserved2;
			public int hStdInput;
			public int hStdOutput;
			public int hStdError;
		}

		public struct PROCESS_INFORMATION
		{
			public int hProcess;
			public int hThread;
			public int dwProcessID;
			public int dwThreadID;
		}
		public static void ExecCmd(ref string cmdline)
		{
			int ret = 0;


			PROCESS_INFORMATION proc = default(PROCESS_INFORMATION);
			STARTUPINFO start = default(STARTUPINFO);

			 // ERROR: Not supported in C#: OnErrorStatement

			//Initialize the STARTUPINFO structure:
			start.cb = Strings.Len(start);
			// Start the shelled application:
			ret = CreateProcess(0, cmdline, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, 0, ref start, ref proc);
			// Wait for the shelled application to finish:
			ret = WaitForSingleObject(proc.hProcess, INFINITE);
			ret = CloseHandle(proc.hProcess);
			goto ExecCmd_exit;
			ExecCmd_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			ExecCmd_exit:


		}
		public static string GetDrive(ref string f)
		{
			object x = null;


			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			i = Strings.InStr(f, ":");

			if (i == 2) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = Strings.Left(f, 2);
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				x = "";
			}

			//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			return x;

		}
		public static string GetFileName(ref string f)
		{


			short i = 0;
			string x = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			x = f;
			for (i = Strings.Len(f); i >= 1; i += -1) {
				if (Strings.Mid(f, i, 1) == "\\" | Strings.Mid(f, i, 1) == ":") {
					x = Strings.Mid(f, i + 1);
					break; // TODO: might not be correct. Was : Exit For
				}
			}

			return x;

		}
		public static string GetPath(ref string f)
		{


			short i = 0;
			string x = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			x = "\\";
			for (i = Strings.Len(f); i >= 1; i += -1) {
				if (Strings.Mid(f, i, 1) == "\\") {
					x = Strings.Left(f, i);
					break; // TODO: might not be correct. Was : Exit For
				}
			}

			return x;

		}

		public static string rpad(ref System.Windows.Forms.Control c)
		{



			string x = null;
			short n = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			x = c.Text;
			n = Strings.Len(x);
			//UPGRADE_WARNING: Couldn't resolve default property of object c.MaxLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			//n = c.MaxLength

			if (Strings.Len(x) < n) {
				x = x + new string(" ", n - Strings.Len(x));
			}

			return x;

		}
		public static void move_scr_tbl(ref DAO.Recordset tbl, ref System.Windows.Forms.Form frm)
		{
			object fpCombo = null;

			System.Windows.Forms.Control c = null;
			object x = null;
			//UPGRADE_WARNING: Arrays in structure DAOtbl may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
			DAO.Recordset DAOtbl = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			foreach (Control c_loopVariable in frm.Controls) {
				c = c_loopVariable;
				//UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				if ((!string.IsNullOrEmpty(c.Tag) & c.Enabled == true) | Information.TypeName(c) == "RichTextBox") {
					//UPGRADE_WARNING: Couldn't resolve default property of object fpCombo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					switch (tbl.Fields[c.Tag].Type) {
						case DAO.DataTypeEnum.dbText:
						case DAO.DataTypeEnum.dbMemo:
						case ADODB.DataTypeEnum.adVarWChar:
						case ADODB.DataTypeEnum.adWChar:
							//UPGRADE_WARNING: TypeOf has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (c is AxLpLib.AxfpCombo) {
								Err().Clear();
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = null;
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Row = c.ListIndex
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Col = 1
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ColList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//x = c.ColList
								//UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
								if ((x != null)) {
									//UPGRADE_WARNING: Couldn't resolve default property of object c.ColList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//tbl.Fields(c.Tag).Value = c.ColList
								}
							} else {
								tbl.Fields[c.Tag].Value = rpad(ref c);
							}
							break;
						case DAO.DataTypeEnum.dbBoolean:
						case ADODB.DataTypeEnum.adBoolean:
							break;
						//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//If c.Value = 1 Then
						//tbl.Fields(c.Tag).Value = True
						//Else
						//tbl.Fields(c.Tag).Value = False
						//End If
						case DAO.DataTypeEnum.dbDate:
						case ADODB.DataTypeEnum.adDate:
						case ADODB.DataTypeEnum.adDBDate:
							Err().Clear();
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = c.Text;
							if (Err().Number == 0) {
								if (string.IsNullOrEmpty(Strings.Trim(c.Text))) {
									//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
									tbl.Fields[c.Tag].Value = System.DBNull.Value;
								} else {
									tbl.Fields[c.Tag].Value = Strings.Trim(c.Text);
								}
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//tbl.Fields(c.Tag).Value = c.Value
							}
							break;
						case fpCombo:
							break;
						default:
							//UPGRADE_WARNING: TypeOf has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (c is AxLpLib.AxfpCombo) {
								Err().Clear();
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = null;
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Row = c.ListIndex
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Col = 1
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ColList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//x = c.ColList
								//UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
								if ((x != null)) {
									//UPGRADE_WARNING: Couldn't resolve default property of object c.ColList. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//tbl.Fields(c.Tag).Value = c.ColList
								}
							} else {
								tbl.Fields[c.Tag].Value = Conversion.Val(c.Text);
							}
							break;
					}
				}
			}

		}
		public static void move_tbl_scr(ref object tbl, ref System.Windows.Forms.Form frm)
		{

			System.Windows.Forms.Control c = null;
			object x = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			foreach (Control c_loopVariable in frm.Controls) {
				c = c_loopVariable;
				if (!string.IsNullOrEmpty(c.Tag)) {
					//UPGRADE_WARNING: Couldn't resolve default property of object tbl(c.Tag).type. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					switch (tbl(c.Tag).type) {
						case DAO.DataTypeEnum.dbText:
						case DAO.DataTypeEnum.dbMemo:
						case ADODB.DataTypeEnum.adVarWChar:
						case ADODB.DataTypeEnum.adWChar:
							//UPGRADE_WARNING: TypeOf has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (c is AxLpLib.AxfpCombo) {
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = tbl(c.Tag);
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ColumnSearch. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ColumnSearch = 1
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchMethod. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchText = x
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Action = LpLib.enumAction.ActionSearch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ListIndex = c.SearchIndex
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								c.Text = Strings.RTrim(tbl(c.Tag));
							}
							break;
						case DAO.DataTypeEnum.dbBoolean:
						case ADODB.DataTypeEnum.adBoolean:
							//UPGRADE_WARNING: Couldn't resolve default property of object tbl(c.Tag). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							if (tbl(c.Tag)) {
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Value = 1
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Value = 0
							}
							break;
						case DAO.DataTypeEnum.dbCurrency:
						case ADODB.DataTypeEnum.adCurrency:
							//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl(c.Tag), c_format(ref c));
							break;
						//UPGRADE_WARNING: Couldn't resolve default property of object c.MaxLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//c.Text = lpad(CStr(x), c.MaxLength)
						case DAO.DataTypeEnum.dbDate:
						case ADODB.DataTypeEnum.adDate:
						case ADODB.DataTypeEnum.adDBDate:
							Err().Clear();
							//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							c.Text = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Month(tbl(c.Tag)), "00") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Day(tbl(c.Tag)), "00") + "/" + Microsoft.VisualBasic.Compatibility.VB6.Support.Format(DateAndTime.Year(tbl(c.Tag)), "0000");
							if (Err().Number != 0) {
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Value = tbl(c.Tag)
								if (Err().Number == 380) {
									//UPGRADE_WARNING: Couldn't resolve default property of object c.Value. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//c.Value = ""
								}
							}
							break;
						case DAO.DataTypeEnum.dbSingle:
						case ADODB.DataTypeEnum.adSingle:
							//UPGRADE_WARNING: TypeOf has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (c is AxLpLib.AxfpCombo) {
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = tbl(c.Tag);
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ColumnSearch. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ColumnSearch = 1
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchMethod. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchText = x
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Action = LpLib.enumAction.ActionSearch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ListIndex = c.SearchIndex
							} else {
								if (!string.IsNullOrEmpty(Strings.Trim(c.Text))) {
									//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl(c.Tag), m_format(ref c));
								} else {
									//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
									x = Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl(c.Tag));
								}
								//UPGRADE_WARNING: Couldn't resolve default property of object c.MaxLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Text = lpad(CStr(x), c.MaxLength)
							}
							break;
						case DAO.DataTypeEnum.dbMemo:
							//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							c.Text = Strings.RTrim(tbl(c.Tag));
							break;
						default:
							//UPGRADE_WARNING: TypeOf has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							if (c is AxLpLib.AxfpCombo) {
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								x = tbl(c.Tag);
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ColumnSearch. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ColumnSearch = 1
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchMethod. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchMethod = LpLib.enumSearchMethod.SearchMethodExactMatch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.SearchText = x
								//UPGRADE_WARNING: Couldn't resolve default property of object c.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.Action = LpLib.enumAction.ActionSearch
								//UPGRADE_WARNING: Couldn't resolve default property of object c.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//UPGRADE_WARNING: Couldn't resolve default property of object c.SearchIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								//c.ListIndex = c.SearchIndex
							} else {
								//UPGRADE_WARNING: Couldn't resolve default property of object tbl(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								c.Text = Strings.RTrim(Microsoft.VisualBasic.Compatibility.VB6.Support.Format(tbl(c.Tag)));
							}
							break;
					}
				}
			}

		}
		public static string c_format(ref AxMBText.AxTextField c)
		{


			string xfmt = null;
			short m = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			m = c.MaxLength - 4;
			return new string("#", m) + "0.00";

		}
		public static string m_format(ref AxMBText.AxTextField c)
		{


			string x = null;
			short i = 0;

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_NOTE: Text was upgraded to CtlText. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			x = c.CtlText;
			for (i = 1; i <= Strings.Len(x); i++) {
				if (Strings.Mid(x, i, 1) == "_")
					Strings.Mid(x, i, 1) = "0";
			}
			return x;

		}
		public static string lpad(ref string s, ref short l)
		{


			 // ERROR: Not supported in C#: OnErrorStatement

			return new string(" ", l - Strings.Len(s)) + s;

		}
		public static string rtpad(ref string s, ref short l)
		{
			string functionReturnValue = null;

			 // ERROR: Not supported in C#: OnErrorStatement

			functionReturnValue = s;
			functionReturnValue = functionReturnValue + new string(" ", System.Math.Abs(l) - Strings.Len(s));
			if (Strings.Len(rtpad()) > l) {
				functionReturnValue = Strings.Left(rtpad(), l);
			}
			return functionReturnValue;

		}
		public static System.DateTime null_date(ref object d)
		{
			System.DateTime functionReturnValue = default(System.DateTime);

			//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			if (Information.IsDBNull(d)) {
				functionReturnValue = Convert.ToDateTime("01/01/1900");
			} else {
				//UPGRADE_WARNING: Couldn't resolve default property of object d. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				functionReturnValue = d;
			}
			return functionReturnValue;

		}
		public static string reverse_name(ref string n)
		{
			string functionReturnValue = null;

			short p = 0;
			string x = null;
			string y = null;

			p = Strings.InStr(n, ",");
			if (p > 0) {
				x = Strings.Trim(Strings.Left(n, p - 1));
				y = Strings.Trim(Strings.Mid(n, p + 1));
				p = Strings.InStr(Strings.LCase(y), "jr");
				if (p > 0) {
					x = x + " " + Strings.Mid(y, p);
					y = Strings.Trim(Strings.Left(y, p - 1));
				}
				functionReturnValue = y + " " + x;
			} else {
				functionReturnValue = n;
			}
			return functionReturnValue;

		}
		public static string noNulls(ref object x)
		{

			short i = 0;
			string z = null;

			z = " ";
			 // ERROR: Not supported in C#: OnErrorStatement

			for (i = 1; i <= Strings.Len(x); i++) {
				//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				//UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				if (!Information.IsDBNull(Strings.Mid(x, i, 1)) & Strings.Asc(Strings.Mid(x, i)) >= 32) {
					//UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					z = z + Strings.Mid(x, i, 1);
				}
			}

			return Strings.Trim(z);

		}
//UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		public static string MemoParse(ref object subString, ref short maxlen, ref string str_Renamed)
		{
			string functionReturnValue = null;

			object i = null;

			str_Renamed = "";
			if (Strings.Len(subString) <= maxlen) {
				//UPGRADE_WARNING: Couldn't resolve default property of object subString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				functionReturnValue = subString;
			} else {
				for (i = maxlen + 1; i >= 1; i += -1) {
					//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					//UPGRADE_WARNING: Couldn't resolve default property of object subString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					if (Strings.Mid(subString, i, 1) == " ") {
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object subString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						functionReturnValue = Strings.Left(subString, i - 1);
						//UPGRADE_WARNING: Couldn't resolve default property of object i. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						//UPGRADE_WARNING: Couldn't resolve default property of object subString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						str_Renamed = Strings.Mid(subString, i + 1, Strings.Len(Strings.Mid(subString, i + 1)) - 1);
						goto MemoParse_exit;
					}
				}
				//UPGRADE_WARNING: Couldn't resolve default property of object subString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				functionReturnValue = subString;
			}
			goto MemoParse_exit;
			MemoParse_error:

			LogError.MsgBox_Renamed("", MsgBoxStyle.Critical, WinglGlobal.sProcID);
			 // ERROR: Not supported in C#: ResumeStatement

			MemoParse_exit:
			return functionReturnValue;


		}
		public static System.DateTime FirstOfNextMonth(ref System.DateTime dtm)
		{
			return DateAndTime.DateSerial(DateAndTime.Year(dtm), DateAndTime.Month(dtm) + 1, 1);
		}
		public static System.DateTime LastOfThisMonth(ref System.DateTime dtm)
		{
			return DateAndTime.DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, FirstOfNextMonth(ref dtm));
		}

//******************************************************************************
		public static void GetForm(ref System.Windows.Forms.Form f, ref short t, ref short l, ref short h, ref short w)
		{
			//******************************************************************************

			f.Top = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Top", Convert.ToString(t))));
			f.Left = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Left", Convert.ToString(l))));
			f.Height = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsY(Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Height", Convert.ToString(h))));
			f.Width = Microsoft.VisualBasic.Compatibility.VB6.Support.TwipsToPixelsX(Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Width", Convert.ToString(w))));
			//UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
			f.WindowState = Conversion.Val(Interaction.GetSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "WindowState", Convert.ToString(Constants.vbNormal)));

		}
//******************************************************************************
		public static void SaveForm(ref System.Windows.Forms.Form f)
		{
			//******************************************************************************

			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Top", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(f.Top)));
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Left", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(f.Left)));
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Height", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsY(f.Height)));
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "Width", Convert.ToString(Microsoft.VisualBasic.Compatibility.VB6.Support.PixelsToTwipsX(f.Width)));
			Interaction.SaveSetting(WinGL.My.MyProject.Application.Info.Title, "Forms_" + f.Name, "WindowState", Convert.ToString(f.WindowState));

		}
	}
}
