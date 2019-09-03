using AplikasiBimbel.ViewModel;
using MaterialDesignThemes.Wpf;

namespace AplikasiBimbel.Admin.ViewModel
{
    public class MainTabItemViewModel : BaseViewModel
    {

        #region Property

        private string _title;
        private bool _isSelected;
        private bool _isVisible;
        private PackIconKind _icon;
        private bool _isMenuCollapsed;

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
            get { return _isSelected; }
            set {
                _isSelected = value;

                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsVisible {
            get { return _isVisible; }
            set {
                _isVisible = value;

                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public PackIconKind Icon {
            get { return _icon; }
            set {
                _icon = value;

                OnPropertyChanged(nameof(Icon));
            }
        }

        public bool IsMenuCollapsed {
            get { return _isMenuCollapsed; }
            set {
                _isMenuCollapsed = value;

                OnPropertyChanged
(nameof(IsMenuCollapsed));
            }
        }

        #endregion
        
        
        #region Constructor

        public MainTabItemViewModel()
        {
        }

        #endregion

    }
}
