using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        private bool _isEditMode;

        #endregion

        #region Constructor

        public TeacherDataView()
        {
            InitializeComponent();

            //Set the Data Context
            this.DataContext = this;


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

        public TeacherViewDataViewModel CurrentTeacherData {
            get {
                if (_currentTeacherData == null)
                    _currentTeacherData = new TeacherViewDataViewModel();

                return _currentTeacherData;
            }
            set {
                if (_currentTeacherData == value)
                    return;

                IsEditMode = false;

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

            IsEditMode = false;
        }

        private bool TeacherSaveChanges_CanExecute(object param)
        {
            return CurrentTeacherData.IsChanged;
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
            MessageBox.Show("Password Reset has been Successfully");
        }

        private bool TeacherResetPassword_CanExecute(object param)
        {
            return CurrentTeacherData.IsPasswordAlreadyMade;
        }

        #endregion


        #region TeacherEdit Command

        private RelayParameterizedCommand _teacherEdit;

        public RelayParameterizedCommand TeacherEdit {
            get {
                if (_teacherEdit == null)
                 _teacherEdit = new RelayParameterizedCommand(TeacherEdit_Execute, TeacherEdit_CanExecute);

                return _teacherEdit;
            }
        }

        private void TeacherEdit_Execute(object param)
        {
            IsEditMode = true;
        }

        private bool TeacherEdit_CanExecute(object param)
        {
            if (App.TeacherSession == null)
                return false;

            bool IsSuperAdmin = CurrentTeacherData.PermissionIndex == (int)TeacherPermission.Super_Admin;

            bool IsCurrentSessionHasSuperAdmin = App.TeacherSession.Permission.Equals("Super Admin");

            return !IsEditMode && (!IsSuperAdmin || IsCurrentSessionHasSuperAdmin);
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
