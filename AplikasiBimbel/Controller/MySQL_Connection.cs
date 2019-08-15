using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace AplikasiBimbel.Controller
{

    /// <summary>
    /// Connection to MYSQL Database
    /// </summary>
    public class MySQL_Connection
	{


        #region Data

        /// <summary>
        /// Get Last Connection Result 
        /// </summary>
        public bool IsConnected;

        public string DatabaseHost;
        public string DatabasePort;
        public string DatabaseUsername;
        public string DatabasePassword;
        public string DatabaseName;


        /// <summary>
        /// Get Connection String From Settings
        /// </summary>
        public string ConnectionString {
			get {
				return $"datasource={DatabaseHost};port={DatabasePort}" +
					   $";username={DatabaseUsername};password={DatabasePassword}" +
					   $";database={DatabaseName};";
			}
		}

        #endregion



        #region Constructor
        public MySQL_Connection()
        {
        }

        public MySQL_Connection( string DatabaseHost, string DatabasePort,
                                 string DatabaseUsername, string DatabasePassword, 
                                 string DatabaseName)
		{
            this.DatabaseHost = DatabaseHost;
            this.DatabasePort = DatabasePort;
            this.DatabaseUsername = DatabaseUsername;
            this.DatabasePassword = DatabasePassword;
            this.DatabaseName = DatabaseName;
		}

		#endregion



		/// <summary>
		/// Execute Query
		/// </summary>
		/// <typeparam name="T">Models for Result</typeparam>
		/// <param name="Query">Query</param>
		/// <param name="Entity">Function to Parsing Result To Models</param>
		/// <returns>List of Models (return null if error)</returns>
		public List<T> ExecuteQuery<T>(string Query, Func<MySqlDataReader, T> Entity) where T : class
		{
			if (!IsConnected) {
				return null;
			}

			List<T> data = new List<T>();
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					MySqlCommand command = new MySqlCommand(Query, connection) { CommandTimeout = 60 };
					MySqlDataReader result = command.ExecuteReader();
					if (result.HasRows) {
						while (result.Read()) data.Add(Entity(result));
					}
					connection.Close();
				}
			} catch (MySqlException ex) {
				MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			} catch (Exception ex) {
				MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + Query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
			if (!IsConnected) return null;

			int rowaffected = 0;
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					using (var command = new MySqlCommand(Query, connection) { CommandTimeout = 60 }) {
						connection.Open();
						rowaffected = command.ExecuteNonQuery();
						connection.Close();
					}
				}
			} catch (MySqlException ex) {
				MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + ConnectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			} catch (Exception ex) {
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
			if (string.IsNullOrEmpty(connectionString)) {
				connectionString = this.ConnectionString;
				if (!IsConnected) return null;
			}
			try {
				using (var connection = new MySqlConnection(connectionString)) {
					connection.Open();
					MySqlCommand command = new MySqlCommand(Query, connection) { CommandTimeout = 60 };
					value = int.TryParse(command.ExecuteScalar().ToString(), out value) ? value : 0;
					connection.Close();
				}
			} catch (MySqlException ex) {
				MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			} catch (Exception ex) {
				MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message + "\nQuery:\n" + Query, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}
			return value;
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

			Host = Host ?? this.DatabaseHost;
			Port = Port ?? this.DatabasePort;
			Username = Username ?? this.DatabaseUsername;
			Password = Password ?? this.DatabasePassword;
			DatabaseName = DatabaseName ?? this.DatabaseName;

            string connectionString = $"datasource={Host};port={Port};username={Username};password={Password};database={DatabaseName};";

			bool isConnected = false;
			MySqlConnection connection = null;

			try {
				connection = new MySqlConnection(connectionString);
				connection.Open();
				isConnected = true;
			} catch (ArgumentException a_ex) {
				string msg = "Check the Connection String." + "\n" + 
							 "Message:" + a_ex.Message + "\n" + a_ex.ToString();
				MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

			} catch (MySqlException ex) {
				string sqlErrorMessage = "Message: " + ex.Message + "\n" +
										 "Number: " + ex.Number + "\n" + 
										 "Con String: " + connectionString;


				MessageBox.Show(sqlErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				isConnected = false;

			} finally {
				if (connection.State == ConnectionState.Open) {
					connection.Close();
				}
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
			string connectionString = $"datasource=127.0.0.1;port=3306;username={DatabaseUsername};password={DatabasePassword};";
			try {
				string queryPath = Path.GetFullPath("/Assets/Query.sql");
				using (var connection = new MySqlConnection(connectionString)) {
					MySqlScript script = new MySqlScript(connection, File.ReadAllText("Query.sql")) {
						Delimiter = "$$"
					};
					if (script.Execute() > 0) isSuccess = true;
				}
			} catch (MySqlException ex) {
				MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			} catch (Exception ex) {
				MessageBox.Show("Code : " + ex.HResult + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return isSuccess;
		}

	}
}
