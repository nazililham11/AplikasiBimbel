using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using AplikasiBimbel.ViewModel;
using AplikasiBimbel.Admin.Views.Dashboard;
using AplikasiBimbel.Admin.Views.Settings;
using AplikasiBimbel.Admin.ViewModel;
using MaterialDesignThemes.Wpf;

namespace AplikasiBimbel.Admin.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Field

        private UserControl _currentView;

        private MainStudentView _studentView;
        private MainTeacherView _teacherView;
        private MainMaterialView _materialView;
        private MainReportView _reportView;
        private LoginView _loginView;
        private MainSettingsView _settingsView;

        private bool _isMenuCollapsed;

        #endregion
        

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            //Reset All Views
            ResetAllViews();

            //Set Current View
            CurrentView = _loginView;

            //Set Menu To Collapsed
            IsMenuCollapsed = true;

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
                if (_currentView == value)
                    return;

                _currentView = value;

                //Notify The Current View
                OnPropertyChanged(nameof(CurrentView));

                //And The Other Views
                OnPropertyChanged(nameof(IsTeacherView));
                OnPropertyChanged(nameof(IsStudentView));
                OnPropertyChanged(nameof(IsMaterialView));
                OnPropertyChanged(nameof(IsReportView));
                OnPropertyChanged(nameof(IsLoginView));
                OnPropertyChanged(nameof(IsSettingsView));
            }
        }

        public bool IsTeacherView {
            get { return CurrentView == _teacherView; }
        }

        public bool IsStudentView {
            get { return CurrentView == _studentView; }
        }

        public bool IsMaterialView {
            get { return CurrentView == _materialView; }
        }

        public bool IsReportView {
            get { return CurrentView == _reportView; }
        }

        public bool IsMenuCollapsed {
            get { return _isMenuCollapsed; }
            set {
                _isMenuCollapsed = value;

                OnPropertyChanged(nameof(IsMenuCollapsed));
            }
        }

        public bool IsLoginView {
            get { return CurrentView == _loginView; }
        }

        public bool IsSettingsView {
            get { return CurrentView == _settingsView; }
        }

        public bool IsLoggedin {
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
            if (IsLoggedin)
            {
                ResetAllViews();
                CurrentView = _teacherView;
            }

            OnPropertyChanged(nameof(IsLoggedin));
        }

        /// <summary>
        /// Callback Set App.Teacher to null and Show the login page
        /// </summary>
        public void LogoutCallback()
        {
            App.TeacherSession = null;
            ResetAllViews();

            CurrentView = _loginView;

            OnPropertyChanged(nameof(IsLoggedin));
        }

        #endregion


        private void ResetAllViews()
        {
            _studentView = new MainStudentView();
            _teacherView = new MainTeacherView();
            _materialView = new MainMaterialView();
            _reportView = new MainReportView();
            _loginView = new LoginView();
            _settingsView = new MainSettingsView();
        }

        #endregion




        #region Events

        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            IsMenuCollapsed = !IsMenuCollapsed;
        }

        private void Button_Teacher_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _teacherView;
        }

        private void Button_Student_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _studentView;
        }

        private void Button_Material_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _materialView;
        }

        private void Button_Report_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _reportView;
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _loginView;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = _settingsView;
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show("Are You Sure Want to Logout?", "Logout",
                                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.Yes)
                LogoutCallback();
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
    public class MenuItemViewModel : INotifyPropertyChanged
    {


        #region Private Member

        private UserControl _view;
        private PackIconKind _icon;
        private string _title;
        private bool _IsSelected;
        private string _tooltip;

        #endregion


        #region Properties

        public UserControl View {
            get {
                if (_view == null)
                    _view = new UserControl();

                return _view;
            }
            set {
                _view = value;

                OnPropertyChanged(nameof(View));
            }
        }

        public PackIconKind Icon {
            get { return _icon; }
            set {
                _icon = value;

                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Title {
            get {
                if (_title == null)
                    _title = string.Empty;

                return _title;
            }
            set {
                _title = value;

                OnPropertyChanged(nameof(Title));
            }
        }

        public bool IsSelected {
            get { return _IsSelected; }
            set {
                _IsSelected = value;

                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string Tooltip {
            get {
                if (_tooltip == null)
                    _tooltip = string.Empty;

                return _tooltip;
            }
            set {
                _tooltip = value;

                OnPropertyChanged(nameof(Tooltip));
            }
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

    public class MainMenuDesignModel
    {
        #region Singleton

        public static MainMenuDesignModel Instance => new MainMenuDesignModel();

        #endregion


        #region Constructor

        public MainMenuDesignModel()
        {
            MainMenus = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel
                {
                    Title = "Manage Database",
                    Icon = PackIconKind.Database,
                    IsSelected = false,
                    View = new DatabaseSettingsView()
                },
                new MenuItemViewModel
                {
                    Title = "Manage Connection",
                    Icon = PackIconKind.LanConnect,
                    IsSelected = false,
                    View = new ConnectionSettings()
                }
            };
        }

        #endregion


        #region Properties

        public List<MenuItemViewModel> MainMenus { get; set; }

        #endregion
    }

}
