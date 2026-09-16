using System.Collections;
using System.Collections.Generic;

public class Session
{
	
	private Hashtable @params;
	
	public Session()
	{
		@params = new Hashtable();
	}
	
	public Session(List<sessionItem> @params)
	{
		this.@params = new Hashtable();
		
		foreach (sessionItem parm in @params)
		{
			this.@params.Add(parm.getKey(), parm.getValue());
		}
	}
	
	public object getParam(string key)
	{
		if (@params.ContainsKey(key))
		{
			return @params[key];
		}
		else
		{
			return null;
		}
	}
	
	public void setParam(string key, object value)
	{
		if (key != null&& value != null)
		{
			if (this.@params.ContainsKey(key))
			{
				this.@params[key] = value;
			}
			else
			{
				this.@params.Add(key, value);
			}
		}
	}
}

public class sessionItem
{
	private string key;
	private object value;
	
	public sessionItem(string key, object value)
	{
		this.key = key;
		this.value = value;
	}
	
	public string getKey()
	{
		return this.key;
	}
	public object getValue()
	{
		return value;
	}
}