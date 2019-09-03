using AplikasiBimbel.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace AplikasiBimbel.Admin.Views.Settings
{
    /// <summary>
    /// Interaction logic for MainSettingsView.xaml
    /// </summary>
    public partial class MainSettingsView : UserControl, INotifyPropertyChanged
    {
        #region Private Member

        private List<SettingsMenuItemViewModel> _settingMenus;
        private SettingsMenuItemViewModel _currentSettingItem;

        #endregion


        #region Constructor

        public MainSettingsView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            //Reset All Views
            ResetAllViews();

            //Open First Menu
            SelectView(SettingMenus[0]);
        }

        #endregion

        #region Method

        private void ResetAllViews()
        {
            SettingMenus = new List<SettingsMenuItemViewModel>()
            {
                new SettingsMenuItemViewModel
                {
                    Title = "Manage Database",
                    Icon = PackIconKind.Database,
                    IsSelected = false,
                    View = new DatabaseSettingsView()
                },
                new SettingsMenuItemViewModel
                {
                    Title = "Manage Connection",
                    Icon = PackIconKind.LanConnect,
                    IsSelected = false,
                    View = new ConnectionSettings()
                }
            };
        }

        private void SelectView(SettingsMenuItemViewModel settingsMenuItem)
        {
            if (CurrentSettingItem == settingsMenuItem)
                return;

            CurrentSettingItem.IsSelected = false;

            CurrentSettingItem = settingsMenuItem;

            CurrentSettingItem.IsSelected = true;
        }


        #endregion


        #region Properties

        public List<SettingsMenuItemViewModel> SettingMenus {
            get {
                if (_settingMenus == null)
                    _settingMenus = new List<SettingsMenuItemViewModel>();

                return _settingMenus;
            }
            set {
                _settingMenus = value;

                OnPropertyChanged(nameof(SettingMenus));
            }
        }

        public UserControl CurrentSettingView {
            get {
                return CurrentSettingItem.View;
            }
        }

        public SettingsMenuItemViewModel CurrentSettingItem {
            get {
                if (_currentSettingItem == null)
                    _currentSettingItem = new SettingsMenuItemViewModel()
                    {
                        View = new UserControl()
                    };

                return _currentSettingItem;
            }
            set {
                if (_currentSettingItem == value)
                    return;

                _currentSettingItem = value;

                OnPropertyChanged(nameof(CurrentSettingItem));
                OnPropertyChanged(nameof(CurrentSettingView));
            }
        }

        #endregion



        #region SelectMenuCommand Command

        private RelayParameterizedCommand _selectMenuCommand = null;

        public RelayParameterizedCommand SelectMenuCommand {
            get {
                if (_selectMenuCommand == null)
                    _selectMenuCommand = new RelayParameterizedCommand(SelectMenuCommand_Execute, SelectMenuCommand_CanExecute);

                return _selectMenuCommand;
            }
        }

        private void SelectMenuCommand_Execute(object param)
        {
            if (!(param is SettingsMenuItemViewModel))
                return;

            SelectView(param as SettingsMenuItemViewModel);
            
        }

        private bool SelectMenuCommand_CanExecute(object param)
        {
            if (!(param is SettingsMenuItemViewModel))
                return false;

            SettingsMenuItemViewModel settingsItem = param as SettingsMenuItemViewModel;

            if (settingsItem.View == null)
                return false;

            return true;
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
