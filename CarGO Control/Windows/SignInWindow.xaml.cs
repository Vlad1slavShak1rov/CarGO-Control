using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using CarGO_Control.DataBase;
using CarGO_Control.Tools;

namespace CarGO_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isShowing = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignInPress(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != string.Empty && PassBox.Password.Length >= 5)
            {
                if (Authorization())
                {
                    SMB.SuccessfulMSG("Успешно!");
                    using (var context = new CarGoDBContext())
                    {
                        var users = context.Users.
                            FirstOrDefault(p => p.Login == LoginBox.Text);
                        int role = users.RoleID;
                        switch (role)
                        {
                            case 0:
                                var operatorWindow = MainWindowFactory.CreateWindow(MainWindowFactory.WindowType.Operator);
                                operatorWindow.Show();
                                this.Close();
                                return;
                            case 1:
                                var driverWindow = MainWindowFactory.CreateWindow(MainWindowFactory.WindowType.Driver);
                                driverWindow.Show();
                                this.Close();
                                return;

                            default:
                                return;
                        }
                    }
                }
                else SMB.ShowWarningMessageBox("Неправильно введен логин и/или пароль");
            }
            else if (PassBox.Password.Length < 5) SMB.ShowWarningMessageBox("Ваш пароль состоит из менее 5 символов");
            else SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");
        }

        private void SingUpPress(object sender, RoutedEventArgs e)
        {
            (new SignUpWindow()).Show();
            this.Close();
        }

        private void LoginBoxCheck(object sender, TextCompositionEventArgs e)
        {
            string forbiddenChars = "()!@#$%^&*_-+=.,<>№;:?*";

            if (forbiddenChars.Contains(e.Text) || forbiddenChars.Contains('"'))
            {
                e.Handled = true;
            }

        }       
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
           

            if (_isShowing)
            {
                PassBox.Password = PasswordShowBox.Text;
                PasswordShowBox.Visibility = Visibility.Hidden;
                PassBox.PasswordChar = '●'; 
                ToggleButton.Content = "Показать";
            }
            else
            {
                PasswordShowBox.Text = PassBox.Password;
                PasswordShowBox.Visibility = Visibility.Visible;
                ToggleButton.Content = "Скрыть";
            }
            
            _isShowing = !_isShowing;
        }

        private bool Authorization()
        {
            using (var context = new CarGoDBContext())
            {
                string search = LoginBox.Text;
                var users = context.Users.FirstOrDefault(p => p.Login == search);

                if (users == null || users.Login != LoginBox.Text) return false;

                if (users.Password == null || !HashFunction.VerifyPassword(PassBox.Password, users.Password)) return false;

                return true;
            }
        }
    }
}