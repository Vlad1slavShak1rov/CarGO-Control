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


namespace CarGO_Control
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            
        }

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
            if(PassBoxTwo.Password != PassBoxOne.Password)
            {
                SMB.ShowWarningMessageBox("У вас разные пароли!");
            }
            else if (PassBoxOne.Password.Length < 5 || PassBoxTwo.Password.Length < 5)
            {
                SMB.ShowWarningMessageBox("Пароли состоят из менее 5 символов");
            }
            else if (LoginTextBox.Text != "" && PassBoxOne.Password.Length >= 5 && PassBoxTwo.Password.Length >= 5)
            {
                SMB.SuccessfulMSG("Успешно!");
            }
            else
            {
                SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");
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

    }
}
