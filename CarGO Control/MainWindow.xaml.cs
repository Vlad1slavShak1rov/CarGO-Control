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
                //TODO ПРВОЕРКА ПАРОЛЯ
                /*
                 * public static bool VerifyPassword(string password, string storedHash)
{
    byte[] hashBytes = Convert.FromBase64String(storedHash);
    
    // Извлечение соли из сохраненного хеша
    byte[] salt = new byte[32]; // Размер соли
    Array.Copy(hashBytes, 0, salt, 0, salt.Length);
    
    // Хеширование введенного пароля с использованием извлеченной соли
    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000))
    {
        byte[] hash = pbkdf2.GetBytes(32); // Получаем 32 байта хеша

        // Сравнение хеша введенного пароля с сохраненным
        for (int i = 0; i < hash.Length; i++)
        {
            if (hash[i] != hashBytes[i + salt.Length])
            {
                return false; // Пароль неверный
            }
        }
    }
    
    return true; // Пароль верный
}

                 */
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


    }
}