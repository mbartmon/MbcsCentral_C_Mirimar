using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MbcsCentral
{
    public class CallClassParms : MarshalByRefObject
    {
        const string EOM = "WinPM.frmEOM";
        const string BUILDING_MASTER = "WinPM.frmReportSelections";

        Type type;

        string m_appName;
        string m_parm;

        public CallClassParms() {}

        public CallClassParms(string name, object[] parms)
        {
            BuildClass(name, parms);
        }

        public CallClassParms(string appName, string parm)
        {
            object[] parms = { parm };
            BuildClass(appName, parms);
        }

        private void BuildClass(string name, object[] parms)
        {
            try
            {
                Module[] modules = Assembly.GetExecutingAssembly().GetModules();
                if (modules == null || modules.Length == 0)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CallClassParms error <name=" + name + "> no module found.", false);
                }
                Module myModule = modules[0];
                type = myModule.GetType(name, true);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "CallClassParms error <name=" + name + ">: " + ex.Message, false);

            }
        }

       public void invokeClass()
        {
            System.Windows.Window frm = null;

            switch (m_appName)        
                {
                case EOM:
                        frm = (WinPM.frmEOM) new WinPM.frmEOM(m_parm);
                        frm.Show();
                        break;
                case BUILDING_MASTER:
                        frm = (WinPM.frmReportSelections) new WinPM.frmReportSelections(m_parm);
                        frm.Show();
                        break;
                }
        }

        public object returnObject(object[] parms)
        {
            object obj = null;
            try
            {
                obj = Activator.CreateInstance(type, parms);
            }
            catch (ArgumentNullException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (ArgumentException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (NotSupportedException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (TargetInvocationException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (MethodAccessException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (MemberAccessException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (InvalidComObjectException ex)
            {

                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (COMException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (TypeLoadException ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);
            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, parms[1].ToString() + " Form CreateInstance error:" + ex.Message, false);

            }

            return obj;
        }

    }
}
