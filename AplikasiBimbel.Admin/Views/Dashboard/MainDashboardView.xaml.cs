using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for MainDashboardView.xaml
    /// </summary>
    public partial class MainDashboardView : UserControl, INotifyPropertyChanged
    {

        #region Field

        private UserControl _currentView;

        private MainStudentView _studentView;
        private MainTeacherView _teacherView;
        private MainMaterialView _materialView;
        private MainReportView _reportView;

        private bool _isMenuCollapsed;

        #endregion


        #region Constructor

        public MainDashboardView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            //Reset All Views
            ResetAllViews();

            //Set Current View
            CurrentView = _teacherView;
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

                OnPropertyChanged(nameof(IsTeacherView));
                OnPropertyChanged(nameof(IsStudentView));
                OnPropertyChanged(nameof(IsMaterialView));
                OnPropertyChanged(nameof(IsReportView));
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

        #endregion


        #region Method


        private void ResetAllViews()
        {
            _studentView = new MainStudentView();
            _teacherView = new MainTeacherView();
            _materialView = new MainMaterialView();
            _reportView = new MainReportView();
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
