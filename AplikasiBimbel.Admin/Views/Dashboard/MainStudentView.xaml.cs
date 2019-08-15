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
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for MainStudentView.xaml
    /// </summary>
    public partial class MainStudentView : UserControl, INotifyPropertyChanged
    {
        #region Field

        private List<StudentViewModel> _studentList;

        #endregion


        #region Constructor

        public MainStudentView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            ReadStudentList();
        }

        #endregion


        #region Properties

        public List<StudentViewModel> StudentList {
            get {
                if (_studentList == null)
                    _studentList = new List<StudentViewModel>();

                return _studentList;
            }
            set {
                _studentList = value;
                OnPropertyChanged(nameof(StudentList));
            }
        }
        #endregion


        #region Method

        private void ReadStudentList()
        {
            StudentList = new List<StudentViewModel>();
            List<StudentModel> studentList = new List<StudentModel>();

            studentList.Add(new StudentModel()
            {
                Student_ID = 1,
                Name = "Student 1",
                Nickname = "student1",
                Password = "1234",
                Address = "192.168.1.1",
                PhoneNumber = "0819216811",
                School = "School1",
                Grade = "6 - SD",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null,
                AddedBy = 1,
                IsPasswordEnabled = false
            });
            studentList.Add(new StudentModel()
            {
                Student_ID = 2,
                Name = "Student 2",
                Nickname = "student2",
                Password = "1234",
                Address = "192.168.1.2",
                PhoneNumber = "0819216812",
                School = "School2",
                Grade = "7 - SMP",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("02/08/2019"),
                DateOut = null,
                AddedBy = 1,
                IsPasswordEnabled = false
            });
            studentList.Add(new StudentModel()
            {
                Student_ID = 1,
                Name = "Student 1",
                Nickname = "student1",
                Password = "1234",
                Address = "192.168.1.1",
                PhoneNumber = "0819216811",
                School = "School",
                Grade = "6 - SD",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null,
                AddedBy = 1,
                IsPasswordEnabled = false
            });
            studentList.Add(new StudentModel()
            {
                Student_ID = 3,
                Name = "Student 3",
                Nickname = "student3",
                Password = "1234",
                Address = "192.168.1.3",
                PhoneNumber = "0819216813",
                School = "School3",
                Grade = "8 - SMP",
                Status = "Tidak Aktif",
                DateIn = System.DateTime.Parse("03/08/2019"),
                DateOut = System.DateTime.Parse("30/08/2019"),
                AddedBy = 2,
                IsPasswordEnabled = false
            });
            studentList.Add(new StudentModel()
            {
                Student_ID = 4,
                Name = "Student 4",
                Nickname = "student4",
                Password = "1234",
                Address = "192.168.1.4",
                PhoneNumber = "0819216814",
                School = "School4",
                Grade = "9 - SMp",
                Status = "Aktif",
                DateIn = System.DateTime.Parse("01/08/2019"),
                DateOut = null,
                AddedBy = 1,
                IsPasswordEnabled = true
            });


            foreach (var item in studentList)
                StudentList.Add(new StudentViewModel(item));
        }

        #endregion


        #region Event

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (StudentList.Count < 1)
                //Read Taecher
                ReadStudentList();
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
