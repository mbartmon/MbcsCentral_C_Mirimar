using System.ComponentModel;
using System.Data;
using System;

namespace MbcsCentral.WinTrm
{
	public static class BindingClasses
	{

		public class ScheduleByAccount : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected int _ID;
			protected string _Account;
			protected string _Name;
			protected string _HowOften;
			public ScheduleByAccount()
			{
			}
			public ScheduleByAccount(int id, string acct, string name, string howoften)
			{
				_ID = id;
				_Account = acct;
				_Name = name;
				_HowOften = howoften;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}
			public int ID {
				get { return _ID; }
				set { _ID = value; }
			}
			public string Account {
				get { return _Account; }
				set { _Name = value; }
			}
			public string Name {
				get { return _Name; }
				set { _Name = value; }
			}
			public string HowOften {
				get { return _HowOften; }
				set { _HowOften = value; }
			}
		}

		public class bindMessageSchedules : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected int _id;
			protected string _description;
			protected string _account;
			protected string _status;
			protected string _content;
			protected string _subject;
			public bindMessageSchedules()
			{
			}
			public bindMessageSchedules(int id, string description, string account, string status, string content, string subject)
			{
				_id = id;
				_description = description;
				_account = account;
				_status = status;
				_content = content;
				_subject = subject;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}
			public int Id {
				get { return _id; }
				set { _id = value; }
			}
			public string Description {
				get { return _description; }
				set { _description = value; }
			}
			public string Account {
				get { return _account; }
				set { _account = value; }
			}
			public string Status {
				get { return _status; }
				set { _status = value; }
			}
			public string Content {
				get { return _content; }
				set { _content = value; }
			}
			public string Subject {
				get { return _subject; }
				set { _subject = value; }
			}
		}

		public class bindAccountList : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected int _id;
			protected string _name;
			public bindAccountList()
			{
			}
			public bindAccountList(int id, string name)
			{
				_id = id;
				_name = name;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}
			public int id {
				get { return _id; }
				set { _id = value; }
			}
			public string name {
				get { return _name; }
				set { _name = value; }
			}
		}
		public class bindScheduleList : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected int _id;
			protected string _name;
			public bindScheduleList()
			{
			}
			public bindScheduleList(int id, string name)
			{
				_id = id;
				_name = name;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}
			public int id {
				get { return _id; }
				set { _id = value; }
			}
			public string name {
				get { return _name; }
				set { _name = value; }
			}
		}
		public class bindContentList : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected int _id;
			protected string _name;
			public bindContentList()
			{
			}
			public bindContentList(int id, string name)
			{
				_id = id;
				_name = name;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}
			public int id {
				get { return _id; }
				set { _id = value; }
			}
			public string name {
				get { return _name; }
				set { _name = value; }
			}
		}
		public class bindAccountEmails : INotifyPropertyChanged, IComparable
		{
			public event PropertyChangedEventHandler PropertyChanged;
			protected long _id;
			protected string _firstName;
			protected string _lastName;
			protected string _addrType;
			protected string _email;
			protected string _sms;
			protected string _address1;
			protected string _address2;
			protected string _address3;
			protected string _city;
			protected string _state;
			protected string _zip;
			protected string _phone;
			protected System.DateTime _dob;

			protected string _name;
			public bindAccountEmails()
			{
			}
			public bindAccountEmails(long id, string firstName, string lastName, string addrType, string email)
			{
				_id = id;
				_firstName = firstName;
				_lastName = lastName;
				_addrType = addrType;
				_email = email;
			}
			private void OnPropertyChanged(string info)
			{
				if (PropertyChanged != null) {
					PropertyChanged(this, new PropertyChangedEventArgs(info));
				}
			}

            public int CompareTo(object obj)
            {
                if (obj is bindAccountEmails)
                {
                    bindAccountEmails other = obj as bindAccountEmails;
                    return (this._email).CompareTo(other.Email);
                }
                return 0;
            }

            public long Id {
				get { return _id; }
				set { _id = value; }
			}
			public string firstName {
				get { return _firstName; }
				set { _firstName = value; }
			}
			public string lastName {
				get { return _lastName; }
				set { _lastName = value; }
			}
			public string AddressType {
				get { return _addrType; }
				set { _addrType = value; }
			}
			public string Email {
				get { return _email; }
				set { _email = value; }
			}
			public string Address1 {
				get { return _address1; }
				set { _address1 = value; }
			}
			public string Address2 {
				get { return _address2; }
				set { _address2 = value; }
			}
			public string Address3 {
				get { return _address3; }
				set { _address3 = value; }
			}
			public string City {
				get { return _city; }
				set { _city = value; }
			}
			public string State {
				get { return _state; }
				set { _state = value; }
			}
			public string Zip {
				get { return _zip; }
				set { _zip = value; }
			}
			public string Phone {
				get { return _phone; }
				set { _phone = value; }
			}
			public System.DateTime DOB {
				get { return _dob; }
				set { _dob = value; }
			}
			public string Name {
				get { return _firstName + " " + lastName; }
			}
		}

		public class bindStatus
		{
			private int _id;
			private string _name;
			public bindStatus()
			{
			}
			public bindStatus(int id, string name)
			{
				_id = id;
				_name = name;
			}
			public string name {
				get { return _name; }
				set { _name = value; }
			}
			public int id {
				get { return _id; }
				set { _id = value; }
			}
		}

	}
}
