using Microsoft.Data.ConnectionUI;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
namespace MbcsCentral
{

	public class Codes : clsDB, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		public static string TABLE = "pm_Codes";
		public const string SQL_COL_CHARGE_CODE = "Charge Code";
		public const string SQL_COL_CHARGE_DESCRIPTION = "Charge Description";
		public const string SQL_COL_G_L_CREDIT_ACCOUNT = "GL Credit Account";
		public const string SQL_COL_G_L_CREDIT_SUB_ACCOUNT = "GL Credit Sub-Account";
		public const string SQL_COL_G_L_DEBIT_ACCOUNT = "GL Debit Account";
		public const string SQL_COL_G_L_DEBIT_SUB_ACCOUNT = "GL Debit Sub-Account";
		public const string SQL_COL_G_L_CASH_ACCOUNT = "GL Cash Account";
		public const string SQL_COL_GL_CASH_SUB_ACCOUNT = "Gl Cash Sub-Account";
		public const string SQL_COL_LEGAL_CODE = "Legal Code";

		static string SQL_LOAD = " SELECT " + 
								 "[" + SQL_COL_CHARGE_CODE + "]," + 
								 "[" + SQL_COL_CHARGE_DESCRIPTION + "]," + 
								 "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," + 
								 "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," + 
								 "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," + 
								 "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," + 
								 "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," + 
								 "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]," + 
								 "[" + SQL_COL_LEGAL_CODE + "]" + 
								 " FROM " + TABLE + 
								 " WHERE [" + SQL_COL_CHARGE_CODE + "] = '?';";

		static string SQL_LOAD_ALL = " SELECT " + 
									 "[" + SQL_COL_CHARGE_CODE + "]," + 
									 "[" + SQL_COL_CHARGE_DESCRIPTION + "]," + 
									 "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," + 
									 "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," + 
									 "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," + 
									 "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," + 
									 "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," + 
									 "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]," + 
									 "[" + SQL_COL_LEGAL_CODE + "]" + 
									 " FROM " + TABLE + 
									 " ORDER BY [" + SQL_COL_CHARGE_CODE + "]";

		static string SQL_INSERT = " INSERT INTO " + TABLE + 
								   " (" + "[" + SQL_COL_CHARGE_CODE + "]," + 
								   "[" + SQL_COL_CHARGE_DESCRIPTION + "]," + 
								   "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]," + 
								   "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]," + 
								   "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]," + 
								   "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]," + 
								   "[" + SQL_COL_G_L_CASH_ACCOUNT + "]," + 
								   "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]," + 
								   "[" + SQL_COL_LEGAL_CODE + "]" + 
								   ") VALUES ('?', '?', '?', '?', '?', '?', '?', '?',?);";

		static string SQL_UPDATE = " UPDATE " + TABLE + 
								   " SET " + "[" + SQL_COL_CHARGE_CODE + "]='?'," + 
								   "[" + SQL_COL_CHARGE_DESCRIPTION + "]='?'," + 
								   "[" + SQL_COL_G_L_CREDIT_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_G_L_CREDIT_SUB_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_G_L_DEBIT_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_G_L_DEBIT_SUB_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_G_L_CASH_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_GL_CASH_SUB_ACCOUNT + "]='?'," + 
								   "[" + SQL_COL_LEGAL_CODE + "]=?" + 
								   " WHERE [" + SQL_COL_CHARGE_CODE + "] = '?';";

		protected string m_ChargeCode;
		protected string m_ChargeDescription;
		protected string m_GLCreditAccount;
		protected string m_GLCreditSubAccount;
		protected string m_GLDebitAccount;
		protected string m_GLDebitSubAccount;
		protected string m_GLCashAccount;
		protected string m_GlCashSubAccount;
		protected bool m_LegalCode;

		protected bool m_isChanged;
		public Codes() : base(TABLE, SQL_COL_CHARGE_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public Codes(string ChargeCode, string ChargeDescription, string GLCreditAccount, string GLCreditSubAccount, string GLDebitAccount, string GLDebitSubAccount, string GLCashAccount, string GlCashSubAccount, bool LegalCode) : base(TABLE, SQL_COL_CHARGE_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_ChargeCode = ChargeCode;
			m_ChargeDescription = ChargeDescription;
			m_GLCreditAccount = GLCreditAccount;
			m_GLCreditSubAccount = GLCreditSubAccount;
			m_GLDebitAccount = GLDebitAccount;
			m_GLDebitSubAccount = GLDebitSubAccount;
			m_GLCashAccount = GLCashAccount;
			m_GlCashSubAccount = GlCashSubAccount;
			m_LegalCode = LegalCode;
		}

		protected Codes fill(DataRow dr)
		{

			try {
				m_ChargeCode = (string)(Information.IsDBNull(dr[SQL_COL_CHARGE_CODE]) ? "" : dr[SQL_COL_CHARGE_CODE]);
				m_ChargeDescription = (string)(Information.IsDBNull(dr[SQL_COL_CHARGE_DESCRIPTION]) ? "" : dr[SQL_COL_CHARGE_DESCRIPTION]);
				m_GLCreditAccount = (string)(Information.IsDBNull(dr[SQL_COL_G_L_CREDIT_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CREDIT_ACCOUNT]);
				m_GLCreditSubAccount = (string)(Information.IsDBNull(dr[SQL_COL_G_L_CREDIT_SUB_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CREDIT_SUB_ACCOUNT]);
				m_GLDebitAccount = (string)(Information.IsDBNull(dr[SQL_COL_G_L_DEBIT_ACCOUNT]) ? "" : dr[SQL_COL_G_L_DEBIT_ACCOUNT]);
				m_GLDebitSubAccount = (string)(Information.IsDBNull(dr[SQL_COL_G_L_DEBIT_SUB_ACCOUNT]) ? "" : dr[SQL_COL_G_L_DEBIT_SUB_ACCOUNT]);
				m_GLCashAccount = (string)(Information.IsDBNull(dr[SQL_COL_G_L_CASH_ACCOUNT]) ? "" : dr[SQL_COL_G_L_CASH_ACCOUNT]);
				m_GlCashSubAccount = (string)(Information.IsDBNull(dr[SQL_COL_GL_CASH_SUB_ACCOUNT]) ? "" : dr[SQL_COL_GL_CASH_SUB_ACCOUNT]);
				m_LegalCode = (bool)dr[SQL_COL_LEGAL_CODE];
			} catch (SqlException ex) {
			} catch (System.Exception exg) {
			}
			return this;

		}

		public static Codes load(string m_ChargeCode)
		{

			Codes obj = new Codes();
			string[] opt = { m_ChargeCode };
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			DataRow dr = ds.Tables[0].Rows[0];

			return obj.fill(dr);

		}

		public static Codes[] load()
		{

			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			Codes obj = new Codes();

			ds = obj.loadAll();
			obj = null;
			DataRow dr = null;
			foreach (DataRow dr_loopVariable in ds.Tables[0].Rows) {
				dr = dr_loopVariable;
				obj = new Codes();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (Codes[])list.ToArray(typeof(Codes));
			} catch (SqlException ex) {
				return null;
			}

		}

				/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
		{

			if (!string.IsNullOrEmpty(m_ChargeCode.Trim())) {
				string[] values = {
					m_ChargeCode,
					m_ChargeDescription,
					m_GLCreditAccount,
					m_GLCreditSubAccount,
					m_GLDebitAccount,
					m_GLDebitSubAccount,
					m_GLCashAccount,
					m_GlCashSubAccount,
					m_LegalCode.ToString(),
					m_ChargeCode
				};
				if (this.updateRecord(m_ChargeCode, values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {
					m_ChargeCode,
					m_ChargeDescription,
					m_GLCreditAccount,
					m_GLCreditSubAccount,
					m_GLDebitAccount,
					m_GLDebitSubAccount,
					m_GLCashAccount,
					m_GlCashSubAccount,
					m_LegalCode.ToString()
				};
				this.insertRecord(values);
				//if (m_ChargeCode != null && m_ChargeCode != "" ) {
					return true;
				//}
			}
			return false;

		}

		public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
		{

			try {
				CN.Open();
				System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "CREATE TABLE Codes (Charge Code TEXT(1),Charge Description TEXT(32),GL Credit Account TEXT(4),GL Credit Sub-Account TEXT(3),GL Debit Account TEXT(4),GL Debit Sub-Account TEXT(3),GL Cash Account TEXT(4),Gl Cash Sub-Account TEXT(3),Legal Code YESNO)";
				cmd.Connection = CN;
				cmd.ExecuteNonQuery();
			} catch (System.Data.OleDb.OleDbException ex) {
				return false;
			} finally {
				try {
					CN.Close();
				} catch {
				}
			}

			return true;

		}

		private void OnPropertyChanged(string info)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
			m_isChanged = true;
		}

// Property Routines
		public bool isChanged {
			get { return m_isChanged; }
			set { m_isChanged = value; }
		}
		public string ChargeCode {
			get { return m_ChargeCode; }
			set {
				m_ChargeCode = value;
				OnPropertyChanged("ChargeCode");
			}
		}

		public string ChargeDescription {
			get { return m_ChargeDescription; }
			set {
				m_ChargeDescription = value;
				OnPropertyChanged("ChargeDescription");
			}
		}

		public string GLCreditAccount {
			get { return m_GLCreditAccount; }
			set {
				m_GLCreditAccount = value;
				OnPropertyChanged("GLCreditAccount");
			}
		}

		public string GLCreditSubAccount {
			get { return m_GLCreditSubAccount; }
			set {
				m_GLCreditSubAccount = value;
				OnPropertyChanged("GLCreditSubAccount");
			}
		}

		public string GLDebitAccount {
			get { return m_GLDebitAccount; }
			set {
				m_GLDebitAccount = value;
				OnPropertyChanged("GLDebitAccount");
			}
		}

		public string GLDebitSubAccount {
			get { return m_GLDebitSubAccount; }
			set {
				m_GLDebitSubAccount = value;
				OnPropertyChanged("GLDebitSubAccount");
			}
		}

		public string GLCashAccount {
			get { return m_GLCashAccount; }
			set {
				m_GLCashAccount = value;
				OnPropertyChanged("GLCashAccount");
			}
		}

		public string GlCashSubAccount {
			get { return m_GlCashSubAccount; }
			set {
				m_GlCashSubAccount = value;
				OnPropertyChanged("GlCashSubAccount");
			}
		}

		public bool LegalCode {
			get { return m_LegalCode; }
			set {
				m_LegalCode = value;
				OnPropertyChanged("LegalCode");
			}
		}

		public string DisplayDescription {
			get { return m_ChargeCode + ": " + m_ChargeDescription; }
		}

	}
}
