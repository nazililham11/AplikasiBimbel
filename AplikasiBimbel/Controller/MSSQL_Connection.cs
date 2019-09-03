using AplikasiBimbel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AplikasiBimbel.Controller
{
    public class MSSQL_Connection : IDisposable
    {
        #region Private Member

        private readonly string _databaseName = "DatabaseBimbel";
        private readonly string _databaseDataSource = @"(localdb)\MSSQLLocalDB";
        private string _connectionString;

        #endregion


        public string ConnectionString {
            get {
                if (_connectionString == null)
                    return $"Data Source={_databaseDataSource};Initial Catalog={_databaseName};Integrated Security=True;";

                return _connectionString;

            }
        }

        public List<T> ExecuteStoredProcedureQuery<T>(string procedureName, SqlParameter[] parameters = null) 
            where T : IDatabaseEntity<T>, new()
        {
            List<T> list = new List<T>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = procedureName;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Open Connection
                        connection.Open();

                        //Fill The Parameters
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                                command.Parameters.Add(parameter);
                        }

                        //Execute The Query
                        SqlDataReader result = command.ExecuteReader();

                        //Fetch Result
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                list.Add(new T().Entity(result));
                            }
                        }

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return list;
        }

        public int? ExecuteStoredProcedureNonQuery(string procedureName, SqlParameter[] parameters = null)
        {
            int rowAffected = 0;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = procedureName;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Open Connection
                        connection.Open();

                        //Fill The Parameters
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                                command.Parameters.Add(parameter);
                        }

                        //Execute The Query
                        rowAffected = command.ExecuteNonQuery();

                        //Close the Connection
                        connection.Close();

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return rowAffected;
        }

        public string ExecuteStoredProcedureScalar(string procedureName, SqlParameter[] parameters = null)
        {
            string value = string.Empty;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = procedureName;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Open Connection
                        connection.Open();  

                        //Fill The Parameters
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                                command.Parameters.Add(parameter);
                        }

                        //Execute The Query
                        value = command.ExecuteScalar().ToString();

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return value;
        }

        public List<T> ExecuteStoredProcedureQuery<T>(string procedureName, string[] columnNames, SqlParameter[] parameters = null) 
            where T : IDatabaseEntity<T>, new()
        {
            List<T> list = new List<T>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = procedureName;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;
                        
                        //Fill The Parameters
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                                command.Parameters.Add(parameter);
                        }
                        
                        //Open Connection
                        connection.Open();

                        //Execute The Query
                        SqlDataReader result = command.ExecuteReader();

                        //Fetch Result
                        if (result.HasRows)
                        {
                            int[] columnOrders = CheckColumn(result, columnNames);
                            while (result.Read())
                            {
                                
                                list.Add(new T().Entity(result, columnOrders));
                            }
                        }

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return list;
        }

        public int[] CheckColumn(IDataReader reader, string[] columnName)
        {
            int[] columnOrders = new int[columnName.Length];

            for (int i = 0; i < columnName.Length; i++)
            {
                int index = -1;
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    if (columnName[i].Equals(reader.GetName(j)))
                    {
                        index = j;
                        break;
                    }
                }
                columnOrders[i] = index;
            }

            return columnOrders;
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                disposedValue = true;
            }
        }

        ~MSSQL_Connection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
