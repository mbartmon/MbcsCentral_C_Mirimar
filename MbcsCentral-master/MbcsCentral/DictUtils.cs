using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace MbcsCentral
{
    /// <summary>
    /// Static class for maintaining a configuration file
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
            s = itm.Split(',');
            try
            {
                switch (s.Length)
                {
                    case 0:
                        // do nothing
                        break;
                    case 1:
                        d.Add( s[0], "" );
                        break;
                    default:
                        d.Add( s[0], s[1] );
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Add or replace a key/value pair
        /// </summary>
        /// <param name="d">The dictionary object</param>
        /// <param name="key">Key to add or replace</param>
        /// <param name="value">The new value</param>
        /// <remarks></remarks>
        public static void setItem(Dictionary< string, string> d, string key, object itm)
        {   
            string s;
            if (itm.GetType() == typeof(CheckBox))
            {
                System.Windows.Controls.CheckBox c   = (CheckBox)itm;
                s = (c.IsChecked.Value ? "true" : "false");
            }
            else
            {
                if (itm.GetType() == typeof(bool))
                    s = ((bool)itm ? "true": "false");
                else
                    s = (string)itm;
           }

            if (d.ContainsKey(key))
                d[key] = s;
            else
                d.Add(key, s);
        }

        /// <summary>
        /// Retrieve an item by key
        /// </summary>
        /// <param name="d">The dictionary object</param>
        /// <param name="key">The key to retrieve</param>
        /// <param name="itm">The value that is retrieved</param>
        /// <remarks></remarks>
        public static object getItem(Dictionary<string, string> d, string key,  object itm)
        {
            object obj = itm;
            string st = null;
            d.TryGetValue(key, out st);
            if ( st != null)
            {
                if (!st.Trim().Equals(""))
                {
                    if (obj.GetType() == typeof(System.Windows.Controls.CheckBox))
                    {
                        System.Windows.Controls.CheckBox c  = (System.Windows.Controls.CheckBox)itm;
                        c.IsChecked = (st.ToLower().Equals("true") ? true : false);
                    }
                    else
                        {
                            if (obj.GetType() == typeof(bool))
                                obj = (st.ToLower().Equals("true") ? true : false);
                            else
                            {
                                double num;
                                if ( !(obj.GetType() == typeof(string)) && double.TryParse((string)obj, out num) )
                                    obj = num;
                                else
                                    obj = st;
                            }
                        }
                }
            }
            return obj;
        }

        /// <summary>
        /// Returns the entire configuration file
        /// </summary>
        /// <param name="d">The dictionary object that will contain the key/value pairs</param>
        /// <remarks>To avoid duplicate values you should pass an empty dictionary object</remarks>
        public static void getConfigs(ref Dictionary<string, string> d , string configFile )
        {

            // Dim fldr As String = String.Format("{0}\{1}\{2}\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config")
            StreamReader sr;
            try
            {
                sr = new StreamReader(configFile);
            } catch (FileNotFoundException ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to find configuration file: ");
                return;
            } catch (IOException ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error opening configuration file: ");
                return;
            }
            string[] s = null;
            if (sr != null)
            {
               string st = null;
               while (!sr.EndOfStream)
               {
                   st = sr.ReadLine();
                   DictUtils.addItem( d, st );
               }
            }
            sr.Close();

        }

        /// <summary>
        /// Writes dictionary to configuration file
        /// </summary>
        /// <param name="d">The dictionary to write</param>
        /// <remarks></remarks>
        public static void setConfigs( Dictionary<string, string> d, string configFile)
        {
            // Dim fldr As String = String.Format("{0}\{1}\{2}\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "WintrmListener.config")
            try {
                System.IO.File.Delete(configFile);
            } catch (IOException ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Warning, "Error deleting <" + configFile + ">: " + ex.Message);
            } catch (System.Exception ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.Warning, "Error deleting <" + configFile + ">: " + ex.Message);
            }
            StreamWriter sw = null;
            try {
                sw = new StreamWriter(configFile, false);
            } catch (FileNotFoundException ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to find configuration file: ");
            } catch (IOException ex) {
                MbcsCentral.GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Error opening configuration file: ");
            }
            foreach (KeyValuePair<string, string> ent in d)
                sw.WriteLine(ent.Key + "," + ent.Value);
            sw.Close();

        }
    }
}
