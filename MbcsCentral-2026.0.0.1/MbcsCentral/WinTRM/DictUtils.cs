using System.IO;
namespace WinTRM
{
	/// <summary>
	/// Static/Shared class for maintaining a configuration file
	/// </summary>
	/// <remarks></remarks>
	public class DictUtils
	{
		/// <summary>
		/// Add a key/value pair
		/// </summary>
		/// <param name="d">The dictionary object</param>
		/// <param name="itm">a comma separted string containing [key],[value]</param>
		/// <remarks></remarks>

		public static void addItem(Dictionary<string, string> d, string itm)
		{
			string[] s = null;
			s = itm.Split(",");
			switch (s.Length) {
				case 0:
					break;
				// do nothing
				case 1:
					d.Add(s(0), "");
					break;
				default:
					d.Add(s(0), s(1));
					break;
			}

		}
		/// <summary>
		/// Add or replace a key/value pair
		/// </summary>
		/// <param name="d">The dictionary object</param>
		/// <param name="key">Key to add or replace</param>
		/// <param name="value">The new value</param>
		/// <remarks></remarks>

		public static void setItem(Dictionary<string, string> d, string key, object value)
		{
			string s = null;
			if ((value) is System.Windows.Controls.CheckBox) {
				System.Windows.Controls.CheckBox c = (System.Windows.Controls.CheckBox)value;
				s = (c.IsChecked ? "True" : "False");
			} else if (value is bool) {
				s = (value ? "True" : "False");
			} else {
				s = value;
			}
			if (d.ContainsKey(key)) {
				d.Item(key) = s;
			} else {
				d.Add(key, s);
			}

		}

		/// <summary>
		/// Retrieve an item by key
		/// </summary>
		/// <param name="d">The dictionary object</param>
		/// <param name="key">The key to retrieve</param>
		/// <param name="itm">The value that is retrieved</param>
		/// <remarks></remarks>

		public static void getItem(Dictionary<string, string> d, string key, ref object itm)
		{
			string st = null;
			d.TryGetValue(key, st);
			if ((st != null)) {
				if (!string.IsNullOrEmpty(st.Trim)) {
					if (itm is System.Windows.Controls.CheckBox) {
						System.Windows.Controls.CheckBox c = (System.Windows.Controls.CheckBox)itm;
						c.IsChecked = (st.ToLower.Equals("true") ? true : false);
					} else if (itm is bool) {
						itm = (st.ToLower.Equals("true") ? true : false);
					} else if (!itm is string & Information.IsNumeric(itm)) {
						itm = Conversion.Val(st);
					} else {
						itm = st;
					}
				}
			}

		}

		/// <summary>
		/// Returns the entire configuration file
		/// </summary>
		/// <param name="d">The dictionary object that will contain the key/value pairs</param>
		/// <remarks>To avoid duplicate values you should pass an empty dictionary object</remarks>

		public static void getConfigs(ref Dictionary<string, string> d, string configFile)
		{
			//Dim fldr As String = String.Format("{0}\{1}\{2}\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config")
			StreamReader sr = default(StreamReader);
			try {
				sr = new StreamReader(configFile);
			} catch (FileNotFoundException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Unable to find configuration file: ");
				return;
			} catch (IOException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error opening configuration file: ");
				return;
			}
			string[] s = null;
			if ((sr != null)) {
				while (!sr.EndOfStream) {
					DictUtils.addItem(d, sr.ReadLine());
				}
			}
			sr.Close();

		}

		/// <summary>
		/// Writes dictionary to configuration file
		/// </summary>
		/// <param name="d">The dictionary to write</param>
		/// <remarks></remarks>

		public static void setConfigs(ref Dictionary<string, string> d, string configFile)
		{
			//Dim fldr As String = String.Format("{0}\{1}\{2}\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config")
			try {
				WinTRM.My.MyProject.Computer.FileSystem.DeleteFile(configFile);
			} catch (IOException ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, "Error deleting <" + configFile + ">: " + ex.Message);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.Warning, "Error deleting <" + configFile + ">: " + ex.Message);
			}
			StreamWriter sw = default(StreamWriter);
			try {
				sw = WinTRM.My.MyProject.Computer.FileSystem.OpenTextFileWriter(configFile, false);
			} catch (FileNotFoundException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Unable to find configuration file: ");
			} catch (IOException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "Error opening configuration file: ");
			}
			foreach (KeyValuePair<string, string> ent in d) {
				sw.WriteLine(ent.Key + "," + ent.Value);
			}
			sw.Close();

		}

	}
}
