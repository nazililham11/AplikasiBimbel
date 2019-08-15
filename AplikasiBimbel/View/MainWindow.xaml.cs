using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.View.Dashboard;
using AplikasiBimbel.View.Settings;

namespace AplikasiBimbel.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region Field

        private UserControl _currentView;

        private LoginView _loginView;
        private MainDashboardView _dashboardView;
        private MainSettingsView _settingsView;

        #endregion


        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            //Set Binding Data Context
            this.DataContext = this;

            //Reset All Views
            ResetAllViews();

            //Show Login Page
            CurrentView = _loginView;
        }

        #endregion


        #region Properties

        public UserControl CurrentView {
            get {
                if (_currentView == null)
                    _currentView = new UserControl();

                return _currentView;
            }
            set {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));

                OnPropertyChanged(nameof(IsLoginView));
                OnPropertyChanged(nameof(IsDashboardView));
                OnPropertyChanged(nameof(IsSettingsView));
            }
        }

        public bool IsLoginView {
            get { return CurrentView == _loginView; }
        }

        public bool IsDashboardView {
            get { return CurrentView == _dashboardView; }
        }

        public bool IsSettingsView {
            get { return CurrentView == _settingsView; }
        }

        public bool IsLoggedIn {
            get { return App.TeacherSession != null; }
        }


        #endregion


        #region Method

        #region CallBack Method

        /// <summary>
        /// Callback for open Main Menu After Login Success
        /// </summary>
        public void LoginCallback()
        {
            if (IsLoggedIn) {
                ResetAllViews();
                CurrentView = _dashboardView;
            }

            OnPropertyChanged(nameof(IsLoggedIn));
        }

        /// <summary>
        /// Callback Set App.Teacher to null and Show the login page
        /// </summary>
        public void LogoutCallback()
        {
            App.TeacherSession = null;
            ResetAllViews();

            CurrentView = _loginView;

            OnPropertyChanged(nameof(IsLoggedIn));
        }

        #endregion

        private void ResetAllViews()
        {
            _loginView = new LoginView();
            _dashboardView = new MainDashboardView();
            _settingsView = new MainSettingsView();
        }

        #endregion


        #region Events 

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _loginView;
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show("Are You Sure Want to Logout?", "Logout",
                                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.Yes)
                LogoutCallback();
        }

        private void Button_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _dashboardView;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _settingsView;
        }

        #endregion


        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #endregion

       
    }
}
