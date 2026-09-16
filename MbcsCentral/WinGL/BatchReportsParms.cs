using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MbcsCentral.WinGL
{
    public class BatchReportsParms
    {
        // Parameters
        public string DefaultPrinter { get; set; }
        public bool CondensePrint { get; set; }


        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static BatchReportsParms DeSerialize(string inputString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BatchReportsParms));
            StringReader rdr = new StringReader(inputString);
            BatchReportsParms parms = (BatchReportsParms)serializer.Deserialize(rdr);
            return parms;
        }

    }
}
