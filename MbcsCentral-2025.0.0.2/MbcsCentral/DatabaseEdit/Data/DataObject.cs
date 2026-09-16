using System.Drawing;

namespace MbcsCentral.Data
{
    public class DataObject
    {
        public bool Status { get; set; }
        public string Table { get; set; }
        public string SqlCommand { get; set; }
        public string Error { get; set; }

        public DataObject()
        {
            //Status = Properties.Resources.unknown;
        }
    }
}
