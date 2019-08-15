using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {

        #region Field

        private string _usernameError;
        private string _passwordError;

        #endregion


        #region Properties

        public string UsernameError {
            get { return _usernameError; }
            set {
                _usernameError = value;
                OnPropertyChanged(nameof(UsernameError));

                if (!string.IsNullOrEmpty(value))
                    TextBox_Username.Focus();
            }
        }


        public string PasswordError {
            get { return _passwordError; }
            set {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));

                if (!string.IsNullOrEmpty(value))
                    PasswordBox_Password.Focus();
            }
        }

        #endregion

        #region Constructor

        public LoginView()
        {
            InitializeComponent();

            //Set Data Context
            this.DataContext = this;

            //Empty Error Message
            UsernameError = PasswordError = string.Empty;

        }

        #endregion


        #region Events

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_Username.Focus();
        }


        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string username = TextBox_Username.Text.Trim().ToLower();
            string password = PasswordBox_Password.Password;
            Login(username, password);
        }


        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (App.ApplicationWindow != null)
                App.ApplicationWindow.Close();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Button_Login_Click(sender, new RoutedEventArgs());
        }

        #endregion


        #region Method

        public void Login(string username, string password)
        {
            //Set Error Message to Empty
            UsernameError = string.Empty;
            PasswordError = string.Empty;

            //Check Username is Empty
            if (string.IsNullOrEmpty(username))
            {
                UsernameError = "Username Tidak Boleh Kosong";
                return;
            }

            //DEBUG
            if (!username.Equals("admin"))
            {
                UsernameError = "Username Tidak Ditemukan";
                return;
            }

            if (!password.Equals("admin"))
            {
                PasswordError = "Password Salah";
                return;
            }

            App.TeacherSession = App.admin;

            if (App.ApplicationWindow != null)
                App.ApplicationWindow.LoginCallback();

        }

        #endregion



        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #endregion

    }
}
