using System.Drawing;

namespace SqlDbCloner.Core.Data
{
    public class DataObject
    {
        public Bitmap Status { get; set; }
        public string Table { get; set; }
        public string SqlCommand { get; set; }
        public string Error { get; set; }

        public DataObject()
        {
            //Status = Properties.Resources.unknown;
        }
    }
}
