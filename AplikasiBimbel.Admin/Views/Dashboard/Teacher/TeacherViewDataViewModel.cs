using System;
using System.ComponentModel;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class TeacherViewDataViewModel : INotifyPropertyChanged
    {
        #region Field

        private readonly TeacherModel _originalModel;
        private TeacherModel _mainModel;
        private bool _isNameChanged;
        private bool _isUsernameChanged;
        private bool _isAddressChanged;
        private bool _isPhoneNumberChanged;
        private bool _isPermissionChanged;
        private bool _isStatusChanged;
        
        #endregion


        #region Constructor

        public TeacherViewDataViewModel(TeacherModel teacher)
        {
            this._originalModel = new TeacherModel()
            {
                Teacher_ID = teacher.Teacher_ID,
                Name = teacher.Name,
                Username = teacher.Username,
                Password = teacher.Password,
                Address = teacher.Address,
                PhoneNumber = teacher.PhoneNumber,
                Permission = teacher.Permission,
                Status = teacher.Status,
                DateIn = teacher.DateIn,
                DateOut = teacher.DateOut
            };
            this._mainModel = teacher;
        }

        public TeacherViewDataViewModel()
        {
            _originalModel = new TeacherModel();
            _mainModel = new TeacherModel();
        }

        #endregion



        #region Properties

        public int Teacher_ID {
            get { return _mainModel.Teacher_ID; }
            set {
                throw new Exception("Changing ID is not allowed.");
            }
        }

        public string Name {
            get { return _mainModel.Name; }
            set {
                //Set Value to main model
                _mainModel.Name = value;

                //Check Is Value Changed
                if (!_originalModel.Name.Equals(value))
                    _isNameChanged = true;
                else
                    _isNameChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public string Username {
            get { return _mainModel.Username; }
            set {
                //Set Value to main model
                _mainModel.Username = value;

                //Check Is Value Changed
                if (!_originalModel.Username.Equals(value))
                    _isUsernameChanged = true;
                else
                    _isUsernameChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public string Address {
            get { return _mainModel.Address; }
            set {
                //Set Value to main model
                _mainModel.Address = value;

                //Check Is Value Changed
                if (!_originalModel.Address.Equals(value))
                    _isAddressChanged = true;
                else
                    _isAddressChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public string PhoneNumber {
            get { return _mainModel.PhoneNumber; }
            set {
                //Set Value to main model
                _mainModel.PhoneNumber = value;

                //Check Is Value Changed
                if (!_originalModel.PhoneNumber.Equals(value))
                    _isPhoneNumberChanged = true;
                else
                    _isPhoneNumberChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public int Permission {
            get {
                if (string.IsNullOrEmpty(_mainModel.Permission))
                    return -1;

                if (_mainModel.Permission.Equals("Super Admin"))
                    return 0;
                else if (_mainModel.Permission.Equals("Admin"))
                    return 1;
                else
                    return 2;
            }
            set {
                //Set Value to main model
                if (value == 0)
                    _mainModel.Permission = "Super Admin";
                else if (value == 1)
                    _mainModel.Permission = "Admin";
                else if (value == 2)
                    _mainModel.Permission = "Guru";

                //Check Is Value Changed
                if (!_originalModel.Permission.Equals(value))
                    _isPermissionChanged = true;
                else
                    _isPermissionChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Permission));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public int Status {
            get {
                if (string.IsNullOrEmpty(_mainModel.Status))
                    return -1;

                if (_mainModel.Status.Equals("Aktif"))
                    return 0;
                else
                    return 1;
            }
            set {
                //Set Value to main model
                if (value == 0)
                    _mainModel.Status = "Aktif";
                else if (value == 1)
                    _mainModel.Status = "Tidak Aktif";

                //Check Is Value Changed
                if (!_originalModel.Status.Equals(value))
                    _isStatusChanged = true;
                else
                    _isStatusChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public string DateIn {
            get { return _mainModel.DateIn.ToLongDateString(); }
        }

        public string DateOut {
            get {
                if (_mainModel.DateOut == null)
                    return "Now";

                return ((DateTime)_mainModel.DateOut).ToLongDateString();
            }
        }
        
        public bool IsChanged {
            get {
                if (_mainModel.Teacher_ID == 0)
                    return false;

                return _isNameChanged || _isUsernameChanged ||
                       _isAddressChanged || _isPhoneNumberChanged ||
                       _isPermissionChanged || _isStatusChanged;
            }
        }

        public bool IsPasswordAlreadyMade {
            get {
                return !string.IsNullOrEmpty(_mainModel.Password);
            }
        }

        public bool IsActiveTeacher {
            get {
                return Status == 0;
            }
            set {
                if (value)
                    Status = 0;
                else
                    Status = 1;
            }
        }

        public TeacherModel Teacher {
            get {
                if (_mainModel == null)
                    _mainModel = new TeacherModel();

                return _mainModel;
            }
            set {
                if (_mainModel == value)
                    return;

                _mainModel = value;

                OnPropertyChanged(nameof(Teacher));
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
}
