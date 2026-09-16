using System.Collections.Generic;

namespace MbcsCentral
{
    public class Users
    {
        LogClass Log = LogClass.instance;

        private Clients _client;
        private Accounts _account;
        private Dictionary<string, bool> _applicationAssignments;
        private Dictionary<string, user_permissions> _userPermissions;
        private user_permissions[] _perms;

        public Users()
        {

            _applicationAssignments = new Dictionary<string, bool>();
            _userPermissions = new Dictionary<string, user_permissions>();

        }

        public Users(Accounts account, Clients client)
        {

            _account = account;
            _client = client;
            _applicationAssignments = new Dictionary<string, bool>();
            _userPermissions = new Dictionary<string, user_permissions>();

        }

        public string getName
        {
            get
            {
                return _account.firstName.Trim() + " " + _account.lastName.Trim();
            }
        }

        public Clients Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        public Accounts Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        public Dictionary<string, user_permissions> PermissionsAsDictionary
        {
            get
            {
                return _userPermissions;
            }
            set
            {
                _userPermissions = value;
            }
        }

        public user_permissions[] PermissionsAsClass
        {
            get
            {
                return _perms;
            }
            set
            {
                try
                {
                    _perms = value;
                    if (_userPermissions == null)
                    {
                        _userPermissions = new Dictionary<string, user_permissions>();
                    }
                    else
                    {
                        _userPermissions.Clear();
                    }
                    permission perm = null;
                    foreach (user_permissions onePerm in _perms)
                    {
                        perm = permission.load(onePerm.Permission_id);
                        if (perm != null)
                        {
                            try 
                            {
                                _userPermissions.Add(perm.permission_name, onePerm);
                            }
                            catch { }   
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, ex.Message);
                }
            }
        }

        public Dictionary<string, bool> ApplicationAssignments
        {
            get
            {
                return _applicationAssignments;
            }
            set
            {
                _applicationAssignments = value;
            }
        }

        public ApplicationAssignments[] setApplicationAssignments
        {
            set
            {
                Applications app = null;
                if (value != null)
                {
                    foreach (ApplicationAssignments oneApp in value)
                    {
                        app = Applications.load(oneApp.applicationId);
                        if (app != null)
                        {
                            _applicationAssignments.Add(app.applicationName, oneApp.active);
                        }
                    }
                }
            }
        }

        public bool hasAppAccess(string app)
        {

            bool result = false;
            if (_applicationAssignments.ContainsKey(app))
            {
                result = System.Convert.ToBoolean(_applicationAssignments[app]);
            }
            return result;

        }

        public bool hasModifyPermission(string app)
        {

            bool result = false;
            try
            {
                int p = app.IndexOf("0");
                if (p > 0)
                    app = app.Substring(0, p);
                string highLevel = app.Substring(0, 2);
                if (Account.accountType == GlobalShared.ADMINUSER)
                    return true;
                if (_userPermissions.ContainsKey(highLevel))
                {
                    user_permissions perm = _userPermissions[highLevel];
                    result = System.Convert.ToBoolean(perm.Modify);
                }
                else if (_userPermissions.ContainsKey(app))
                {
                    user_permissions perm = _userPermissions[app];
                    result = System.Convert.ToBoolean(perm.Modify);
                }
            }
            catch { }
            return result;

        }

        public bool hasAccessPermission(string app)
        {
            bool result = false;
            int p = app.IndexOf("0");
            if (p > 0)
                app = app.Substring(0, p);
            string highLevel = app.Substring(0, 2);
            if (Account.accountType == GlobalShared.ADMINUSER)
                return true;
            if (_userPermissions.ContainsKey(highLevel))
            {
                user_permissions perm = _userPermissions[highLevel];
                result = System.Convert.ToBoolean(perm.Access);
            } else if (_userPermissions.ContainsKey(app))
            {
                user_permissions perm = _userPermissions[app];
                result = System.Convert.ToBoolean(perm.Access);
            }
            return result;
        }

        public bool hasRemovePermission(string app)
        {
            bool result = false;
            int p = app.IndexOf("0");
            if (p > 0)
                app = app.Substring(0, p);
            string highLevel = app.Substring(0, 2);
            if (Account.accountType == GlobalShared.ADMINUSER)
                return true;
            if (_userPermissions.ContainsKey(highLevel))
            {
                user_permissions perm = _userPermissions[highLevel];
                result = System.Convert.ToBoolean(perm.Remove);

            } else if (_userPermissions.ContainsKey(app))
            {
                user_permissions perm = _userPermissions[app];
                result = System.Convert.ToBoolean(perm.Remove);
            }
            return result;
        }

        public bool hasPrintPermission(string app)
        {
            bool result = false;
            int p = app.IndexOf("0");
            if (p > 0)
                app = app.Substring(0, p);
            string highLevel = app.Substring(0, 2);
            if (Account.accountType == GlobalShared.ADMINUSER)
                return true;
            if (_userPermissions.ContainsKey(highLevel))
            {
                user_permissions perm = _userPermissions[highLevel];
                result = System.Convert.ToBoolean(perm.Print);

            }
            else if (_userPermissions.ContainsKey(app))
            {
                user_permissions perm = _userPermissions[app];
                result = System.Convert.ToBoolean(perm.Print);
            }
            return result;
        }
    }
}