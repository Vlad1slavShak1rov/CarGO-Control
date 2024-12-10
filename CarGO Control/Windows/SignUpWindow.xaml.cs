using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Security;
using CarGO_Control.DataBase;
using System.Security.Cryptography;
using CarGO_Control.Tools;
using CarGO_Control.Windows;
using System.Windows.Navigation;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace CarGO_Control
{
    public partial class SignUpWindow : Window
    {
        UsersRepository _usersRepository;
        public SignUpWindow()
        {
            InitializeComponent();
        }
        int RoleID = 0;
        
        private void LoginLabelCheck(object sender, TextCompositionEventArgs e)
        {
            if (!CheckTextBox.CheckText(e)) e.Handled = true;
        }

        private void DriverCheck(object sender, RoutedEventArgs e)
        {
            Animation("pack://application:,,,/Resources/OperatorMenu.jpg");
        }

        private void OperatorCheck(object sender, RoutedEventArgs e)
        {
            Animation("pack://application:,,,/Resources/DriverMenu.jpg");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SignUpButton_Click(this, null);
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CarGoDBContext())
            {
                _usersRepository = new(context);
                var users = _usersRepository.GetByLogin(LoginTextBox.Text);
                if (users == null) Authorization();
                else SMB.ShowWarningMessageBox("Пользователь с таким же логином уже зарегистрирован!");
            } 
        }

        private void Animation(string path)
        {
            imgDisplay.Source = new BitmapImage(new Uri(path));

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1)
            };
            imgDisplay.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        private void Registration()
        {
            int exp = 0;

            string password = HashFunction.HashPassword(PassBoxOne.Password);

            if (OperatorRadioButton.IsChecked == true) RoleID = 1;
            else if (DriverRadioButton.IsChecked == true) 
            { 
                RoleID = 2;
                questionWin form2 = new questionWin();
                form2.ShowDialog();
                exp = form2.Result;
            }

            using (var context = new CarGoDBContext())
            {
                var user = new Users
                {
                    Login = LoginTextBox.Text,
                    Password = password,
                    RoleID = RoleID,
                };
                _usersRepository = new(context);
                _usersRepository.Add(user);

                switch (RoleID)
                {
                    case 1:
                        var op = new Operator
                        {
                            Name = LoginTextBox.Text,
                            UserID = user.ID,
                        };
                        _usersRepository.AddCascadeOperator(op);
                        break;
                    case 2:
                        var dr = new Driver
                        {
                            UserID = user.ID,
                            Name = LoginTextBox.Text,
                            Experience = exp,
                            Users = user,
                        };
                        _usersRepository.AddCascadeDriver(dr);
                        break;
                }
            }
        }
 
        private void Authorization()
        {
            if (PassBoxTwo.Password != PassBoxOne.Password)
            {
                SMB.ShowWarningMessageBox("У вас разные пароли!");
            }
            else if (PassBoxOne.Password.Length < 5 || PassBoxTwo.Password.Length < 5)
            {
                SMB.ShowWarningMessageBox("Пароли состоят из менее 5 символов");
            }
            else if (LoginTextBox.Text != "" && PassBoxOne.Password.Length >= 5 && PassBoxTwo.Password.Length >= 5)
            {
                if (CheckTextBox.CheckPass(PassBoxOne.Password))
                {
                    Registration();
                    SMB.SuccessfulMSG("Успешно!");
                    switch (RoleID)
                    {
                        case 1:
                            var operatorWindow = MainWindowFactory.CreateWindow(MainWindowFactory.WindowType.Operator);
                            operatorWindow.Show(LoginTextBox.Text);
                            this.Close();
                            return;
                        case 2:
                            var driverWindow = MainWindowFactory.CreateWindow(MainWindowFactory.WindowType.Driver);
                            driverWindow.Show(LoginTextBox.Text);
                            this.Close();
                            return;
                    }
                }
                else
                {
                    SMB.ShowWarningMessageBox("Ваш пароль небезопасен\nВаш пароль должен содержать хотя бы одну " +
                        "заглавную букву\nДолжен иметь хотя бы одну цифру\nНе чередоваться: 1111, 00000 и т.п.");
                }
            }
            else
            {
                SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");
            }
        }

        private void BackWindow_Click(object sender, RoutedEventArgs e)
        {
            (new MainWindow()).Show();
            this.Close();
        }

       
    }
}
