using System;
using System.Data.SqlClient;


namespace MbcsCentralLogCleaner
{
    class GlobalShared
    {
        public static SqlConnection CN;
        public static LogClass Log = null;
        public static DateTime NULL_DATE = new DateTime(1900, 1, 1, 0, 0, 0); // "1/1/1900 12:00:00 AM";
        public static string ConnectionString;
    }

}
