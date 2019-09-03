using System.ComponentModel;

namespace AplikasiBimbel.Admin.ViewModel
{

    public class BaseViewModel : INotifyPropertyChanged
    {

        private static ApplicationViewModel _appViewModel;
        public static ApplicationViewModel AppViewModel {
            get {
                if (_appViewModel == null)
                    _appViewModel = new ApplicationViewModel();

                return _appViewModel;
            }
            set {
                _appViewModel = value;
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
