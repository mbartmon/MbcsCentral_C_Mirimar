using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlDbCloner.Core.Data
{
    public class DataTransfer
    {
        public ServerConnection sourceConnection;
        SqlBulkCopy bulkCopy;
        public List<DataObject> SourceObjects;

        public DataTransfer(string src, string dest)
        {
            bulkCopy = new SqlBulkCopy(dest,SqlBulkCopyOptions.KeepIdentity);
            bulkCopy.BatchSize = 500;
            bulkCopy.NotifyAfter = 1000;

            //sourceConnection = new ServerConnection(new SqlConnection(src));
            var sourceServer = new Server(sourceConnection);
            InitServer(sourceServer);
            var db = sourceServer.Databases[sourceServer.ConnectionContext.DatabaseName];

            SourceObjects = new List<DataObject>();
            foreach (Table item in db.Tables)
            {
                if (!item.IsSystemObject)
                    SourceObjects.Add(new DataObject { Table = item.Name });
            }
        }

        private void InitServer(Server serv)
        {
            // set the default properties we want upon partial instantiation - 
            // smo is *really* slow if you don't do this
            serv.SetDefaultInitFields(typeof(Table), "IsSystemObject", "Name");
            serv.SetDefaultInitFields(typeof(StoredProcedure), "IsSystemObject", "Name");
            serv.SetDefaultInitFields(typeof(UserDefinedFunction), "IsSystemObject", "Name");
            serv.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.View), "IsSystemObject", "Name");
            serv.SetDefaultInitFields(typeof(Column), "Identity");
            serv.SetDefaultInitFields(typeof(Index), "IndexKeyType");
        }

        public void TrasnferData(string table, string query)
        {
            SqlDataReader reader = null;
            SqlCommand myCommand;

            try
            {
                //myCommand = new SqlCommand(query, sourceConnection.SqlConnectionObject);
                //reader = myCommand.ExecuteReader();

                bulkCopy.DestinationTableName = table;
                bulkCopy.WriteToServer(reader);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
    }
}
