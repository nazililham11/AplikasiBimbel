using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AplikasiBimbel.Controller;
using AplikasiBimbel.Model;
using AplikasiBimbel.Admin.Views;
using AplikasiBimbel.Properties;

namespace AplikasiBimbel.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Field

        //Teacher Active Session
        public static TeacherModel TeacherSession;

        //Main Application Window
        public static MainWindow ApplicationWindow;

        //DEBUG
        //Admin Account
        public static TeacherModel admin;

        #endregion


        #region Events

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            ApplicationWindow = new MainWindow();
            
            //Show Main Window
            ApplicationWindow.Show();

            //DEBUG
            //Fll Admin Account 
            admin = new TeacherModel()
            {
                Teacher_ID = 9999,
                Name = "Admin",
                Username = "admin",
                Password = "admin",
                Address = "Localhost",
                Permission = "Super Admin",
                PhoneNumber = "911",
                Status = "Aktif",
                DateIn = DateTime.Parse("01/08/2019"),
                DateOut = null
            };

            //Force Auto Admin Login
            //ForceAdminLogin();

        }


        //Save Settings When Application Exit
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }

        #endregion


        #region Method

        private void ForceAdminLogin()
        {
            TeacherSession = admin;
            ApplicationWindow.LoginCallback();
        }
        #endregion
    }
}
