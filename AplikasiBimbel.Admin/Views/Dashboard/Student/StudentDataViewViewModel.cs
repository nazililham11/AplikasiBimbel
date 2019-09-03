using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class StudentDataViewViewModel : INotifyPropertyChanged
    {
        #region Field

        private readonly StudentModel _originalModel;
        private StudentModel _mainModel;
        private bool _isNameChanged;
        private bool _isNicknameChanged;
        private bool _isAddressChanged;
        private bool _isPhoneNumberChanged;
        private bool _isGradeChanged;
        private bool _isSchoolChanged;
        private bool _isStatusChanged;

        #endregion


        #region Constructor

        public StudentDataViewViewModel(StudentModel student)
        {
            this._originalModel = new StudentModel()
            {
                Student_ID = student.Student_ID,
                Name = student.Name,
                Nickname = student.Nickname,
                Password = student.Password,
                School = student.School,
                Grade = student.Grade,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                IsPasswordEnabled = student.IsPasswordEnabled,
                Status = student.Status,
                AddedBy = student.AddedBy,
                DateIn = student.DateIn,
                DateOut = student.DateOut

            };
            this._mainModel = student;
        }

        public StudentDataViewViewModel()
        {
            this._originalModel = new StudentModel();
            this._mainModel = new StudentModel();
        }

        #endregion


        #region Properties

        public StudentModel Student {
            get {
                if (_mainModel == null)
                    _mainModel = new StudentModel();

                return _mainModel;
            }
            set {
                if (_mainModel == value)
                    return;

                _mainModel = value;

                OnPropertyChanged(nameof(Student));
            }
        }

        public int Student_ID {
            get { return _mainModel.Student_ID; }
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

        public string Nickname {
            get { return _mainModel.Nickname; }
            set {
                //Set Value to main model
                _mainModel.Nickname = value;

                //Check Is Value Changed
                if (!_originalModel.Nickname.Equals(value))
                    _isNicknameChanged = true;
                else
                    _isNicknameChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Nickname));
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

        public string School {
            get { return _mainModel.School; }
            set {
                //Set Value to main model
                _mainModel.School = value;

                //Check Is Value Changed
                if (!_originalModel.School.Equals(value))
                    _isSchoolChanged = true;
                else
                    _isSchoolChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(School));
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public int Grade {
            get {
                if (string.IsNullOrEmpty(_mainModel.Grade))
                    return -1;

                switch (_mainModel.Grade)
                {
                    case "TK": return 0;
                    case "1 - SD": return 1;
                    case "2 - SD": return 2;
                    case "3 - SD": return 3;
                    case "4 - SD": return 4;
                    case "5 - SD": return 5;
                    case "6 - SD": return 6;
                    case "7 - SMP": return 7;
                    case "8 - SMP": return 8;
                    case "9 - SMP": return 9;
                    case "10 - SMA": return 10;
                    case "11 - SMA": return 11;
                    case "12 - SMA": return 12;
                    default: return 0;
                }
            }
            set {
                //Set Value to main model
                switch (value)
                {
                    case 0 :
                        _mainModel.Grade = "TK";
                        break;
                    case 1 :
                        _mainModel.Grade = "1 - SD";
                        break;
                    case 2 :
                        _mainModel.Grade = "2 - SD";
                        break;
                    case 3 :
                        _mainModel.Grade = "3 - SD";
                        break;
                    case 4 :
                        _mainModel.Grade = "4 - SD"; 
                        break;
                    case 5 :
                        _mainModel.Grade = "5 - SD"; 
                        break;
                    case 6 :
                        _mainModel.Grade = "6 - SD";
                        break;
                    case 7 :
                        _mainModel.Grade = "7 - SMP";
                        break;
                    case 8 :
                        _mainModel.Grade = "8 - SMP";
                        break;
                    case 9 :
                        _mainModel.Grade = "9 - SMP";
                        break;
                    case 10 :
                        _mainModel.Grade = "10 - SMA";
                        break;
                    case 11 :
                        _mainModel.Grade = "11 - SMA";
                        break;
                    case 12 :
                        _mainModel.Grade = "12 - SMA";
                        break;
                    default:
                        _mainModel.Grade = "TK";
                        break;
                }

                //Check Is Value Changed
                if (!_originalModel.Grade.Equals(value))
                    _isGradeChanged = true;
                else
                    _isGradeChanged = false;

                //Property Changed
                OnPropertyChanged(nameof(Grade));
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

        public string AddedBy {
            get {
                return "Teacher " + _mainModel.AddedBy.ToString();
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
                if (_mainModel.Student_ID == 0)
                    return false;

                return _isNameChanged || _isNicknameChanged ||
                       _isAddressChanged || _isPhoneNumberChanged ||
                       _isGradeChanged || _isSchoolChanged || _isStatusChanged;
            }
        }

        public bool IsPasswordAlreadyMade {
            get {
                return !string.IsNullOrEmpty(_mainModel.Password);
            }
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
