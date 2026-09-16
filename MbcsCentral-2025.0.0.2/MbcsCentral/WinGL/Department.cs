using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsCentral.WinGL {
public class Department : clsDB	, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public static string TABLE = "gl_Department";
    public const string SQL_COL_DEPARTMENT_NUMBER = "Department Number";
    public const string SQL_COL_COMPANY_NUMBER = "Company Number";
    public const string SQL_COL_ID = "Id";
    public const string SQL_COL_IN_ACTIVE = "InActive";
    public const string SQL_COL_NAME = "Name";
    public const string SQL_COL_STREET_ADDRESS = "Street Address";
    public const string SQL_COL_CITY = "City";
    public const string SQL_COL_STATE = "State";
    public const string SQL_COL_ZIP = "Zip";

    static string SQL_LOAD = "SELECT " +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_IN_ACTIVE + "]," +
            "[" + SQL_COL_NAME + "]," +
            "[" + SQL_COL_STREET_ADDRESS + "]," +
            "[" + SQL_COL_CITY + "]," +
            "[" + SQL_COL_STATE + "]," +
            "[" + SQL_COL_ZIP + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

    static string SQL_LOAD_ALL = "SELECT " +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_IN_ACTIVE + "]," +
            "[" + SQL_COL_NAME + "]," +
            "[" + SQL_COL_STREET_ADDRESS + "]," +
            "[" + SQL_COL_CITY + "]," +
            "[" + SQL_COL_STATE + "]," +
            "[" + SQL_COL_ZIP + "]" +
            " FROM " + TABLE + ";";

    static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_IN_ACTIVE + "]," +
            "[" + SQL_COL_NAME + "]," +
            "[" + SQL_COL_STREET_ADDRESS + "]," +
            "[" + SQL_COL_CITY + "]," +
            "[" + SQL_COL_STATE + "]," +
            "[" + SQL_COL_ZIP + "]" +
            ") VALUES (?, ?, ?, '?', '?', '?', '?', '?');";

    static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]=?," +
            "[" + SQL_COL_COMPANY_NUMBER + "]=?," +
            "[" + SQL_COL_IN_ACTIVE + "]=?," +
            "[" + SQL_COL_NAME + "]='?'," +
            "[" + SQL_COL_STREET_ADDRESS + "]='?'," +
            "[" + SQL_COL_CITY + "]='?'," +
            "[" + SQL_COL_STATE + "]='?'," +
            "[" + SQL_COL_ZIP + "]='?'" +
            " WHERE " + "[" + SQL_COL_ID + "] = ?;";

    static string SQL_LOAD_BY_COMPANY = "SELECT " +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_IN_ACTIVE + "]," +
            "[" + SQL_COL_NAME + "]," +
            "[" + SQL_COL_STREET_ADDRESS + "]," +
            "[" + SQL_COL_CITY + "]," +
            "[" + SQL_COL_STATE + "]," +
            "[" + SQL_COL_ZIP + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_COMPANY_NUMBER + "] = ?;";

    static string SQL_LOAD_BY_DEPARTMENT = "SELECT " +
            "[" + SQL_COL_DEPARTMENT_NUMBER + "]," +
            "[" + SQL_COL_COMPANY_NUMBER + "]," +
            "[" + SQL_COL_ID + "]," +
            "[" + SQL_COL_IN_ACTIVE + "]," +
            "[" + SQL_COL_NAME + "]," +
            "[" + SQL_COL_STREET_ADDRESS + "]," +
            "[" + SQL_COL_CITY + "]," +
            "[" + SQL_COL_STATE + "]," +
            "[" + SQL_COL_ZIP + "]" +
            " FROM " + TABLE +
            " WHERE " + "[" + SQL_COL_DEPARTMENT_NUMBER + "] = ?;";


	protected short m_DepartmentNumber;
	protected short m_CompanyNumber;
	protected int m_Id;
    protected bool m_InActive;
    protected string m_Name;
	protected string m_StreetAddress;
	protected string m_City;
	protected string m_State;
	protected string m_Zip;
	protected bool m_isChanged;

	public Department() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
	{
	}

    public Department(short DepartmentNumber, short CompanyNumber, int Id, bool InActive, string Name, string StreetAddress, string City, string State, string Zip) :
        base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
    {
        m_DepartmentNumber = DepartmentNumber;
        m_CompanyNumber = CompanyNumber;
        m_Id = Id;
        m_InActive = InActive;
        m_Name = Name;
        m_StreetAddress = StreetAddress;
        m_City = City;
        m_State = State;
        m_Zip = Zip;
    }

    protected Department fill(DataRow dr)
    {
        try
        {
            m_DepartmentNumber = (short)dr[SQL_COL_DEPARTMENT_NUMBER];
            m_CompanyNumber = (short)dr[SQL_COL_COMPANY_NUMBER];
            m_Id = (int)dr[SQL_COL_ID];
            m_InActive = (bool)dr[SQL_COL_IN_ACTIVE];
            m_Name = (string)(Convert.IsDBNull(dr[SQL_COL_NAME]) ? "" : dr[SQL_COL_NAME]);
            m_StreetAddress = (string)(Convert.IsDBNull(dr[SQL_COL_STREET_ADDRESS]) ? "" : dr[SQL_COL_STREET_ADDRESS]);
            m_City = (string)(Convert.IsDBNull(dr[SQL_COL_CITY]) ? "" : dr[SQL_COL_CITY]);
            m_State = (string)(Convert.IsDBNull(dr[SQL_COL_STATE]) ? "" : dr[SQL_COL_STATE]);
            m_Zip = (string)(Convert.IsDBNull(dr[SQL_COL_ZIP]) ? "" : dr[SQL_COL_ZIP]);
        }
        catch (SqlException ex)
        {
        }
        return this;

    }

	public static Department load( int m_Id)
	{
		Department obj  = new Department();
		string[] opt = {m_Id.ToString()};
		DataSet ds = new DataSet();
		ds = obj.load(SQL_LOAD, opt);
		if (ds != null && ds.Tables[0].Rows.Count > 0) {
			DataRow dr = ds.Tables[0].Rows[0];
			return obj.fill(dr);
		} else
			return null;

	}

	public static Department[] load( bool addBlank = false, bool Inactive = false)
	{
		ArrayList list = new ArrayList();

		DataSet ds = new DataSet();
		Department obj = new Department();

		ds = obj.loadAll();
		if (ds == null)
			return null;
		obj = null;
        if (addBlank)
            list.Add(new Department());
		foreach (DataRow dr in ds.Tables[0].Rows) {
			obj = new Department();
			obj.fill(dr);
            if (Inactive || (!Inactive && !obj.InActive))
			    list.Add(obj);
			obj = null;
		}
		try {
			return (Department[])list.ToArray(typeof(Department));
		} catch (SqlException ex) {
			return null;
		}

	}

    public static Department[] loadByCompany(short cmp, bool addBlank = false, bool Inactive = false)
    {
        ArrayList list = new ArrayList();

        DataSet ds = new DataSet();
        Department obj = new Department();

        string[] opt = {cmp.ToString()};

        ds = obj.load(SQL_LOAD_BY_COMPANY, opt);
        if (ds == null)
            return null;
        obj = null;
        if (addBlank)
            list.Add(new Department());
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            obj = new Department();
            obj.fill(dr);
            if (Inactive || (!Inactive && !obj.InActive))
			    list.Add(obj);
            obj = null;
        }
        try
        {
            return (Department[])list.ToArray(typeof(Department));
        }
        catch (SqlException ex)
        {
            return null;
        }

    }

    public static Department loadByDepartment(short dpt)
    {
        DataSet ds = new DataSet();
        Department obj = new Department();

        string[] opt = { dpt.ToString() };

        ds = obj.load(SQL_LOAD_BY_DEPARTMENT, opt);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            return obj.fill(dr);
        } else
        return null;
    }

    		/// <summary>
		/// Save or Update a record
		/// </summary>
		/// <returns>True for success, False for failure. See MbcsCentral log for details</returns>
	public bool save()

    {
        if (m_Id > 0)
        {
            string[] values = { m_DepartmentNumber.ToString(), m_CompanyNumber.ToString(), m_InActive.ToString(), m_Name, m_StreetAddress, m_City, m_State, m_Zip, m_Id.ToString() };
            if (this.updateRecord(m_Id.ToString(), values))
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
            string[] values = { m_DepartmentNumber.ToString(), m_CompanyNumber.ToString(), m_InActive.ToString(), m_Name, m_StreetAddress, m_City, m_State, m_Zip };
            m_Id = this.insertRecord(values);
            if (m_Id >= 0)
            {
                return true;
            }
        }
        return false;

    }

	private void OnPropertyChanged(string info) {
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
			m_isChanged = true;
		}
	}

	// Properties
    /// <summary>
	/// Has record been modified?
	/// </summary>
	public bool IsChanged

    {
        get
        {
            return m_isChanged;
        }
        set { m_isChanged = value; }
    }
	public short DepartmentNumber {
		set{ m_DepartmentNumber = value;
		OnPropertyChanged("DepartmentNumber"); }
		get { return m_DepartmentNumber; }
	}

	public short CompanyNumber {
		set{ m_CompanyNumber = value;
		OnPropertyChanged("CompanyNumber"); }
		get { return m_CompanyNumber; }
	}

	public int Id {
		get { return m_Id; }
	}

    public bool InActive
    {
        set
        {
            m_InActive = value;
            OnPropertyChanged("InActive");
        }
        get { return m_InActive; }
    }

    public string Name {
		set{ m_Name = value;
		OnPropertyChanged("Name"); }
		get { return m_Name; }
	}

	public string StreetAddress {
		set{ m_StreetAddress = value;
		OnPropertyChanged("StreetAddress"); }
		get { return m_StreetAddress; }
	}

	public string City {
		set{ m_City = value;
		OnPropertyChanged("City"); }
		get { return m_City; }
	}

	public string State {
		set{ m_State = value;
		OnPropertyChanged("State"); }
		get { return m_State; }
	}

	public string Zip {
		set{ m_Zip = value;
		OnPropertyChanged("Zip"); }
		get { return m_Zip; }
	}

}
}
