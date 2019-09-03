using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Controller;
using AplikasiBimbel.Model;
using AplikasiBimbel.ViewModel;
using AplikasiBimbel.Properties;

namespace AplikasiBimbel.Admin.Views.Settings
{
    /// <summary>
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : UserControl, INotifyPropertyChanged
    {

        #region Private Member

        private RelayParameterizedCommand _edit;
        private RelayParameterizedCommand _testConnection;
        private RelayParameterizedCommand _cancel;
        private RelayParameterizedCommand _save;
        private ConnectionModel _currentConnection;
        private string _connectionString;
        private bool _isEditMode;

        #endregion


        #region Contructor

        public ConnectionSettings()
        {
            InitializeComponent();

            this.DataContext = this;

        }

        #endregion


        #region Properties

        public RelayParameterizedCommand Edit {
            get {
                if (_edit == null)
                    _edit = new RelayParameterizedCommand(Edit_Execute);

                return _edit;
            }
        }

        public RelayParameterizedCommand TestConnection {
            get {
                if (_testConnection == null)
                    _testConnection = new RelayParameterizedCommand(TestConnection_Execute);

                return _testConnection;
            }
        }

        public RelayParameterizedCommand Cancel {
            get {
                if (_cancel == null)
                    _cancel = new RelayParameterizedCommand(Cancel_Execute);

                return _cancel;
            }
        }

        public RelayParameterizedCommand Save {
            get {
                if (_save == null)
                    _save = new RelayParameterizedCommand(Save_Execute);

                return _save;
            }
        }

        public ConnectionModel CurrentConnection {
            get {
                if (_currentConnection == null)
                    _currentConnection = App.Connection;

                return _currentConnection;
            }
            set {
                _currentConnection = value;

                OnPropertyChanged(nameof(CurrentConnection));
            }
        }

        public bool IsEditMode {
            get { return _isEditMode; }
            set {
                _isEditMode = value;

                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        public string ConnectionString {
            get {
                if (_connectionString == null)
                    _connectionString = App.ConnectionString;

                return _connectionString;
            }
            set {
                _connectionString = value;

                OnPropertyChanged(nameof(ConnectionString));
            }
        }

        #endregion


        #region Method

        private void Edit_Execute(object param)
        {
            IsEditMode = true;
        }

        private void Cancel_Execute(object param)
        {
            IsEditMode = false;
        }

        private void TestConnection_Execute(object param)
        {
            MSSQL_Connection_old connection = new MSSQL_Connection_old(CurrentConnection);

            if (connection.CheckDatabaseServer(ConnectionString))
            {
                MessageBox.Show("Database Connected");
            }
            else
            {
                MessageBox.Show("Unable to Connect to Database");
            }
        }

        private void Save_Execute(object param)
        {
            App.ConnectionString = ConnectionString;
        }

        #endregion
    

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
