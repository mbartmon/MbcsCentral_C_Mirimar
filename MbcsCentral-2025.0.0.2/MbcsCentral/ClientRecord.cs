using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral
{
    class ClientRecord
    {
        public ClientRecord()
        { }

        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public string DatabaseSource { get; set; }
        public string InitialCatalog { get; set; }
        public string CreateDate { get; set; }
    }
}
