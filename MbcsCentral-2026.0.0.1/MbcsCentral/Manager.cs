using System.Data.SqlClient;
using System.Windows.Forms;

namespace MbcsCentral
{
    public sealed class Manager
    {

        public static Session userSession;
        //static LogClass Log = LogClass.instance;

        public static Users setUser(string login, string pswd)
        {
            GlobalShared.user = null;
            //GlobalShared.CN.ConnectionString = MbcsCentral.GlobalShared.SqlConnectionString;
            Accounts acct = Accounts.load(login);
            if (acct == null)
            {
                MessageBoxCustom.Show("Current user not found in this database.  You should restart the application.");
                return null;
            }
            //GlobalShared.CN.ConnectionString = MbcsCentral.GlobalShared.SqlConnectionString; 
            Accounts[] accts = Accounts.load();
            foreach (Accounts a in accts)
            {
                GlobalShared.Log.Log((int)LogClass.logType.Info, "Account: " + a.login, false);
            }
            if (acct != null)
            {
                // User is in AD, retrieve password from database
                if (pswd.Equals("*"))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "User <" + acct.login + "> auto login", false);
                    pswd = acct.password.Trim();
                }
                if (!acct.password.Trim().Equals(pswd.Trim()))
                {
                    MessageBox.Show("Password is invalid");
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "UserId=" + login + ", Password=" + pswd + "\r\nFOUND: UserId=" + acct.login + ", Password=" + acct.password, false);
                    return null;
                    //return MbcsCentral.GlobalShared.user;
                }
                Clients client = Clients.load((long)acct.clientId);
                if (client == null)
                {
                    MessageBox.Show("Client record <" + acct.clientId.ToString() + "> not found for user <" + acct.accountId.ToString() + "> <" + acct.login + ">.");
                    return null;
                }
                GlobalShared.WinGlEnabled = client.WinGlEnabled;
                MbcsCentral.GlobalShared.user = new Users(acct, client);
                Main.mSession.setParam("User", MbcsCentral.GlobalShared.user);
                user_permissions[] userPerms = null;
                userPerms = getAppPerms();
                if (userPerms != null)
                {
                    try
                    {
                        MbcsCentral.GlobalShared.user.PermissionsAsClass = userPerms;
                    }
                    catch (System.Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
                    }
                }
                MbcsCentral.GlobalShared.user.setApplicationAssignments = getAppAssigments();
                return MbcsCentral.GlobalShared.user;
            }
            else
            {
                return null;
            }

        }

        public static void RefreshPermissions()
        {
            user_permissions[] userPerms = getAppPerms();
            if (userPerms != null)
                MbcsCentral.GlobalShared.user.PermissionsAsClass = userPerms;
        }

        public static user_permissions[] getAppPerms()
        {

            user_permissions[] perms = null;
            Users user = (Users)Main.mSession.getParam("User");
            if (user != null)
            {
                perms = user_permissions.loadByUser(user.Account.accountId, true);
            }

            return perms;

        }

        public static ApplicationAssignments[] getAppAssigments()
        {

            ApplicationAssignments[] appAssign = null;
            Users user = (Users)Main.mSession.getParam("User");
            appAssign = ApplicationAssignments.loadByClient(user.Client.clientId);
            return appAssign;

        }

    }
}