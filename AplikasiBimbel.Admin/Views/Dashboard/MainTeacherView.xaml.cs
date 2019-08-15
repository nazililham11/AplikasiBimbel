using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
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

            ReadTeacherList();
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
            List<TeacherModel> teacherList = new List<TeacherModel>();

            teacherList.Add(new TeacherModel()
            {
                Teacher_ID = 1,
                Name = "Teacher 1",
                Username = "teacher1",
                Password = "1234",
                Address = "192.168.1.1",
                PhoneNumber = "0819216811",
                Permission = "Super Admin",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null
            });
            teacherList.Add(new TeacherModel()
            {
                Teacher_ID = 2,
                Name = "Teacher 2",
                Username = "teacher2",
                Password = "1234",
                Address = "192.168.1.2",
                PhoneNumber = "0819216812",
                Permission = "Admin",
                Status = "Tidak Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = System.DateTime.Parse("01/07/2019")
            });
            teacherList.Add(new TeacherModel()
            {
                Teacher_ID = 3,
                Name = "Teacher 3",
                Username = "teacher3",
                Password = "1234",
                Address = "192.168.1.3",
                PhoneNumber = "0819216813",
                Permission = "Guru",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null
            });
            teacherList.Add(new TeacherModel()
            {
                Teacher_ID = 4,
                Name = "Teacher 4",
                Username = "teacher4",
                Password = "1234",
                Address = "192.168.1.4",
                PhoneNumber = "0819216814",
                Permission = "Guru",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null
            });


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
