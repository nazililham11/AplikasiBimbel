using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplikasiBimbel.Admin.Views.Settings
{
    /// <summary>
    /// Interaction logic for DatabaseSettingsView.xaml
    /// </summary>
    public partial class DatabaseSettingsView : UserControl
    {

        #region Private Member

        private SqlConnection connection;

        #endregion


        #region Constructor

        public DatabaseSettingsView()
        {
            InitializeComponent();
        }

        #endregion


        #region Properties

        public string ConnectionString {
            get {
                if (App.Connection == null)
                    return string.Empty;

                return App.ConnectionString;

                //return "datasource=" + App.Connection.DatabaseHost + ";" +
                //       "port=" + App.Connection.DatabasePort + ";" +
                //       "username=" + App.Connection.DatabaseUsername + ";" +
                //       "password=" + App.Connection.DatabasePassword + ";" +
                //       "database=" + App.Connection.DatabaseName + ";";
            }
        }

        #endregion


        #region Method

        private void ExecuteQuery(string Query)
        {
            DataTable Result = new DataTable();

            try
            {
                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(Query, connection);
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(Result);
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DataGrid_Result.ItemsSource = Result.DefaultView;
            DataGrid_Result.Items.Refresh();
        }

        #endregion


        #region Events

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Query.Text = string.Empty;
        }

        private void Button_ExecuteSelected_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery(TextBox_Query.SelectedText);
        }

        private void Button_Execute_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery(TextBox_Query.Text);
        }

        #endregion


       
    }
}
