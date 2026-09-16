using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace $rootnamespace$
{
    class $itemname$
    {
        // Parameters


        public string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();

        }

        public static $itemname$ DeSerialize(string inputString)
        {
    XmlSerializer serializer = new XmlSerializer(typeof($itemname$));
            StringReader rdr = new StringReader(inputString);
            $itemname$ parms = ($itemname$)serializer.Deserialize(rdr);
            return parms;
        }

    }
}
