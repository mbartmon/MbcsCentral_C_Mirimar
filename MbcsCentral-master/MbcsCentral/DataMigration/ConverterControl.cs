
namespace MbcsCentral.DataMigration
{
    public class ConverterControl
    {
        public static MbcsCentral.WinPM.Control Convert(MbcsCentral.WinPM.Control record)
        {
            MbcsCentral.WinPM.Control outRecord = null;

            return outRecord;
        }

        private void MemoVendorNegate(ref int vendor, ref int batch)
        {
            batch = (vendor < 0 ? batch : 0);
            vendor = (vendor < 0 ? -vendor : vendor);
        }

    }
}
