using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace MbcsCentral
{
    public class SqlCreateSchemaAndTables
    {
        //public static Server SourceServer = new Server(@"DESKTOP-BEV9G3L\MbcsCentralSQL");
        //public static Server TargetServer = new Server(@"Windows10\MbcsCentralNW,1433");
        //public static string DatabaseName = "MbcsCentral_fishmanTEST";
        public static string userName = "mbartmon";
        public static string passWord = "Vietnam65";
        public static List<StringCollection> dropScript;
        public static List<StringCollection> createScript;

        public string SourceServerName { get; set; }
        public string TargetServerName { get; set; }
        public string SourceDatabaseName { get; set; }
        public string TargetDatabaseName { get; set; }
        public frmProgressWindow Progress { get; set; }

        private Server SourceServer;
        private Server TargetServer;
        public void Start()
        {
            try
            {
                SourceServer = new Server(SourceServerName);
                SourceServer.ConnectionContext.LoginSecure = false;
                SourceServer.ConnectionContext.Login = GlobalShared.DB_USER_NAME;
                SourceServer.ConnectionContext.Password = GlobalShared.DB_PASSWORD;
                SourceServer.ConnectionContext.Connect();

                Progress.txtProgress.Dispatcher.Invoke((Action)(() => Progress.txtProgress.Text = "GENERATING SQL SCRIPTS"));
                GenerateTableScripts(SourceServer);
                Progress.txtProgress.Dispatcher.Invoke((Action)(() => Progress.txtProgress.Text = "SQL SCRIPTS GENERATED"));

                string connectionString = MbcsCentral.GlobalShared.SqlBaseConnectionString.Replace("<source>", TargetServerName).
                                                                        Replace("<catalog>", TargetDatabaseName).
                                                                        Replace("<user>", GlobalShared.DB_USER_NAME).
                                                                        Replace("<password>", GlobalShared.DB_PASSWORD);

                TargetServer = new Server(TargetServerName);
                TargetServer.ConnectionContext.LoginSecure = false;
                TargetServer.ConnectionContext.Login = GlobalShared.DB_USER_NAME;
                TargetServer.ConnectionContext.Password = GlobalShared.DB_PASSWORD;
                TargetServer.ConnectionContext.ConnectionString = connectionString;
                TargetServer.ConnectionContext.Connect();

                Progress.txtProgress.Dispatcher.Invoke((Action)(() => Progress.txtProgress.Text = "RUNNING SQL DROP SCRIPTS"));
                RunScripts(TargetServer, TargetDatabaseName, dropScript);
                Progress.txtProgress.Dispatcher.Invoke((Action)(() => Progress.txtProgress.Text = "RUNNING SQL TABLE CREATE SCRIPTS"));
                RunScripts(TargetServer, TargetDatabaseName, createScript);

            }
            catch (Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Copy Database (Start): " + ex.Message, false);
            }
            finally
            {
                if (SourceServer.ConnectionContext.IsOpen)
                    SourceServer.ConnectionContext.Disconnect();
                if (TargetServer !=null && TargetServer.ConnectionContext.IsOpen)
                    TargetServer.ConnectionContext.Disconnect();
            }
        }

        private bool RunScripts(Server server, string databaseName, List<StringCollection> list)
        {

            foreach (StringCollection collection in list)
            {
                foreach (string script in collection)
                {
                    try
                    {
                        server.ConnectionContext.ExecuteNonQuery(script);
                    }
                    catch (Exception ex)
                    {
                        GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Copy Database (RunScripts): " +  ex.Message, false);
                    }
                }
            }
            return true;
        }
        private  void GenerateTableScripts(Server SourceServer)
        {
            dropScript = new List<StringCollection>();
            createScript = new List<StringCollection>();
            Scripter scripter = new Scripter(SourceServer);
            Database myMbcsCentral_fishmanTEST = SourceServer.Databases[SourceDatabaseName];
            /* With ScriptingOptions you can specify different scripting
             * options, for example to include IF NOT EXISTS, DROP
             * statements, output location etc*/
            ScriptingOptions scriptOptions = new ScriptingOptions();
            scriptOptions.ScriptDrops = true;
            scriptOptions.IncludeIfNotExists = true;
            foreach (Microsoft.SqlServer.Management.Smo.Table myTable in myMbcsCentral_fishmanTEST.Tables)
            {
                /* Generating IF EXISTS and DROP command for tables */
                StringCollection tableScripts = myTable.Script(scriptOptions);
                dropScript.Add(tableScripts);
                /* Generating CREATE TABLE command */
                tableScripts  = GenerateTableScriptWithDependencies(SourceServer, myTable.Name);
                createScript.Add(tableScripts);
            }
        }
        private StringCollection GenerateTableScriptWithDependencies(Server SourceServer, string tableName)
        {
            Scripter scripter = new Scripter(SourceServer);
            Database database = SourceServer.Databases[SourceDatabaseName];
            Microsoft.SqlServer.Management.Smo.Table myTable = database.Tables[tableName];
            /* Generate Scripts of table along with for all
             * objects on which this table depends on */
            ScriptingOptions scriptOptionsForDependendencies = new ScriptingOptions();
            scriptOptionsForDependendencies.WithDependencies = true;
            /* DriAll will include all DRI objects in the generated script. */
            scriptOptionsForDependendencies.DriAll = true;
            StringCollection tableScripts = myTable.Script(scriptOptionsForDependendencies);

            return tableScripts;
        }
        private void CreateNewTables(Server server, StringCollection tables)
        {
            server.ConnectionContext.LoginSecure = false;
            server.ConnectionContext.Login = userName;
            server.ConnectionContext.Password = passWord;
            try
            {
                server.ConnectionContext.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (string script in tables)
                server.Databases[SourceDatabaseName].ExecuteNonQuery(script);

        }
        private void GenerateDBScript(Server SourceServer)
        {
            Scripter scripter = new Scripter(SourceServer);
            Database myMbcsCentral_fishmanTEST = SourceServer.Databases[SourceDatabaseName];
            StringCollection scriptCollection = scripter.Script(new Urn[] { myMbcsCentral_fishmanTEST.Urn });
            foreach (string script in scriptCollection)
                Console.WriteLine(script);
        }
        private List<StringCollection> GenerateTableDropScript(Server SourceServer)
        {
            List<StringCollection> list = new List<StringCollection>();
            Scripter scripter = new Scripter(SourceServer);
            Database database = SourceServer.Databases[SourceDatabaseName];
            /* With ScriptingOptions you can specify different scripting
             * options, for example to include IF NOT EXISTS, DROP
             * statements, output location etc*/
            ScriptingOptions scriptOptions = new ScriptingOptions();
            scriptOptions.ScriptDrops = true;
            scriptOptions.IncludeIfNotExists = true;
            foreach (Microsoft.SqlServer.Management.Smo.Table myTable in database.Tables)
            {
                /* Generating IF EXISTS and DROP command for tables */
                StringCollection tableScripts = myTable.Script(scriptOptions);
                list.Add(tableScripts);
                //foreach (string script in tableScripts)
                //    Console.WriteLine(script);
             }
            return list;
        }
        private  void GenerateTableScriptWithIndexes(Server SourceServer)
        {
            Scripter scripter = new Scripter(SourceServer);
            Database myMbcsCentral_fishmanTEST = SourceServer.Databases["MbcsCentral_fishmanTEST"];
            /* With ScriptingOptions you can specify different scripting
             * options, for example to include IF NOT EXISTS, DROP
             * statements, output location etc*/
            ScriptingOptions scriptOptions = new ScriptingOptions();
            scriptOptions.ScriptDrops = true;
            scriptOptions.IncludeIfNotExists = true;
            foreach (Microsoft.SqlServer.Management.Smo.Table myTable in myMbcsCentral_fishmanTEST.Tables)
            {
                /* Generating IF EXISTS and DROP command for tables */
                StringCollection tableScripts = myTable.Script(scriptOptions);
                foreach (string script in tableScripts)
                    Console.WriteLine(script);
                /* Generating CREATE TABLE command */
                tableScripts = myTable.Script();
                foreach (string script in tableScripts)
                    Console.WriteLine(script);
                IndexCollection indexCol = myTable.Indexes;
                foreach (Index myIndex in myTable.Indexes)
                {
                    /* Generating IF EXISTS and DROP command for table indexes */
                    StringCollection indexScripts = myIndex.Script(scriptOptions);
                    foreach (string script in indexScripts)
                        Console.WriteLine(script);
                    /* Generating CREATE INDEX command for table indexes */
                    indexScripts = myIndex.Script();
                    foreach (string script in indexScripts)
                        Console.WriteLine(script);
                }
            }
        }


        private void GenerateScriptWithoutCreatingObjectOnServer(Server SourceServer)
        {
            /* Drop the database if it exists */
            if (SourceServer.Databases[SourceDatabaseName] != null)
                SourceServer.Databases[SourceDatabaseName].Drop();
            /* Create database called, "MyNewDatabase" */
            Database myDatabase = new Database(SourceServer, SourceDatabaseName);
            /* Output the database script on the console */
            StringCollection DBScripts = myDatabase.Script();
            foreach (string script in DBScripts)
                Console.WriteLine(script);
            /* Create a table instance */
            Microsoft.SqlServer.Management.Smo.Table myEmpTable = new  Microsoft.SqlServer.Management.Smo.Table(myDatabase, "MyEmpTable");
            /* Add [EmpID] column to created table instance */
            Column empID = new Column(myEmpTable, "EmpID", DataType.Int);
            empID.Identity = true;
            myEmpTable.Columns.Add(empID);
            /* Add another column [EmpName] to created table instance */
            Column empName = new Column(myEmpTable, "EmpName", DataType.VarChar(200));
            empName.Nullable = true;
            myEmpTable.Columns.Add(empName);
            /* Add third column [DOJ] to created table instance with default constraint */
            Column DOJ = new Column(myEmpTable, "DOJ", DataType.DateTime);
            DOJ.AddDefaultConstraint(); // you can specify constraint name here as well
            DOJ.DefaultConstraint.Text = "GETDATE()";
            myEmpTable.Columns.Add(DOJ);
            /* Add primary key index to the table */
            Index primaryKeyIndex = new Index(myEmpTable, "PK_MyEmpTable");
            primaryKeyIndex.IndexKeyType = IndexKeyType.DriPrimaryKey;
            primaryKeyIndex.IndexedColumns.Add(new IndexedColumn(primaryKeyIndex, "EmpID"));
            myEmpTable.Indexes.Add(primaryKeyIndex);
            /* Output the table script on the console */
            StringCollection TableScripts = myEmpTable.Script();
            foreach (string script in TableScripts)
                Console.WriteLine(script);
            /* If you want to create objects on the server you need call 
             * create method or else objects will not be created on the server */
            myDatabase.Create();
            myEmpTable.Create();
        }
     }
}


