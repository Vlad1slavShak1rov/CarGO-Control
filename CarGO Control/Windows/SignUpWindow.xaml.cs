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

namespace CarGO_Control
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }
        int RoleID = 0;
        private void LoginLabelCheck(object sender, TextCompositionEventArgs e)
        {
            string forbiddenChars = "()!@#$%^&*_-+=.,<>№;:?*";

            if (forbiddenChars.Contains(e.Text) || forbiddenChars.Contains('"'))
            {
                e.Handled = true;
            }

        }

        private void DriverCheck(object sender, RoutedEventArgs e)
        {
            Animation("C:\\Users\\sakir\\source\\repos\\CarGO Control\\CarGO Control\\Resources\\bbsqIiUZ4M4tC561FZ8onewDagtf7gcsGhfyVLW4DygeqhSOB.jpg");
        }
        private void OperatorCheck(object sender, RoutedEventArgs e)
        {
            Animation("C:\\Users\\sakir\\source\\repos\\CarGO Control\\CarGO Control\\Resources\\f7ERynfdtWhxnU4A8sqf1tAG3Pp05YxODd9azteiTA1eGDlcC.jpg");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CarGoDBContext())
            {
                var users = context.Users.
                    FirstOrDefault(p => LoginTextBox.Text == p.Login);

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
            string password = HashFunction.HashPassword(PassBoxOne.Password);
            
            if (OperatorRadioButton.IsChecked == true) RoleID = 1;
            else if (DriverRadioButton.IsChecked == true) RoleID = 2;
            using (var context = new CarGoDBContext())
            {
                var user = new Users
                {
                    Login = LoginTextBox.Text,
                    Password = password,
                    RoleID = RoleID,
                };

                switch (RoleID)
                {
                    case 1:
                        var op = new Operator
                        {
                            Name = LoginTextBox.Text,
                            UserID = user.ID,
                        };
                        return;
                    case 2:
                        var dr = new Driver
                        {
                            UserID = user.ID,
                            Name = LoginTextBox.Text,
                            Experience = 0, //Нужно будет сделать это

                        };
                        return;
                }

                context.Add(user);
                context.SaveChanges();
            }
        }

        private bool CheckPass() 
        {
            bool Upper = false, Number = false;

            string password = PassBoxOne.Password;
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    Upper = true;
                    break;
                }
                
            }
            foreach (char c in password)
            {
                if (char.IsNumber(c))
                {
                    Number = true;
                    break;
                }

            }
            if(!Upper || !Number) return false;

            return true;
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
                if (CheckPass())
                {
                    Registration();
                    SMB.SuccessfulMSG("Успешно!");
                    switch (RoleID)
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
