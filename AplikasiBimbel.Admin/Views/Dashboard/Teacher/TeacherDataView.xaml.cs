using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Controller;
using AplikasiBimbel.Model;
using AplikasiBimbel.ViewModel;

namespace AplikasiBimbel.Admin.Views.Dashboard
{

    /// <summary>
    /// Interaction logic for TeacherDataView.xaml
    /// </summary>
    public partial class TeacherDataView : UserControl, INotifyPropertyChanged
    {
        #region Private Member

        private TeacherViewDataViewModel _currentTeacherData;
        private OpenEditControlArgs _args;
        private TeacherController _teacherDatabase;

        #endregion

        #region Constructor

        public TeacherDataView(OpenEditControlArgs args)
        {

            InitializeComponent();

            //Set the Data Context
            this.DataContext = this;

            _teacherDatabase = new TeacherController();

            this._args = args;

            if (args == OpenEditControlArgs.Add)
                PrepareNewTeacher();

        }

        #endregion


        #region Properties

        public TeacherViewDataViewModel CurrentTeacherData {
            get {
                if (_currentTeacherData == null)
                    _currentTeacherData = new TeacherViewDataViewModel();

                return _currentTeacherData;
            }
            set {
                if (_currentTeacherData == value)
                    return;

                _currentTeacherData = value;

                OnPropertyChanged(nameof(CurrentTeacherData));
            }
        }

        #endregion


        #region TeacherSaveChanges Command

        private RelayParameterizedCommand _teacherSaveChanges = null;

        public RelayParameterizedCommand TeacherSaveChanges {
            get {
                if (_teacherSaveChanges == null)
                    _teacherSaveChanges = new RelayParameterizedCommand(TeacherSaveChanges_Execute, TeacherSaveChanges_CanExecute);

                return _teacherSaveChanges;
            }
        }

        private void TeacherSaveChanges_Execute(object param)
        {
            MessageBox.Show("Save Changes Successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool TeacherSaveChanges_CanExecute(object param)
        {
            return CurrentTeacherData.IsChanged;
        }

        #endregion

        #region Method

        private void PrepareNewTeacher()
        {

            int newID = _teacherDatabase.GenerateNewID();

            if (newID < 1)
                return;
            
            TeacherModel teacher = new TeacherModel
            {
                Teacher_ID = newID,
                Username = string.Empty,
                Password = string.Empty,
                Name = string.Empty,
                Address = string.Empty,
                PhoneNumber = string.Empty,
                Permission = "Teacher",
                Status = "Aktif",
                DateIn = DateTime.Now,
                DateOut = null
            };

            CurrentTeacherData = new TeacherViewDataViewModel(teacher);

        }

        private bool ResetPassword()
        {
            if (CurrentTeacherData.Teacher_ID < 1)
                return false;

            TeacherModel teacher = _teacherDatabase.Find(CurrentTeacherData.Teacher_ID);

            if (teacher == null)
                return false;

            teacher.Password = null;

            return _teacherDatabase.Update(teacher);
        }

        #endregion

        #region TeacherResetPassword Command

        private RelayParameterizedCommand _TeacherResetPassword = null;

        public RelayParameterizedCommand TeacherResetPassword {
            get {
                if (_TeacherResetPassword == null)
                    _TeacherResetPassword = new RelayParameterizedCommand(TeacherResetPassword_Execute, TeacherResetPassword_CanExecute);

                return _TeacherResetPassword;
            }
        }

        private void TeacherResetPassword_Execute(object param)
        {
            if (ResetPassword())
                MessageBox.Show("Password Reset has been Successfully");
            else
                MessageBox.Show("Error occured, Reset Password failed");
        }

        private bool TeacherResetPassword_CanExecute(object param)
        {
            return CurrentTeacherData.IsPasswordAlreadyMade;
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
