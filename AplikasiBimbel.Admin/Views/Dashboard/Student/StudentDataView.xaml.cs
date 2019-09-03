using AplikasiBimbel.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for StudentDataView.xaml
    /// </summary>
    public partial class StudentDataView : UserControl, INotifyPropertyChanged
    {
        #region Private Member

        private StudentDataViewViewModel _currentStudentData;
        private bool _isEditMode;

        #endregion

        #region Constructor

        public StudentDataView()
        {
            InitializeComponent();

            //Set the Data Context
            this.DataContext = this;

            IsEditMode = false;
        }

        #endregion


        #region Properties

        public bool IsEditMode {
            get { return _isEditMode; }
            set {
                _isEditMode = value;

                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        public StudentDataViewViewModel CurrentStudentData {
            get {
                if (_currentStudentData == null)
                    _currentStudentData = new StudentDataViewViewModel();

                return _currentStudentData;
            }
            set {
                if (_currentStudentData == value)
                    return;

                IsEditMode = false;

                _currentStudentData = value;

                OnPropertyChanged(nameof(CurrentStudentData));
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

            IsEditMode = false;
        }

        private bool TeacherSaveChanges_CanExecute(object param)
        {
            return CurrentStudentData.IsChanged;
        }

        #endregion


        #region StudentResetPassword Command

        private RelayParameterizedCommand _studentResetPassword;

        public RelayParameterizedCommand StudentResetPassword {
            get {
                if (_studentResetPassword == null)
                    _studentResetPassword = new RelayParameterizedCommand(StudentResetPassword_Execute, StudentResetPassword_CanExecute);

                return _studentResetPassword;
            }
        }

        private void StudentResetPassword_Execute(object param)
        {
            MessageBox.Show("Password Reset has been Successfully");
        }

        private bool StudentResetPassword_CanExecute(object param)
        {
            return CurrentStudentData.IsPasswordAlreadyMade;
        }

        #endregion


        #region StudentEdit Command

        private RelayParameterizedCommand _studentEdit;

        public RelayParameterizedCommand StudentEdit {
            get {
                if (_studentEdit == null)
                    _studentEdit = new RelayParameterizedCommand(StudentEdit_Execute);

                return _studentEdit;
            }
        }

        private void StudentEdit_Execute(object param)
        {
            IsEditMode = true;
        }


        #endregion


        #region CancelEdit Command

        private RelayParameterizedCommand _cancelEdit;

        public RelayParameterizedCommand CancelEdit {
            get {
                if (_cancelEdit == null)
                    _cancelEdit = new RelayParameterizedCommand(CancelEdit_Execute);

                return _cancelEdit;
            }
        }

        private void CancelEdit_Execute(object param)
        {
            IsEditMode = false;
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
