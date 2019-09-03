using System.ComponentModel;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace AplikasiBimbel.Admin.Views.Settings
{
    public class SettingsMenuItemViewModel : INotifyPropertyChanged
    {


        #region Private Member

        private UserControl _view;
        private PackIconKind _icon;
        private string _title;
        private bool _IsSelected;

        #endregion


        #region Properties

        public UserControl View {
            get {
                if (_view == null)
                    _view = new UserControl();

                return _view;
            }
            set {
                _view = value;

                OnPropertyChanged(nameof(View));
            }
        }

        public PackIconKind Icon {
            get { return _icon; }
            set {
                _icon = value;

                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Title {
            get {
                if (_title == null)
                    _title = string.Empty;

                return _title;
            }
            set {
                _title = value;

                OnPropertyChanged(nameof(Title));
            }
        }
        
        public bool IsSelected {
            get { return _IsSelected; }
            set {
                _IsSelected = value;

                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public int Index { get; set; }
        
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
