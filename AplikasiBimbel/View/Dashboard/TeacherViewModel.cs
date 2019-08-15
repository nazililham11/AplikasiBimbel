using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.View.Dashboard
{
    public class TeacherViewModel : INotifyPropertyChanged
    {

        #region Field

        private TeacherModel _teacher;
        private bool _isSelected;
        #endregion

        #region Constructor

        public TeacherViewModel(TeacherModel teacher)
        {
            this._teacher = teacher;
        }

        #endregion

        #region Properties

        public TeacherModel Teacher {
            get { return _teacher; }
            set {
                _teacher = value;
                OnPropertyChanged(nameof(Teacher));
            }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsActiveTeacher {
            get { return Teacher.Status == "Aktif"; }
            set {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
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
