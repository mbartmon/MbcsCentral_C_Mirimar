using Microsoft.SqlServer.Management.Smo;
using System.ComponentModel;
using System.Drawing;

namespace SqlDbCloner.Core.Schema
{
    public class SqlObject
    {
        public Bitmap Status { get; set; } 
        public string Name { get; set; }
        [Browsable(false)]
        public NamedSmoObject Object { get; set; }
        public string Type { get; set; }        
        public string Error { get; set; }

        public SqlObject()
        {
            //Status = Properties.Resources.unknown;
        }       
        
    }
}
