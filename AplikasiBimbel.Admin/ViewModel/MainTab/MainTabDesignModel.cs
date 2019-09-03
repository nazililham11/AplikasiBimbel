using System.Collections.Generic;
using MaterialDesignThemes.Wpf;

namespace AplikasiBimbel.Admin.ViewModel
{
    public class MainTabDesignModel : MainTabViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static MainTabDesignModel Instance => new MainTabDesignModel();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainTabDesignModel()
        {
            Items = new List<MainTabItemViewModel>
                {
                    new MainTabItemViewModel
                        {
                            Title = "Login",
                            Icon = PackIconKind.LoginVariant,
                            IsSelected = false,
                            IsVisible = true,
                            IsMenuCollapsed = true
                        },
                        new MainTabItemViewModel
                        {
                            Title = "Teacher",
                            Icon = PackIconKind.Teacher,
                            IsSelected = true,
                            IsVisible = true,
                            IsMenuCollapsed = true
                        },
                        new MainTabItemViewModel
                        {
                            Title = "Student",
                            Icon = PackIconKind.School,
                            IsSelected = false,
                            IsVisible = true,
                            IsMenuCollapsed = true
                        },
                        new MainTabItemViewModel
                        {
                            Title = "Material",
                            Icon = PackIconKind.FileDocument,
                            IsSelected = false,
                            IsVisible = true,
                            IsMenuCollapsed = true
                        },
                        new MainTabItemViewModel
                        {
                            Title = "Report",
                            Icon = PackIconKind.FileDocumentBoxCheck,
                            IsSelected = false,
                            IsVisible = true,
                            IsMenuCollapsed = true
                        }
                };
        }



        #endregion
    }
}
