namespace AplikasiBimbel.Admin.ViewModel
{
    public class MainTabItemDesignModel : MainTabItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static MainTabItemDesignModel Instance => new MainTabItemDesignModel();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainTabItemDesignModel()
        {
            Title = "Teacher";
            Icon = MaterialDesignThemes.Wpf.PackIconKind.Teacher;
            IsSelected = true;
            IsVisible = true;
        }



        #endregion
    }
}
