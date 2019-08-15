using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.View.Dashboard
{
    /// <summary>
    /// Interaction logic for MainTeacherView.xaml
    /// </summary>
    public partial class MainTeacherView : UserControl, INotifyPropertyChanged
    {
        #region Field

        private List<TeacherViewModel> _teacherList;

        #endregion


        #region Constructor

        public MainTeacherView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            
        }

        #endregion

        #region Properties

        public List<TeacherViewModel> TeacherList {
            get {
                if (_teacherList == null)
                    _teacherList = new List<TeacherViewModel>();

                return _teacherList;
            }
            set {
                _teacherList = value;
                OnPropertyChanged(nameof(TeacherList));
            }
        }
        #endregion


        #region Method

        private void ReadTeacherList()
        {
            TeacherList = new List<TeacherViewModel>();
            List<TeacherModel> teacherList = App.TeacherController.ReadAll() ?? new List<TeacherModel>();

            foreach (var item in teacherList)
                TeacherList.Add(new TeacherViewModel(item));
        }

        #endregion


        #region Event

        private void TeacherView_Loaded(object sender, RoutedEventArgs e)
        {
            if (TeacherList.Count < 1)
                //Read Taecher
                ReadTeacherList();
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
