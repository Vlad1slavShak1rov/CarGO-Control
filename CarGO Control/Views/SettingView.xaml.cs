using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using CarGO_Control.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UserControl = System.Windows.Controls.UserControl;

namespace CarGO_Control.Views
{
    public partial class SettingView : UserControl
    {
        private string _name;
        public EventHandler<string> ChangeData;
        public RoutedEventHandler BackToMain;
        public EventHandler<MessageBoxResult> LeaveProfile;
        int _role;
        OperatorRepository _operator;
        DriverRepository _driver;
        public SettingView(string Name, int role)
        {
            _name = Name;
            _role = role;
            InitializeComponent();
            SaveСhangeButton.Visibility = Visibility.Hidden;
            LoadData(_name);
        }
        private void LoadData(string Name)
        {
            using (var db = new CarGoDBContext())
            {
                switch (_role)
                {
                    case 1:
                        RoleBox.Text = "Оператор";
                        var @operator = db.Operators.
                        Include(d => d.User).
                        FirstOrDefault(d => d.Name == Name);
                        if (@operator != null)
                        {
                            NameBox.Text = @operator.Name;
                            var user = @operator.User.Login;
                            if (user != null)
                            {
                                LoginBox.Text = user;
                            }
                        }
                        break;
                    case 2:
                        RoleBox.Text = "Водитель";
                        var driver = db.Drivers.
                        Include(d => d.Users).
                        FirstOrDefault(d => d.Name == Name);
                        if (driver != null)
                        {
                            NameBox.Text = driver.Name;
                            var user = driver.Users.Login;
                            if (user != null)
                            {
                                LoginBox.Text = user;
                            }
                        }
                        break;
                }
                
            }
        }

        private void NameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CheckTextBox.CheckText(e);
            if (_name != NameBox.Text)
            {
                SaveСhangeButton.Visibility = Visibility.Visible;
            }
        }

        private void SaveСhangeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CarGoDBContext())
            {
                switch (_role)
                {
                    case 1:
                        _operator = new(db);
                        var @operator = _operator.GetByLogin(_name);
                        @operator.Name = NameBox.Text;
                        _operator.Update(@operator);
                        _name = @operator.Name;
                        break;
                    case 2:
                        _driver = new(db);
                        var driver = _driver.GetByLogin(_name);
                        driver.Name = NameBox.Text;
                        _driver.Update(driver);
                        _name = driver.Name;
                        break;
                }
               
                ChangeData?.Invoke(this, NameBox.Text);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackToMain?.Invoke(this, e);
        }

        private void LeaveProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            if (_name != NameBox.Text)
            {
                result = SMB.QuestionMSG("У вас есть несохранненые данные! Сохранить?");
                if (result == MessageBoxResult.Yes)
                {
                    SaveСhangeButton_Click(null, null);
                    LeaveProfile?.Invoke(this, result);
                }
            }
            else
            {
                result = SMB.QuestionMSG("Вы действительно хотите выйти?");
                if (result == MessageBoxResult.Yes) LeaveProfile?.Invoke(this, result);
            }
            
        }
    }
}
