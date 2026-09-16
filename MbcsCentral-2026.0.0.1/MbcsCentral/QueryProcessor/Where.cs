using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.QueryProcessor
{
    public class Where
    {

        private Dictionary<string, string> TablePairs = new Dictionary<string, string>();

        private string[] pairs =  { "pm_Tenants/pm_Building|[Building Number]" , "pm_Tenants/pm_Charges|[Sequence Number]", "pm_Charges/pm_Tenants|[Sequence Number]",
            "pm_Charges/pm_ChargeCodes|[Charge Code]", "gl_Transactions/gl_Accounts|[AccountNumber]/[Key]", 
            "pm_Tenants/pm_HistMast|[Sequence Number]", "pm_Histmast/pm_HistChg|[Sequence Number]", "pm_HistMast/pm_HistPay|[Sequence Number]", 
            "pm_HistChg/pm_HistPay|[Sequence Number]", "pm_HistChg/pm_HistMast|[Sequence Number]"}; 

        public Where()
        {
            string[] p;

            foreach (string s in pairs)
            {
                p = s.Split('|');
                if (p.Length == 2)
                    TablePairs.Add(p[0].ToLower(), p[1]);
            }
        }

        public string GetKey(string key)
        {
            if (TablePairs.ContainsKey(key.ToLower()))
                return TablePairs[key.ToLower()];
            return ""; 

        }
    }
}
