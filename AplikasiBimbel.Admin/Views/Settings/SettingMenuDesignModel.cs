using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiBimbel.Admin.Views.Settings
{
    public class SettingMenuDesignModel
    {
        #region Singleton

        public static SettingMenuDesignModel Instance => new SettingMenuDesignModel();

        #endregion

        
        #region Constructor
        
        public SettingMenuDesignModel()
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

        #endregion


        #region Properties

        public List<SettingsMenuItemViewModel> SettingMenus { get; set; }

        #endregion
    }
}
