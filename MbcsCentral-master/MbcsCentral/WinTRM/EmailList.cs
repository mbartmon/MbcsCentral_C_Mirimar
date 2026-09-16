using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace WinTRM
{

	[Serializable()]
	public class EmailList
	{

		private string m_emailAddress;
		private string m_firstName;
		private string m_lastName;

		private Dictionary<string, string> m_values;
		public static string[] ValueList = {
			"ArrearsAmount",
			"FirstName",
			"LastName",
			"Apartment",
			"Address"

		};

		public EmailList()
		{
		}


		public EmailList(string emailAddress, string firstName, string lastName)
		{
			m_emailAddress = emailAddress;
			m_firstName = firstName;
			m_lastName = lastName;

		}


		public EmailList(string emailAddress, string firstName, string lastName, Dictionary<string, string> values)
		{
			m_emailAddress = emailAddress;
			m_firstName = firstName;
			m_lastName = lastName;
			m_values = values;

		}


		public void Add(string name, string value)
		{
			try {
				m_values.Add(name, value);
			} catch (ArgumentException ex) {
				m_values.Item(name) = value;
			} catch (ArgumentException ex) {
			}

		}

		public string Item(string name)
		{

			try {
				return m_values.Item(name);
			} catch {
				return null;
			}

		}

		public string EmailAddress {
			get { return m_emailAddress; }
			set { m_emailAddress = value; }
		}

		public string FirstName {
			get { return m_firstName; }
			set { m_firstName = value; }
		}

		public string LastName {
			get { return m_lastName; }
			set { m_lastName = value; }
		}

		public Dictionary<string, string> Values {
			get { return m_values; }
			set { m_values = value; }
		}

		public static void Serialize(EmailList[] emails, string fileName)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			try {
				IO.File.Delete(fileName);
			} catch {
			}

			try {
				using (IO.FileStream fs = IO.File.OpenWrite(fileName)) {
					foreach (EmailList e in emails) {
						using (IO.MemoryStream mem = new IO.MemoryStream()) {
							ser.Serialize(mem, e);
							byte[] b = mem.ToArray;
							fs.Write(BitConverter.GetBytes(b.Length), 0, 4);
							fs.Write(b, 0, b.Length);
						}
					}
				}
			} catch (System.IO.DirectoryNotFoundException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "List Directory not found <" + fileName + ">: " + ex.Message);
			} catch (System.IO.IOException ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "List Directory not found <" + fileName + ">: " + ex.Message);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log(LogClass.logType.ErrorCondition, "List Directory not found <" + fileName + ">: " + ex.Message);
			}

		}

		public static EmailList[] DeSerialize(string fileName)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			ArrayList list = new ArrayList();
			using (IO.FileStream fs = IO.File.OpenRead(fileName)) {
				byte[] bDateLen = new byte[4];
				do {
					int n = fs.Read(bDateLen, 0, 4);
					if (n < bDateLen.Length)
						break; // TODO: might not be correct. Was : Exit Do

					int dataLen = BitConverter.ToInt32(bDateLen, 0);
					byte[] b = Convert.ToByte(Array.CreateInstance(typeof(byte), dataLen));
					fs.Read(b, 0, b.Length);
					using (IO.MemoryStream mem = new IO.MemoryStream(b)) {
						EmailList tmp = (EmailList)ser.Deserialize(mem);
						list.Add(tmp);
					}
				} while (true);
			}
			try {
				return (EmailList[])list.ToArray(typeof(EmailList));
			} catch {
				return null;
			}
		}

		private class serializeObject
		{
			public bool serialize()
			{

				FileStream fl = null;
				string filename = "SelectionParms.ser";
				if ((filename == null)) {
					return false;
				}
				if (string.IsNullOrEmpty(Strings.Trim(filename))) {
					return false;
				}
				fl = File.OpenWrite(GlobalShared.appPath + filename);
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fl, this);
				fl.Close();
				return true;

			}

			public static EmailList deSerialize()
			{

				string filename = "SelectionParms.ser";
				FileStream fl = null;
				try {
					fl = File.OpenRead(GlobalShared.appPath + filename);
					BinaryFormatter bf = new BinaryFormatter();
					object obj = bf.Deserialize(fl);
					fl.Close();
					if ((obj != null)) {
						return (EmailList)obj;
					} else {
						return null;
					}
				} catch {
					return null;
				}

			}
		}
	}
}
