using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Model;
using AplikasiBimbel.ViewModel;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for MainStudentView.xaml
    /// </summary>
    public partial class MainStudentView : UserControl, INotifyPropertyChanged
    {
        #region Private Member

        private List<StudentListItemViewModel> _studentList;
        private List<StudentListItemViewModel> _originalStudentList;
        private UserControl _currentStudentPanelView;
        private StudentDataView _studentDataView;
        private StudentAccessView _studentAccessView;
        private string _studentListSortLabel;
        private string _searchKeyword;

        #endregion


        #region Constructor

        public MainStudentView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            //Read Student Data
            ReadStudentList();
        }

        #endregion


        #region Properties

        private StudentDataView StudentDataView {
            get {
                if (_studentDataView == null)
                    _studentDataView = new StudentDataView();

                return _studentDataView;
            }
        }

        private StudentAccessView StudentAccessView {
            get {
                if (_studentAccessView == null)
                    _studentAccessView = new StudentAccessView();

                return _studentAccessView;
            }
        }

        public List<StudentListItemViewModel> StudentList {
            get {
                if (_studentList == null)
                    _studentList = new List<StudentListItemViewModel>();

                return _studentList;
            }
            set {
                _studentList = value;

                OnPropertyChanged(nameof(StudentList));

                OnPropertyChanged(nameof(DataFoundLabel));
            }
        }

        public UserControl CurrentStudentPanelView {
            get {
                if (_currentStudentPanelView == null)
                    _currentStudentPanelView = new UserControl();

                return _currentStudentPanelView;
            }
            set {
                if (_currentStudentPanelView == value)
                    return;

                _currentStudentPanelView = value;

                OnPropertyChanged(nameof(CurrentStudentPanelView));

                OnPropertyChanged(nameof(IsGeneralView));
                OnPropertyChanged(nameof(IsAccessView));

            }
        }

        public string DataFoundLabel {
            get {
                return StudentList.Count.ToString() + " Student Data Found";
            }
        }

        public string SearchKeyword {
            get {
                if (_searchKeyword == null)
                    return string.Empty;

                return _searchKeyword;
            }
            set {
                _searchKeyword = value;

                //Search    
                StudentSearch(_searchKeyword);

                OnPropertyChanged(nameof(SearchKeyword));
            }
        }

        public string StudentListSortLabel {
            get {
                if (_studentListSortLabel == null)
                    return "Sort By : ID";

                return "Sort By : " + _studentListSortLabel;
            }
            set {
                _studentListSortLabel = value;

                OnPropertyChanged(nameof(StudentListSortLabel));
            }
        }

        public bool IsGeneralView {
            get {
                return CurrentStudentPanelView == StudentDataView;
            }
        }

        public bool IsAccessView {
            get {
                return CurrentStudentPanelView == StudentAccessView;
            }
        }

        #endregion


        #region Method

        private void ReadStudentList()
        {
            _originalStudentList = new List<StudentListItemViewModel>();
            StudentList = new List<StudentListItemViewModel>();
            List<StudentModel> studentList = new List<StudentModel>{
                new StudentModel
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
                },
                new StudentModel
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
                },
                new StudentModel
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
                },
                new StudentModel
                {
                    Student_ID = 4,
                    Name = "Student 4",
                    Nickname = "student4",
                    Password = "1234",
                    Address = "192.168.1.4",
                    PhoneNumber = "0819216814",
                    School = "School4",
                    Grade = "9 - SMP",
                    Status = "Aktif",
                    DateIn = System.DateTime.Parse("01/08/2019"),
                    DateOut = null,
                    AddedBy = 1,
                    IsPasswordEnabled = true
                }
            };

            foreach (var item in studentList)
            {
                StudentList.Add(new StudentListItemViewModel(item));
                _originalStudentList.Add(new StudentListItemViewModel(item));
            }

            if (StudentList.Count > 0)
                SelectStudent(StudentList[0].Student.Student_ID);
        }

        private void StudentSearch(string searchKeyword)
        {

            if (string.IsNullOrEmpty(searchKeyword))
            {
                StudentList = _originalStudentList;
                return;
            }

            List<StudentListItemViewModel> newList = new List<StudentListItemViewModel>();
            foreach (var i in _originalStudentList)
            {
                if (i.Student.Student_ID.ToString().Contains(searchKeyword)) { }
                else if (i.Student.Name.Contains(searchKeyword)) { }
                else if (i.Student.Nickname.Contains(searchKeyword)) { }
                else if (i.Student.Address.Contains(searchKeyword)) { }
                else if (i.Student.PhoneNumber.Contains(searchKeyword)) { }
                else if (i.Student.School.Contains(searchKeyword)) { }
                else if (i.Student.Grade.Contains(searchKeyword)) { }
                else if (i.Student.DateIn.ToLongDateString().Contains(searchKeyword)) { }
                else continue;

                newList.Add(i);
            }
            StudentList = newList;
        }

        private StudentModel GetStudentData(int student_ID)
        {
            foreach (var item in StudentList)
            {
                if (item.Student.Student_ID == student_ID)
                    return item.Student;
            }

            return null;
        }

        private void SelectStudent(int student_ID)
        {
            //Get Student Data From Database
            StudentModel student = GetStudentData(student_ID);

            //Check student value is not null
            if (student == null)
                return;

            //Assign to Current teacher data
            StudentDataView.CurrentStudentData= new StudentDataViewViewModel(student);

            //Set All List to Unselected
            foreach (var i in StudentList)
            {
                if (i.Student.Student_ID == student_ID)
                    i.IsSelected = true;
                else
                    i.IsSelected = false;
            }

            //Open Teacher Data View
            CurrentStudentPanelView = StudentDataView;

        }

        private void TeacherListSort(StudentSort sortBy)
        {
            List<StudentListItemViewModel> newList;

            switch (sortBy)
            {
                case StudentSort.Name:
                    newList = StudentList.OrderBy(x => x.Student.Name).ToList();
                    StudentListSortLabel = "Name";
                    break;
                case StudentSort.Nickname:
                    newList = StudentList.OrderBy(x => x.Student.Nickname).ToList();
                    StudentListSortLabel = "Nickname";
                    break;
                case StudentSort.School:
                    newList = StudentList.OrderBy(x => x.Student.School).ToList();
                    StudentListSortLabel = "School";
                    break;
                case StudentSort.Grade:
                    newList = StudentList.OrderBy(x => x.Student.Grade).ToList();
                    StudentListSortLabel = "Grade";
                    break;
                case StudentSort.Status:
                    newList = StudentList.OrderBy(x => x.Student.Status).ToList();
                    StudentListSortLabel = "Status";
                    break;
                default:
                    newList = StudentList.OrderBy(x => x.Student.Student_ID).ToList();
                    StudentListSortLabel = "ID";
                    break;
            }
            if (newList != null)
                StudentList = newList;

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



        #region StudentSelect Command

        private RelayParameterizedCommand _studentSelectCommand;

        public RelayParameterizedCommand StudentSelectCommand {
            get {
                if (_studentSelectCommand == null)
                    _studentSelectCommand = new RelayParameterizedCommand(StudentSelectCommand_Execute, StudentSelectCommand_CanExecute);

                return _studentSelectCommand;
            }
        }

        private void StudentSelectCommand_Execute(object param)
        {
            //Check param type is Teacher List Item ViewModel
            if (!(param is StudentListItemViewModel))
                return;

            //Get Teacher ID
            StudentListItemViewModel item = param as StudentListItemViewModel;
            int student_ID = item.Student.Student_ID;

            //Select The Item
            SelectStudent(student_ID);
        }

        private bool StudentSelectCommand_CanExecute(object param)
        {
            return true;
        }

        #endregion


        #region StudentSorting Command

        private RelayParameterizedCommand _studentSorting;

        public RelayParameterizedCommand StudentSorting {
            get {
                if (_studentSorting == null)
                    _studentSorting = new RelayParameterizedCommand(StudentSort_Execute, StudentSort_CanExecute);

                return _studentSorting;
            }
        }

        private void StudentSort_Execute(object param)
        {
            if (!int.TryParse(param.ToString(), out int sort))
                return;

            TeacherListSort((StudentSort)sort);
        }

        private bool StudentSort_CanExecute(object param)
        {
            return true;
        }

        #endregion


        #region ChangeTab Command

        private RelayParameterizedCommand _changeTab;

        public RelayParameterizedCommand ChangeTab {
            get {
                if (_changeTab == null)
                    _changeTab = new RelayParameterizedCommand(ChangeTab_Execute);

                return _changeTab;
            }
        }

        private void ChangeTab_Execute(object param)
        {
            if (!int.TryParse(param.ToString(), out int index))
                return;

            if (index == 0)
                CurrentStudentPanelView = StudentDataView;
            else
                CurrentStudentPanelView = StudentAccessView;
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
