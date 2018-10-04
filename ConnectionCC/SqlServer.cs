using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConnectionCC
{
    class SqlServer:IDisposable
    {
        /// <summary>
        /// The Sql Connection private variable.
        /// </summary>
        private SqlConnection _sqlConnection;

        /// <summary>
        /// The list collection to store the sql parameter objects.
        /// </summary>
        private List<SqlParameter> _sqlParameters = new List<SqlParameter>();

        /// <summary>
        /// Dispose SqlServer object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose SqlServer object
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }
            }
        }

        /// <summary>
        /// Creata SqlServer object using connection string
        /// </summary>
        /// <param name="connectionString">The Sql Server connection string.</param>
        internal SqlServer(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        /// <summary>
        /// Clear SqlServer parameters
        /// </summary>
        public void ClearParameters()
        {
            _sqlParameters.Clear();
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="storedProcedureName">The Sql Server store procedure name.</param>
        public void Execute(string storedProcedureName)
        {
            using (SqlCommand sqlCommand = new SqlCommand(storedProcedureName, _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0; // Set the timeout to zero, unless we are going to handle the timeout exception.

                try
                {
                    // Add the parameters from the collection. 
                    foreach (SqlParameter sqlParameter in _sqlParameters)
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                    }

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// Execute SqlQuery script
        /// </summary>
        /// <param name="sqlQuery">The Sql Server query.</param>
        public void ExecuteText(string sqlQuery)
        {
            using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Execute SqlQuery script and return DataSet
        /// </summary>
        /// <param name="sqlQuery">The Sql Server query.</param>
        /// <returns>DataSet object.</returns>
        public DataSet ExecuteTextRetrieveDataSet(string sqlQuery)
        {
            using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;

                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DateTime startDateTime;
                startDateTime = DateTime.Now;

                sqlDataAdapter.Fill(dataSet);

                DateTime endDateTime;
                endDateTime = DateTime.Now;

                //this._log.Write(_sqlConnection.DataSource + "/" + _sqlConnection.Database + ".RetrieveDataSet", sqlQuery, startDateTime, endDateTime);

                return dataSet;
            }
        }

        /// <summary>
        /// Execute stored procedure and return DataSet
        /// </summary>
        /// <param name="storedProcedureName">The Sql Server store procedure name.</param>
        /// <returns>DataSet object.</returns>
        public DataSet RetrieveDataSet(string storedProcedureName)
        {
            using (SqlCommand sqlCommand = new SqlCommand(storedProcedureName, _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                try
                {
                    // Add the parameters from the collection.
                    foreach (SqlParameter sqlParameter in _sqlParameters)
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                    }

                    DataSet dataSet = new DataSet();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    DateTime startDateTime;
                    startDateTime = DateTime.Now;

                    sqlDataAdapter.Fill(dataSet);

                    DateTime endDateTime;
                    endDateTime = DateTime.Now;

                    //this._log.Write(_sqlConnection.DataSource + "/" + _sqlConnection.Database + ".RetrieveDataSet", storedProcedureName, startDateTime, endDateTime);

                    return dataSet;
                }
                finally
                {
                    _sqlParameters.Clear();
                    sqlCommand.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// Add SqlServer parameter using int value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be int.</param>
        public void AddInParameter(string parameterName, int parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = parameterValue;
            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using int(nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be int or null.</param>
        public void AddInParameter(string parameterName, int? parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Input;

            if (parameterValue.HasValue)
            {
                sqlParameter.Value = parameterValue;
            }
            else
            {
                sqlParameter.Value = DBNull.Value;
            }

            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using decimal value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be decimal.</param>
        public void AddInParameter(string parameterName, decimal parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Decimal);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = parameterValue;
            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using decimal(nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be decimal or null.</param>
        public void AddInParameter(string parameterName, decimal? parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Decimal);
            sqlParameter.Direction = ParameterDirection.Input;

            if (parameterValue.HasValue)
            {
                sqlParameter.Value = parameterValue;
            }
            else
            {
                sqlParameter.Value = DBNull.Value;
            }

            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using double value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be double.</param>
        public void AddInParameter(string parameterName, double parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Float);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = parameterValue;
            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using double(nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be double or null.</param>
        public void AddInParameter(string parameterName, double? parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.Float);
            sqlParameter.Direction = ParameterDirection.Input;

            if (parameterValue.HasValue)
            {
                sqlParameter.Value = parameterValue;
            }
            else
            {
                sqlParameter.Value = DBNull.Value;
            }

            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using string value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be string.</param>
        public void AddInParameter(string parameterName, string parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.VarChar);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = parameterValue;
            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using DateTime value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be datetime.</param>
        public void AddInParameter(string parameterName, DateTime parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.DateTime);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = parameterValue;
            _sqlParameters.Add(sqlParameter);
        }

        /// <summary>
        /// Add SqlServer parameter using DateTime(nullable) value
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="parameterValue">The parameter value. Value must be datetime or null.</param>
        public void AddInParameter(string parameterName, DateTime? parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.DateTime);
            sqlParameter.Direction = ParameterDirection.Input;

            if (parameterValue.HasValue)
            {
                sqlParameter.Value = parameterValue;
            }
            else
            {
                sqlParameter.Value = DBNull.Value;
            }

            _sqlParameters.Add(sqlParameter);
        }

        public void AddInParameter(string parameterName, byte[] parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("@" + parameterName, SqlDbType.VarBinary);
            sqlParameter.Direction = ParameterDirection.Input;

            if (parameterValue.Length > 0)
            {
                sqlParameter.Value = parameterValue;
            }
            else
            {
                sqlParameter.Value = DBNull.Value;
            }

            _sqlParameters.Add(sqlParameter);
        }
    }
}
