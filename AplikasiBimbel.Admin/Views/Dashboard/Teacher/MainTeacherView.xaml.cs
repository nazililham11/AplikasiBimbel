using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using AplikasiBimbel.Controller;
using AplikasiBimbel.Model;
using AplikasiBimbel.ViewModel;
using AplikasiBimbel.Admin.ViewModel;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for MainTeacherView.xaml
    /// </summary>
    public partial class MainTeacherView : UserControl, INotifyPropertyChanged
    {
        #region Private Member

        private TeacherController _teacherDatabase;

        private List<TeacherListItemViewModel> _originalTeacherList;
        private List<TeacherListItemViewModel> _teacherList;
        private UserControl _currentTeacherPanelView;
        private TeacherDataView _teacherDataView;
        private TeacherLogView _teacherLogView;
        private string _teacherListSortLabel;
        private string _searchKeyword;

        #endregion


        #region Constructor

        public MainTeacherView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            _teacherDatabase = new TeacherController();


        }

        #endregion


        #region Properties


        public TeacherDataView TeacherDataView {
            get {
                if (_teacherDataView == null)
                    _teacherDataView = new TeacherDataView(OpenEditControlArgs.Edit);

                return _teacherDataView;
            }
            set {
                _teacherDataView = value;

                OnPropertyChanged(nameof(TeacherDataView));
            }
        }

        private TeacherLogView TeacherLogView {
            get {
                if (_teacherLogView == null)
                    _teacherLogView = new TeacherLogView();

                return _teacherLogView;
            }
        }

        public List<TeacherListItemViewModel> TeacherList {
            get {
                if (_teacherList == null)
                    _teacherList = new List<TeacherListItemViewModel>();

                return _teacherList;
            }
            set {
                _teacherList = value;

                OnPropertyChanged(nameof(TeacherList));

                OnPropertyChanged(nameof(DataFoundLabel));
            }
        }

        public UserControl CurrentTeacherPanelView {
            get {
                if (_currentTeacherPanelView == null)
                    _currentTeacherPanelView = new UserControl();

                return _currentTeacherPanelView;
            }
            set {
                if (_currentTeacherPanelView == value)
                    return;

                _currentTeacherPanelView = value;

                OnPropertyChanged(nameof(CurrentTeacherPanelView));

                OnPropertyChanged(nameof(IsGeneralView));
                OnPropertyChanged(nameof(IsLogView));

            }
        }
         
        public string DataFoundLabel {
            get {
                return TeacherList.Count.ToString() + " Teacher Data Found";
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
                TeacherSearch(_searchKeyword);

                OnPropertyChanged(nameof(SearchKeyword));
            }
        }

        public string TeacherListSortLabel {
            get {
                if (_teacherListSortLabel == null)
                    return "ID";

                return _teacherListSortLabel;
            }
            set {
                _teacherListSortLabel = value;

                OnPropertyChanged(nameof(TeacherListSortLabel));
            }
        }

        public bool IsGeneralView {
            get {
                return CurrentTeacherPanelView == TeacherDataView;
            }
        }

        public bool IsLogView {
            get {
                return CurrentTeacherPanelView == TeacherLogView;
            }
        }

        public bool IsCurrentSessionHasSuperAdmin {
            get {
                if (App.TeacherSession == null)
                    return false;

                return App.TeacherSession.Permission.Equals("Super Admin");
            }
        }

        public bool IsTeacherSelected {
            get {
                return TeacherDataView.CurrentTeacherData != null;
            }
        }

        #endregion


        #region Method

        private void TeacherSearch(string searchKeyword)
        {
            if (TeacherList.Count < 1)
                return;

            if (string.IsNullOrEmpty(searchKeyword))
            {
                TeacherList = _originalTeacherList;
                return;
            }

            List<TeacherListItemViewModel> newList = new List<TeacherListItemViewModel>();

            foreach (var i in _originalTeacherList)
            {
                if (i.Teacher.Teacher_ID.ToString().Contains(searchKeyword)) { }
                else if (i.Teacher.Name.Contains(searchKeyword)) { }
                else if (i.Teacher.Username.Contains(searchKeyword)) { }
                else if (i.Teacher.Address.Contains(searchKeyword)) { }
                else if (i.Teacher.PhoneNumber.Contains(searchKeyword)) { }
                else if (i.Teacher.DateIn.ToLongDateString().Contains(searchKeyword)) { }
                else continue;

                newList.Add(i);
            }
            TeacherList = newList;
        }

        private void ReadTeacherList()
        {


            #region Debug

            //List<TeacherModel> teacherList = new List<TeacherModel>
            //{
            //    new TeacherModel()
            //    {
            //        Teacher_ID = 1,
            //        Name = "Administrator",
            //        Username = "administrator",
            //        Password = "1234",
            //        Address = "Lebo",
            //        PhoneNumber = "0819216811",
            //        Permission = "Super Admin",
            //        Status = "Aktif",
            //        DateIn = System.DateTime.Parse("01/08/2019"),
            //        DateOut = null
            //    },
            //    new TeacherModel()
            //    {
            //        Teacher_ID = 2,
            //        Name = "Nazil Ilham Buthanudin",
            //        Username = "nazililham",
            //        Password = null,
            //        Address = "Yogyakarta sleman",
            //        PhoneNumber = "0819216812",
            //        Permission = "Admin",
            //        Status = "Tidak Aktif",
            //        DateIn = System.DateTime.Parse("01/08/2019"),
            //        DateOut = System.DateTime.Parse("01/07/2019")
            //    },
            //    new TeacherModel()
            //    {
            //        Teacher_ID = 3,
            //        Name = "Ainun Nur Mazidah",
            //        Username = "ainun",
            //        Password = "1234",
            //        Address = "Sidoarjo",
            //        PhoneNumber = "0819216813",
            //        Permission = "Guru",
            //        Status = "Aktif",
            //        DateIn = System.DateTime.Parse("01/08/2019"),
            //        DateOut = null
            //    },
            //    new TeacherModel()
            //    {
            //        Teacher_ID = 4,
            //        Name = "Teacher 4",
            //        Username = "teacher4",
            //        Password = "1234",
            //        Address = "192.168.1.4",
            //        PhoneNumber = "0819216814",
            //        Permission = "Guru",
            //        Status = "Aktif",
            //        DateIn = System.DateTime.Parse("01/08/2019"),
            //        DateOut = null
            //    }
            //};

            #endregion

            _originalTeacherList = new List<TeacherListItemViewModel>();
            List<TeacherModel> teachers = _teacherDatabase.ReadAll();
            List<TeacherListItemViewModel> teacherList = new List<TeacherListItemViewModel>();

            if (teachers == null)
                return;

            foreach (var item in teachers)
            {
                teacherList.Add(new TeacherListItemViewModel(item));
                _originalTeacherList.Add(new TeacherListItemViewModel(item));
            }

            TeacherList = teacherList;

            if (teacherList.Count > 0)
                SelectTeacher(TeacherList[0].Teacher.Teacher_ID);
        }

        private void SelectTeacher(int teacher_ID)
        {
            //Check Selected Item
            if (teacher_ID == TeacherDataView.CurrentTeacherData.Teacher_ID)
                return;

            //Get Teacher Data From Database
            TeacherModel teacher = _teacherDatabase.Find(teacher_ID);

            //Check teacher value is not null
            if (teacher == null)
                return;

            //Assign to Current teacher data
            TeacherDataView.CurrentTeacherData = new TeacherViewDataViewModel(teacher);

            //Set All List to Unselected
            foreach (var i in TeacherList)
            {
                if (i.Teacher.Teacher_ID == teacher_ID)
                    i.IsSelected = true;
                else 
                    i.IsSelected = false;
            }
            
            //Open Teacher Data View
            CurrentTeacherPanelView = TeacherDataView;

            //Notify the Is Teacher Selected Property
            OnPropertyChanged(nameof(IsTeacherSelected));

        }

        private void TeacherListSort(TeacherSort sortBy)
        {
            List<TeacherListItemViewModel> newList;

            switch (sortBy)
            {
                case TeacherSort.Name:
                    newList = TeacherList.OrderBy(x => x.Teacher.Name).ToList();
                    TeacherListSortLabel = "Name";
                    break;
                case TeacherSort.Username:
                    newList = TeacherList.OrderBy(x => x.Teacher.Username).ToList();
                    TeacherListSortLabel = "Username";
                    break;
                case TeacherSort.Access:
                    newList = TeacherList.OrderBy(x => x.Teacher.Permission).ToList();
                    TeacherListSortLabel = "Permission";
                    break;
                case TeacherSort.Status:
                    newList = TeacherList.OrderBy(x => x.Teacher.Status).ToList();
                    TeacherListSortLabel = "Status";
                    break;
                default:
                    newList = TeacherList.OrderBy(x => x.Teacher.Teacher_ID).ToList();
                    TeacherListSortLabel = "ID";
                    break;
            }
            if (newList != null)
                TeacherList = newList;

        }

        #endregion


        #region TeacherSelect Command

        private RelayParameterizedCommand _teacherSelectCommand;

        public RelayParameterizedCommand TeacherSelectCommand {
            get {
                if (_teacherSelectCommand == null)
                    _teacherSelectCommand = new RelayParameterizedCommand(TeacherSelectCommand_Execute); 

                return _teacherSelectCommand;
            }
        }

        private void TeacherSelectCommand_Execute(object param)
        {
            //Check param type is Teacher List Item ViewModel
            if (!(param is TeacherListItemViewModel))
                return;

            //Get Teacher ID
            TeacherListItemViewModel item = param as TeacherListItemViewModel;
            int teacher_ID = item.Teacher.Teacher_ID;

            //Select The Item
            SelectTeacher(teacher_ID);
        }

        #endregion


        #region TeacherSorting Command

        private RelayParameterizedCommand _teacherSorting;

        public RelayParameterizedCommand TeacherSorting {
            get {
                if (_teacherSorting == null)
                    _teacherSorting = new RelayParameterizedCommand(TeacherSort_Execute); 

                return _teacherSorting;
            }
        }

        private void TeacherSort_Execute(object param)
        {
            if (!int.TryParse(param.ToString(), out int sort))
                return;

            TeacherListSort((TeacherSort)sort);
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
                CurrentTeacherPanelView = TeacherDataView;
            else
                CurrentTeacherPanelView = TeacherLogView;
        }

        #endregion



        #region AddNewTeacher Command

        private RelayCommand _addNewTeacher = null;

        public RelayCommand AddNewTeacher {
            get {
                if (_addNewTeacher == null)
                    _addNewTeacher = new RelayCommand(AddNewTeacher_Execute);

                return _addNewTeacher;
            }
        }

        private void AddNewTeacher_Execute()
        {
            TeacherDataView = new TeacherDataView(OpenEditControlArgs.Add);
            CurrentTeacherPanelView = TeacherDataView;
        }

        #endregion



        #region Event

        private void TeacherView_Loaded(object sender, RoutedEventArgs e)
        {

            //Read Taecher
            if (TeacherList.Count < 1)
                ReadTeacherList();

            Textbox_Search.Focus();
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
