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
	internal class clsConnection
	{
//local variable(s) to hold property value(s)
			//ADODB.Connection 'local copy
		private object mvarCn;
//******************************************************************************
		public void CloseDB()
		{
			//******************************************************************************

			 // ERROR: Not supported in C#: OnErrorStatement

			//UPGRADE_WARNING: Couldn't resolve default property of object cn.Close. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			cn.Close();
			//UPGRADE_NOTE: Object cn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			cn = null;

		}
//******************************************************************************
		public int OpenDB(ref string userid, ref string datasource, ref short opentype)
		{
			int functionReturnValue = 0;
			//******************************************************************************

			 // ERROR: Not supported in C#: OnErrorStatement

			functionReturnValue = 0;
			cn = new ADODB.Connection();
			var _with1 = mvarCn;
			//UPGRADE_WARNING: Couldn't resolve default property of object mvarCn.Provider. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			_with1.Provider = "Microsoft.ACE.OLEDB.12.0";
			//.Properties("Jet OLEDB:System database") = DefaultPath & "\pmsys.mdw"
			//UPGRADE_WARNING: Couldn't resolve default property of object mvarCn.Mode. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			_with1.Mode = opentype;
			//.Open "Data Source=." & datasource & ";User Id=" & userid & ";Password=;"
			//UPGRADE_WARNING: Couldn't resolve default property of object mvarCn.Open. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			_with1.Open(datasource);
			goto opendb_exit;
			OpenDB_error:

			functionReturnValue = Err().Number;
			LogError.MsgBox_Renamed("Unable to create database object: " + Err().Description);
			 // ERROR: Not supported in C#: ResumeStatement

			opendb_exit:
			return functionReturnValue;


		}
//******************************************************************************
		public object cn {
//ADODB.Connection
//used when retrieving value of a property, on the right side of an assignment.
//Syntax: Debug.Print X.Connection
			get { return mvarCn; }
// ADODB.Connection)
//******************************************************************************
//used when assigning an Object to the property, on the left side of a Set statement.
//Syntax: Set x.Connection = Form1
			set { mvarCn = new ADODB.Connection(); }
		}
//******************************************************************************
//UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		private void Class_Terminate_Renamed()
		{
			//******************************************************************************


		}
		protected override void Finalize()
		{
			Class_Terminate_Renamed();
			base.Finalize();
		}
	}
}
