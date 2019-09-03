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
        public static string ConnectionString;

        public static ConnectionModel Connection;

        #endregion


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ReadConnectionSettings();

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
 
        protected override void OnExit(ExitEventArgs e)
        {
            //Save Settings When Application Exit
            Settings.Default.Save();
        }


        #region Method
        private void ReadConnectionSettings()
        {
            //Read Connection
            Connection = new ConnectionModel()
            {
                DatabaseHost = Settings.Default.DatabaseHost,
                DatabaseName = Settings.Default.DatabaseName,
                DatabaseUsername = Settings.Default.DatabaseUsername,
                DatabasePassword = Settings.Default.DatabasePassword,
                DatabasePort = Settings.Default.DatabasePort
            };
            ConnectionString = Settings.Default.ConnectionString;

        }

        private void ForceAdminLogin()
        {
            TeacherSession = admin;
            ApplicationWindow.LoginCallback();
        }
        #endregion
    }
}
