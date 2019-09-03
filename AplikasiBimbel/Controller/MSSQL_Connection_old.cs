using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Controller
{
    public class MSSQL_Connection_old
    {
        #region Singleton

        private static MSSQL_Connection_old _instance;
        private static readonly object padlock = new object();
        public static MSSQL_Connection_old Instance {
            get {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new MSSQL_Connection_old();
                    }
                    return _instance;
                }
            }
        }
        #endregion


        #region Data
        /// <summary>
        /// Get Last Connection Result 
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Connection Model
        /// </summary>
        public ConnectionModel ConnectionModel { get; set; }

        public string ConnectionString { get; set; }

        #endregion



        #region Constructor

        public MSSQL_Connection_old()
        {
        }

        public MSSQL_Connection_old(ConnectionModel connection)
        {
            ConnectionModel = connection;
        }

        public MSSQL_Connection_old(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion


        



        /// <summary>
        /// Execute Query
        /// </summary>
        /// <typeparam name="T">Models for Result</typeparam>
        /// <param name="Query">Query</param>
        /// <param name="Entity">Function to Parsing Result To Models</param>
        /// <returns>List of Models (return null if error)</returns>
        public List<T> ExecuteQuery<T>(string Query, Func<SqlDataReader, T> Entity) where T : class
        {

            //if (!IsConnected)
            //    return null;

            List<T> data = new List<T>();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(Query, connection) { CommandTimeout = 60 };
                    SqlDataReader result = command.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                            data.Add(Entity(result));
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + Query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return data;
        }


        public DataTable ExecuteQuery(string query)
        {
            //if (!IsConnected)
                //return null;

            DataTable data = new DataTable();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection) {
                        CommandTimeout = 60
                    };
                    SqlDataAdapter result = new SqlDataAdapter(command);

                    result.Fill(data);

                    if (connection != null && connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return data;
        }


        /// <summary>
        /// Execute Non Query that result How Many Row Effect
        /// </summary>
        /// <param name="Query">Wuery</param>
        /// <returns>Many Row Effect (return null if error)</returns>
        public int? ExecuteNonQuery(string Query)
        {
            if (!IsConnected)
                return null;

            int rowaffected = 0;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = new SqlCommand(Query, connection) { CommandTimeout = 60 })
                    {
                        connection.Open();
                        rowaffected = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + Query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return rowaffected;
        }



        /// <summary>
        /// Execute Scalar that result only one column and one row
        /// </summary>
        /// <param name="Query">Query</param>
        /// <param name="connectionString">Custom Connection String (null to default)</param>
        /// <returns>Value(return null if error)</returns>
        public int? ExecuteScalar(string Query, string connectionString = null)
        {
            int value = 0;
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = this.ConnectionString;
                if (!IsConnected) return null;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(Query, connection) { CommandTimeout = 60 };
                    value = int.TryParse(command.ExecuteScalar().ToString(), out value) ? value : 0;
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + Query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return value;
        }


        public bool CheckDatabaseServer(ConnectionModel connection)
        {
            return CheckDatabaseServer(connection.DatabaseHost, connection.DatabasePort,
                                       connection.DatabaseUsername, connection.DatabasePassword,
                                       connection.DatabaseName);
        }

        /// <summary>
        /// Check Connection to Database
        /// </summary>
        /// <param name="Host">Database Host (empty/null to default)</param>
        /// <param name="Port">Database Port (empty/null to default)</param>
        /// <param name="Username">Database Username (empty/null to default)</param>
        /// <param name="Password">Database Password (empty/null to default)</param>
        /// <param name="DatabaseName">Database Name (empty/null to default)</param>
        /// <returns>true if Database Connected</returns>
        public bool CheckDatabaseServer(string Host = null, string Port = null, string Username = null, string Password = null, string DatabaseName = null)
        {

            ConnectionModel con = new ConnectionModel()
            {
                DatabaseHost = Host ?? this.ConnectionModel.DatabaseHost,
                DatabasePort = Port ?? this.ConnectionModel.DatabasePort,
                DatabaseUsername = Username ?? this.ConnectionModel.DatabaseUsername,
                DatabasePassword = Password ?? this.ConnectionModel.DatabasePassword,
                DatabaseName = DatabaseName ?? this.ConnectionModel.DatabaseName
            };

            string connectionString = con.GetMSSQLConnectionString();

            return CheckDatabaseServer(connectionString);
        }

        public bool CheckDatabaseServer(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return false;

            bool isConnected = false;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                isConnected = true;

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (SqlException ex)
            {
                string sqlErrorMessage = "Message: " + ex.Message + "\n" +
                                         "Number: " + ex.Number + "\n" +
                                         "Con String: " + connectionString;


                MessageBox.Show(sqlErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isConnected = false;

            }
            catch (ArgumentException a_ex)
            {
                string msg = "Check the Connection String." + "\n" +
                             "Message:" + a_ex.Message + "\n" + a_ex.ToString();
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Connection String : \n" + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return isConnected;
        }



        /// <summary>
        /// Create Database From Query.sql
        /// </summary>
        /// <returns>Is Creating Databse Complete</returns>
        public bool CreateDatabase()
        {
            bool isSuccess = false;
            string connectionString = $"datasource=127.0.0.1;port=3306;username={ConnectionModel.DatabaseUsername};password={ConnectionModel.DatabasePassword};";
            try
            {
                string queryPath = Path.GetFullPath("/Assets/Query.sql");
                using (var connection = new SqlConnection(connectionString))
                {
                    //SqlScript script = new SqlScript(connection, File.ReadAllText("Query.sql"))
                    //{
                    //    Delimiter = "$$"
                    //};
                    //if (script.Execute() > 0) isSuccess = true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Code : " + ex.HResult + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return isSuccess;
        }

        public void CheckColumn(IDataReader reader)
        {
            string[] columnName = { "Teacher_ID", "Username", "Password", "Name", "Address", "PhoneNumberr", "Permission", "Status", "DateIn", "DateOUt" };
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
        }

        public TeacherModel TeacherEntity(IDataReader reader, int[] orders)
        {
            TeacherModel teacher = new TeacherModel();

            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i] < 0 || reader.IsDBNull(orders[i]))
                    continue;

                switch (i)
                {
                    case 0 :
                        teacher.Teacher_ID = reader.GetInt32(orders[i]);
                        break;
                    case 1:
                        teacher.Username = reader.GetString(orders[i]);
                        break;
                    case 2:
                        teacher.Password = reader.GetString(orders[i]);
                        break;
                    case 3:
                        teacher.Name = reader.GetString(orders[i]);
                        break;
                    case 4:
                        teacher.Address = reader.GetString(orders[i]);
                        break;
                    case 5:
                        teacher.PhoneNumber = reader.GetString(orders[i]);
                        break;
                    case 6:
                        teacher.Permission = reader.GetString(orders[i]);
                        break;
                    case 7:
                        teacher.Status = reader.GetString(orders[i]);
                        break;
                    case 8:
                        teacher.DateIn = reader.GetDateTime(orders[i]);
                        break;
                    case 9:
                        teacher.DateOut = reader.GetDateTime(orders[i]);
                        break;
                }
            }


            return teacher;
        }

    }

}
