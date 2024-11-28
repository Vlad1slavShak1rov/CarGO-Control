using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using CarGO_Control.Windows;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CarGO_Control.Views
{
    public partial class SettingView : UserControl
    {
        private string _name;
        public EventHandler<string> ChangeData;
        public RoutedEventHandler BackToMain;
        public EventHandler LeaveProfile;
       
        public SettingView(string Name)
        {
            _name = Name;
            InitializeComponent();
            SaveСhangeButton.Visibility = Visibility.Hidden;
            LoadData(_name);
           
           
        }
        private void LoadData(string Name)
        {
            using (var db = new CarGoDBContext())
            {
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
                var @operator = db.Operators.
                    FirstOrDefault(d => d.Name == _name);
                @operator.Name = NameBox.Text;
                _name = @operator.Name;
                db.SaveChanges();

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
                }
            }

            LeaveProfile?.Invoke(this, e);
        }
    }
}
