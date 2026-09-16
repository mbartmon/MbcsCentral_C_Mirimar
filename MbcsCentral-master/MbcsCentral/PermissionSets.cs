using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MbcsCentral
{
    class PermissionSets  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private long id;
        private int idType;
        private user_permissions[] userPerms;
        private bool isChanged;

        public PermissionSets()
        {

        }

        public PermissionSets(long id, int idType, user_permissions[] userPerms)
        {
            this.id = id;
            this.idType = idType;
            this.userPerms = userPerms;
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                isChanged = true;
            }
        }

        public long Id
        {
            get { return id; }
            set 
            { 
                this.id = value; 
                OnPropertyChanged("Id");
            }
        }
        public int IdType
        {
            get { return this.idType;}
            set 
            {
                this.idType = value;
                OnPropertyChanged("IdType");
            }
        }
        public user_permissions[] UserPerms
        {
            get { return this.userPerms;}
            set 
            {
                this.userPerms = value;
                OnPropertyChanged("UserPerms");
            }
        }
        public bool IsChanged
        {
            get {return isChanged;}
            set { isChanged = value;}
        }
    }
}
