public class MenuEntry
{
	
	private int _menuType;
	private string _appPath;
	private string _appName;
	private string _dataPath;
	private string _dataName;
	private string _target;
	private string _application;
    private string _parameter;
    private string _class;
	
	public static string[] MenuTypes = new string[] {"Internal", "Form", "Application", "Header", "Method", "Separator"};
	public enum MenuTypeEnum
	{
		Internal,
		Form,
		Application,
		Header,
		Method,
        Separator
	}
	
	public MenuEntry()
	{
		
	}
	
	public MenuEntry(int menuType)
	{
		_menuType = menuType;
	}
	
	public MenuEntry(int menuType, string appPath, string appName, string dataPath, string dataName, string target, string application, string parameter = "", string mclass = "")
	{
		_menuType = menuType;
		_appPath = appPath;
		_appName = appName;
		_dataPath = dataPath;
		_dataName = dataName;
		_target = target;
		_application = application;
        _parameter = parameter;
	    _class = mclass;
	}
	
	public MenuEntry(string appPath, string appName, string dataPath, string dataName)
	{
		_menuType = System.Convert.ToInt32(MenuTypeEnum.Application);
		_appPath = appPath;
		_appName = appName;
		_dataPath = dataPath;
		_dataName = dataName;
	}
	
	public int MenuType
	{
		get
		{
			return _menuType;
		}
		set
		{
			_menuType = System.Convert.ToInt32(value);
		}
	}
	
	public string AppPath
	{
		get
		{
			return _appPath;
		}
		set
		{
			_appPath = value.ToString();
		}
	}
	
	public string AppName
	{
		get
		{
			return _appName;
		}
		set
		{
			_appName = value.ToString();
		}
	}
	
	public string DataPath
	{
		get
		{
			return _dataPath;
		}
		set
		{
			_dataPath = value.ToString();
		}
	}
	
	public string DataName
	{
		get
		{
			return _dataName;
		}
		set
		{
			_dataName = value.ToString();
		}
	}
	
	public string Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value.ToString();
		}
	}
	
	public string Application
	{
		get
		{
			return _application;
		}
		set
		{
			_application = value.ToString();
		}
	}
    public string Parameter
    {
        get
        {
            return _parameter; 
        }
        set
        {
            _parameter = value; 
        }
    }

    public string MenuClass
    {
        get { return _class; }
        set { _class = value; }
    }
}