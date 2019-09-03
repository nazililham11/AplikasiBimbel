using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class StudentListItemViewModel : INotifyPropertyChanged
    {

        #region Private Member

        private StudentModel _student;
        private bool _isSelected;

        #endregion
        
        
        #region Constructor

        public StudentListItemViewModel(StudentModel student)
        {
            this._student = student;
        }

        #endregion


        #region Properties

        public StudentModel Student {
            get { return _student; }
            set {
                _student = value;
                OnPropertyChanged(nameof(Student));
            }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsActiveStudent {
            get { return Student.Status == "Aktif"; }
            set {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
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
