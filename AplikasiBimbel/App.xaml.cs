using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AplikasiBimbel.Controller;
using AplikasiBimbel.Model;
using AplikasiBimbel.Properties;
using AplikasiBimbel.View;

namespace AplikasiBimbel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Field

        //Database Connnection
        public static MySQL_Connection Connection;

        //Teacher Active Session
        public static TeacherModel TeacherSession;

        //Main Application Window
        public static MainWindow ApplicationWindow;

        //Controllers
        public static TeacherController TeacherController;

        #endregion

       
        #region Events

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            //Assigns
            Connection = new MySQL_Connection();

            ApplicationWindow = new MainWindow();

            TeacherController = new TeacherController();


            //Read Saved Settings
            ReadSettings();

            //Check Database Connection
            Connection.IsConnected = Connection.CheckDatabaseServer();

            //Show Main Window
            ApplicationWindow.Show();

            ForceAdminLogin();

        }
       

        //Save Settings When Application Exit
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }

        #endregion


        #region Method

        private void ReadSettings()
        {
            Connection.DatabaseHost = Settings.Default.DatabaseHost;
            Connection.DatabasePort = Settings.Default.DatabasePort;
            Connection.DatabaseName = Settings.Default.DatabaseName;
            Connection.DatabaseUsername = Settings.Default.DatabaseUsername;
            Connection.DatabasePassword = Settings.Default.DatabasePassword;
        }

        private void ForceAdminLogin()
        {
            TeacherModel teacher = new TeacherModel()
            {
                Teacher_ID = 9999,
                Name = "Admin",
                Username = "admin",
                Password = "admin",
                Address = "Localhost",
                Permission = "Super Admin",
                PhoneNumber = "911"
            };

            TeacherSession = teacher;

            App.ApplicationWindow.LoginCallback();
        }
        #endregion


       
    }
}
