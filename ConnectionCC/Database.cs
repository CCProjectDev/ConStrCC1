using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionCC
{
    public class Database : IDisposable
    {
        /// <summary>
        /// Database type enumeration CC 
        /// </summary>
        public enum DatabaseType
        {
            SqlServer
        }

        /// <summary>
        /// Data type enumeration
        /// </summary>
        public enum DataType
        {
            DateTime,
            Number,
            Integer,
            Varchar,
            Varbinary
        }

        /// <summary>
        /// The SqlServer class object.
        /// </summary>
        private SqlServer _sqlServer;

        /// <summary>
        /// The database type.
        /// </summary>
        private DatabaseType _databaseType = new DatabaseType();

        /// <summary>
        /// Log variable
        /// </summary>
        private bool _newLog;

        /// <summary>
        /// Dispose database objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose database objects
        /// </summary>
        /// <param name="disposing">Set True to dispose object.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sqlServer != null)
                {
                    _sqlServer.Dispose();
                    _sqlServer = null;
                }
            }
        }

        //public Database(string applicationId, string connectionName)
        //{
        //    this._newLog = false;

        //    SetupDatabase(applicationId, connectionName);
        //}

        public Database(string connectionName)
        {
            //this._newLog = false;
            //this._log = log;
            _sqlServer = new SqlServer(connectionName);
            //SetupDatabase(log.ApplicationID, connectionName);
        }

        /// <summary>
        /// Connect to database using window authentication
        /// </summary>
        /// <param name="log"></param>
        //public Database(Logging.Log log, DatabaseType databaseType)
        //{
        //    this._newLog = false;
        //    this._log = log;
        //}

        //public Database(int logNumber, string applicationId, string connectionName)
        //{
        //    this._newLog = true;
        //    this._log = new Logging.Log(logNumber);

        //    SetupDatabase(applicationId, connectionName);
        //}

        //public Database(int logNumber, string connectionName)
        //{
        //    this._newLog = true;
        //    this._log = new Logging.Log(logNumber);

        //    SetupDatabase(_log.ApplicationID, connectionName);
        //}

        //private void SetupDatabase(string applicationId, string connectionName)
        //{
        //    // Get the connection info from framework.
        //    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(Config.FrameworkConnectionString);

        //    string procedureName = "GET_APPLICATION_CONNECTION";

        //    System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(procedureName, sqlConnection);

        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //    // Always Set Timeout equal to Zero unless you are goinf to handle the timeout error.
        //    sqlCommand.CommandTimeout = 0;

        //    sqlCommand.Parameters.Add("@APP_ID", System.Data.SqlDbType.VarChar);
        //    sqlCommand.Parameters["@APP_ID"].Direction = System.Data.ParameterDirection.Input;
        //    sqlCommand.Parameters["@APP_ID"].Value = applicationId;
        //    //_log.WriteDebug("ApplicationID", applicationId);

        //    sqlCommand.Parameters.Add("@ALIAS", System.Data.SqlDbType.VarChar);
        //    sqlCommand.Parameters["@ALIAS"].Direction = System.Data.ParameterDirection.Input;
        //    sqlCommand.Parameters["@ALIAS"].Value = connectionName;

        //    // Create the DataSet to hold the results.
        //    DataSet ds = new DataSet();

        //    // A Data Adapter is needed.
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

        //    // Open the connection.
        //    sqlConnection.Open();

        //    try
        //    {
        //        // Track the DataTime Now for Metrics.
        //        DateTime dtStart;
        //        dtStart = DateTime.Now;

        //        // Fill the DataSet with data.
        //        sqlDataAdapter.Fill(ds);

        //        DateTime dtEnd;
        //        dtEnd = DateTime.Now;

        //        // Put an entry in the log. Decide if this is necessary or not since we are also recording the data in the metrics table.

        //        // Put an entry in the Metrics table.
        //        _log.Write(sqlConnection.DataSource + "/" + sqlConnection.Database + ".RetrieveDataSet", procedureName, dtStart, dtEnd, 1);

        //        // This dataset should contain exactly 1 row.
        //        switch (ds.Tables[0].Rows.Count)
        //        {
        //            case 0: // No rows were returned.
        //                {
        //                    throw new ApplicationException("The Named Connection was not found for this application.");
        //                }
        //            case 1: // If one row was returned then we received data to use.
        //                {

        //                    // The connection info is passed back in a DataSet.
        //                    string connAttribute = ds.Tables[0].Rows[0]["ATTRIBUTE"].ToString();
        //                    string databaseTypeString = ds.Tables[0].Rows[0]["DATABASE_TYPE"].ToString().ToUpper();

        //                    string serverDatabase = string.Empty;
        //                    string useridDatabase = string.Empty;
        //                    string passwordDatabase = string.Empty;
        //                    string databaseNameOrDSN = string.Empty;
        //                    bool isWindowAuthentication = false;

        //                    XmlDocument doc = new XmlDocument();
        //                    doc.LoadXml(connAttribute);

        //                    XmlElement elm = doc.DocumentElement["row"];

        //                    if (elm.Attributes["window_authentication"] == null)
        //                    {
        //                        serverDatabase = elm.Attributes["server"].Value;
        //                        useridDatabase = elm.Attributes["userid"].Value;
        //                        passwordDatabase = elm.Attributes["password"].Value;
        //                        databaseNameOrDSN = elm.Attributes["database"].Value;
        //                    }
        //                    else
        //                    {
        //                        serverDatabase = elm.Attributes["server"].Value;
        //                        databaseNameOrDSN = elm.Attributes["database"].Value;
        //                        isWindowAuthentication = true;
        //                    }

        //                    // If the type is valid instantiate the actual database type.
        //                    switch (databaseTypeString)
        //                    {
        //                        case "SQLSERVER":
        //                            {
        //                                _databaseType = DatabaseType.SqlServer;

        //                                if (!isWindowAuthentication)
        //                                    _sqlServer = new SqlServer(ContructConnectionString(serverDatabase, useridDatabase, Tools.DecryptString(passwordDatabase), databaseNameOrDSN, _databaseType));
        //                                else
        //                                    _sqlServer = new SqlServer(ContructConnectionString(serverDatabase, databaseNameOrDSN, _databaseType));

        //                                break;
        //                            }
        //                        default:
        //                            throw new ApplicationException("Database Type " + databaseTypeString + " is not allowed.");
        //                    }

        //                    break;
        //                }
        //            default: // The wrong number of rows were returned.
        //                {
        //                    throw new ApplicationException("The wrong number of rows were returned from the database for the Named Connection for this Application. Row Count = " + ds.Tables[0].Rows.Count.ToString());
        //                }
        //        }
        //    }
        //    finally
        //    {
        //        // Make sure the connection is closed.
        //        sqlConnection.Close();
        //    }

        //    if (sqlCommand != null)
        //    {
        //        sqlCommand.Dispose();
        //        sqlCommand = null;
        //    }

        //    if (sqlConnection != null)
        //    {
        //        sqlConnection.Dispose();
        //        sqlConnection = null;
        //    }
        //}

        //private string ContructConnectionString(string serverName, string userId, string password, string databaseNameOrDSN, DatabaseType databaseType)
        //{
        //    string connectionString = string.Empty;

        //    switch (databaseType)
        //    {
        //        case DatabaseType.SqlServer:
        //            connectionString = string.Format(CultureInfo.InvariantCulture, "Data Source={0}; User Id={1}; Password={2}; Initial Catalog={3}",
        //                 serverName,
        //                 userId,
        //                 password,
        //                 databaseNameOrDSN);
        //            break;
        //        default:
        //            throw new ApplicationException("Database type is invalid.");
        //    }

        //    return connectionString;
        //}

        //private string ContructConnectionString(string serverName, string databaseNameOrDSN, DatabaseType databaseType)
        //{
        //    string connectionString = string.Empty;

        //    switch (databaseType)
        //    {
        //        case DatabaseType.SqlServer:
        //            connectionString = string.Format(CultureInfo.InvariantCulture, "Initial Catalog={0};Data Source={1};Integrated Security=SSPI",
        //                 databaseNameOrDSN,
        //                 serverName);
        //            break;
        //        default:
        //            throw new ApplicationException("Database type is invalid.");
        //    }

        //    return connectionString;
        //}

        /// <summary>
        /// Clear database stored procedure parameters
        /// </summary>
        public void ClearParameter()
        {
            switch (_databaseType)
            {
                case DatabaseType.SqlServer:
                    _sqlServer.ClearParameters();
                    break;
                default:
                    throw new ApplicationException("Database type is invalid.");
            }
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="storeProcedureName">The store procedure name.</param>
        public void Execute(string storeProcedureName)
        {
            switch (_databaseType)
            {
                case DatabaseType.SqlServer:
                    _sqlServer.Execute(storeProcedureName);
                    break;
                default:
                    throw new ApplicationException("Database type is invalid.");
            }
        }

        /// <summary>
        /// Execute SqlQuery script
        /// </summary>
        /// <param name="sqlQuery">The Sql Query string.</param>
        public void ExecuteText(string sqlQuery)
        {
            switch (_databaseType)
            {
                case DatabaseType.SqlServer:
                    _sqlServer.ExecuteText(sqlQuery);
                    break;
                default:
                    throw new ApplicationException("Database type is invalid.");
            }
        }

        /// <summary>
        /// Execute stored procedure and return DataSet
        /// </summary>
        /// <param name="storedProcedureName">The store procedure name.</param>
        /// <returns></returns>
        public DataSet RetrieveDataSet(string storedProcedureName)
        {
            DataSet dataSet = new DataSet();

            try
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        dataSet = _sqlServer.RetrieveDataSet(storedProcedureName);
                        break;
                    default:
                        throw new ApplicationException("Database type is invalid.");
                }
            }
            catch
            {
                throw;
            }

            return dataSet;
        }

        /// <summary>
        /// Execute SQL Query string and return DataSet
        /// </summary>
        /// <param name="sqlQuery">The Sql Query string.</param>
        /// <returns></returns>
        public DataSet ExecuteTextRetrieveDataSet(string sqlQuery)
        {
            DataSet dataSet = new DataSet();

            try
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        dataSet = _sqlServer.ExecuteTextRetrieveDataSet(sqlQuery);
                        break;
                    default:
                        throw new ApplicationException("Database type is invalid.");
                }
            }
            catch
            {
                throw;
            }

            return dataSet;
        }

        /// <summary>
        /// Execute SQL Query and return DataTable
        /// </summary>
        /// <param name="sqlQuery">The Sql Query string.</param>
        /// <returns></returns>
        public DataTable ExecuteTextRetrieveDataTable(string sqlQuery)
        {
            DataSet dataSet = ExecuteTextRetrieveDataSet(sqlQuery);
            DataTable dataTable;

            switch (dataSet.Tables.Count)
            {
                case 0:
                    throw new ApplicationException("RetrieveDataTable Error: No tables were returned from the database.");
                case 1:
                    dataTable = dataSet.Tables[0];
                    break;
                default:
                    throw new ApplicationException("RetrieveDataTable Error: Too many tables were returned from the database. Count = " + dataSet.Tables.Count.ToString());
            }

            return dataTable;
        }

        /// <summary>
        /// Execute stored procedure and return DataTable
        /// </summary>
        /// <param name="storedProcedureName">The store procedure name.</param>
        /// <returns></returns>
        public DataTable RetrieveDataTable(string storedProcedureName)
        {
            DataSet dataSet = RetrieveDataSet(storedProcedureName);
            DataTable dataTable;

            switch (dataSet.Tables.Count)
            {
                case 0:
                    throw new ApplicationException("RetrieveDataTable Error: No tables were returned from the database.");
                case 1:
                    dataTable = dataSet.Tables[0];
                    break;
                default:
                    throw new ApplicationException("RetrieveDataTable Error: Too many tables were returned from the database. Count = " + dataSet.Tables.Count.ToString());
            }

            return dataTable;
        }

        /// <summary>
        /// Add database parameter for int value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Integer for parameterValue data type is int.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, int parameterValue)
        {
            if (dataType == DataType.Integer)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for int (nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Integer for parameterValue data type is int?.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, int? parameterValue)
        {
            if (dataType == DataType.Integer)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for decimal value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Number fpr parameterValue data type is decimal.</param>
        /// <param name="parameterValue">The parameter value. Value is decimal.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, decimal parameterValue)
        {
            if (dataType == DataType.Number)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for decimal (nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Number for parameterValue data type is decimal?.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, decimal? parameterValue)
        {
            if (dataType == DataType.Number)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for double value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Number for parameterValue data type is double.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, double parameterValue)
        {
            if (dataType == DataType.Number)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for double (nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Number for parameterValue data type is double?.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, double? parameterValue)
        {
            if (dataType == DataType.Number)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST number.");
            }
        }

        /// <summary>
        /// Add database parameter for string value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Varchar for parameterValue data type is string.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, string parameterValue)
        {
            if (dataType == DataType.Varchar)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST varchar.");
            }
        }

        /// <summary>
        /// Add database parameter for DateTime value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Datetime for parameterValue data type is DateTime.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, DateTime parameterValue)
        {
            if (dataType == DataType.DateTime)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST datetime.");
            }
        }

        /// <summary>
        /// Add database parameter for DateTime(nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Datetime for parameterValue data type is DateTime?.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, DateTime? parameterValue)
        {
            if (dataType == DataType.DateTime)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST datetime.");
            }
        }

        /// <summary>
        /// Add database parameter for DateTime value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="dataType">The parameter data type. dataType Datetime for parameterValue data type is DateTime.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public void AddInParameter(string parameterName, Database.DataType dataType, byte[] parameterValue)
        {
            if (dataType == DataType.Varbinary)
            {
                switch (_databaseType)
                {
                    case DatabaseType.SqlServer:
                        _sqlServer.AddInParameter(parameterName, parameterValue);
                        break;
                    default:
                        throw new ApplicationException("Database Type is invalid.");
                }
            }
            else
            {
                throw new ApplicationException("Data type MUST datetime.");
            }
        }

        /// <summary>
        /// Get database connection string
        /// </summary>
        /// <param name="serverName">The database server location.</param>
        /// <param name="userId">The database user login.</param>
        /// <param name="password">The database user password login.</param>
        /// <param name="databaseName">The database name.</param>
        /// <param name="databaseType">The database type.</param>
        /// <returns></returns>
        //public string GetConnectionString(string serverName, string userId, string password, string databaseNameOrDSN, DatabaseType databaseType)
        //{
        //    string connectionString = string.Empty;

        //    switch (databaseType)
        //    {
        //        case DatabaseType.SqlServer:
        //            connectionString = string.Format(CultureInfo.InvariantCulture, "Data Source={0}; User Id={1}; Password={2}; Initial Catalog={3}",
        //                 serverName,
        //                 userId,
        //                 password,
        //                 databaseNameOrDSN);
        //            break;
        //        default:
        //            throw new ApplicationException("Database type is invalid.");
        //    }

        //    return connectionString;
        //}
    }
}
