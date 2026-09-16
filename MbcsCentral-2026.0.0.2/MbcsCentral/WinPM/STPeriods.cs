using System.Data;
using System.ComponentModel;
using System.Data.OleDb;


public class STPeriods : clsDB, INotifyPropertyChanged
{
	
	private PropertyChangedEventHandler PropertyChangedEvent;
	public event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Combine(PropertyChangedEvent, value);
		}
		remove
		{
			PropertyChangedEvent = (PropertyChangedEventHandler) System.Delegate.Remove(PropertyChangedEvent, value);
		}
	}
	
	
	static string TABLE = "STPeriods";
	
	public const string SQL_COL_CODE = "Code";
	public const string SQL_COL_DESCRIPTION = "Description";
	
	static string SQL_LOAD = " SELECT " + "[" + SQL_COL_CODE + "]," + "[" + SQL_COL_DESCRIPTION + "]" + " FROM " + "STPeriods" + " WHERE " + SQL_COL_CODE + " = \'?\';";
	
	static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_CODE + "]," + "[" + SQL_COL_DESCRIPTION + "]" + " FROM " + "STPeriods" + ";";
	
	static string SQL_INSERT = " INSERT INTO " + "STPeriods" + " (" + "[" + SQL_COL_CODE + "]" + "[" + SQL_COL_DESCRIPTION + "]" + ") VALUES (\'?\'\'?\');";
	
	static string SQL_UPDATE = " UPDATE " + "STPeriods" + " SET " + "[" + SQL_COL_CODE + "]=\'?\'" + "[" + SQL_COL_DESCRIPTION + "]=\'?\'" + " WHERE " + SQL_COL_CODE + " = \'?\';";
	
	protected string m_Code;
	protected string m_Description;
	protected bool m_isChanged;
	
	public STPeriods() : base(TABLE, SQL_COL_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	{
	}
	
	public STPeriods(string Code, string Description) : base(TABLE, SQL_COL_CODE, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, CN)
	{
		m_Code = Code;
		m_Description = Description;
	}
	
	protected STPeriods fill(DataRow dr)
	{
		
		try
		{
			m_Code = (string) ((Information.IsDBNull(dr[SQL_COL_CODE])) ? "" : (dr[SQL_COL_CODE]));
			m_Description = (string) ((Information.IsDBNull(dr[SQL_COL_DESCRIPTION])) ? "" : (dr[SQL_COL_DESCRIPTION]));
		}
		catch (OleDbException)
		{
		}
		catch (System.Exception)
		{
		}
		return this;
		
	}
	
	public static STPeriods load(string m_Code)
	{
		
		STPeriods obj = new STPeriods();
		string[] opt = new string[] {m_Code};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		DataRow dr = ds.Tables[0].Rows[0];
		
		return obj.fill(dr);
		
	}
	
	public static STPeriods[] load()
	{
		
		ArrayList list = new ArrayList();
		
		DataSet ds = new DataSet();
		STPeriods obj = new STPeriods();
		
		ds = obj.loadAll();
		obj = null;
		
		foreach (DataRow dr in ds.Tables[0].Rows)
		{
			obj = new STPeriods();
			obj.fill(dr);
			list.Add(obj);
			obj = null;
		}
		try
		{
			return ((STPeriods[]) (list.ToArray(typeof(STPeriods))));
		}
		catch (OleDbException)
		{
			return null;
		}
		
	}
	
			/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()
	{
		
		if (m_Code > 0)
		{
			string[] values = new string[] {m_Code, m_Description, m_Code};
			if (this.updateRecord(m_Code, values))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			string[] values = new string[] {m_Code, m_Description};
			m_Code = (string) (this.insertRecord(values));
			if (m_Code >= 0)
			{
				return true;
			}
		}
		return false;
		
	}
	
	
	public static bool CreateTable(System.Data.OleDb.OleDbConnection CN)
	{
		
		try
		{
			CN.Open();
			OleDb.OleDbCommand cmd = new OleDb.OleDbCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "CREATE TABLE STPeriods (Code TEXT(1),Description TEXT(50))";
			cmd.Connection = CN;
			cmd.ExecuteNonQuery();
		}
		catch (OleDb.OleDbException)
		{
			return false;
		}
		finally
		{
			try
			{
				CN.Close();
			}
			catch
			{
			}
		}
		
		return true;
		
	}
	
	private void OnPropertyChanged(string info)
	{
		if (PropertyChangedEvent != null)
			PropertyChangedEvent(this, new PropertyChangedEventArgs(info));
		m_isChanged = true;
	}
	
	// Property Routines
	public bool isChanged
	{
		get
		{
			return m_isChanged;
		}
		set
		{
			m_isChanged = System.Convert.ToBoolean(value);
		}
	}
	public string Code
	{
		get
		{
			return m_Code;
		}
		set
		{
			m_Code = value;
			OnPropertyChanged("Code");
		}
	}
	
	public string Description
	{
		get
		{
			return m_Description;
		}
		set
		{
			m_Description = value;
			OnPropertyChanged("Description");
		}
	}
	
}