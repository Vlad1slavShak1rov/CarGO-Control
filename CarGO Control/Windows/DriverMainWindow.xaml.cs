using CarGO_Control.Views;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace CarGO_Control.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverMainWindow.xaml
    /// </summary>
    public partial class DriverMainWindow : Window
    {
        private string _driverName;
        private DispatcherTimer _timer;
        private SettingView settingView;
        public DriverMainWindow(string name)
        {
            InitializeComponent();
            TimerInit();
            _driverName = name;
            settingView = new(_driverName,2);
            HelloLabel.Content = $"Добро пожаловать: {_driverName}";

            settingView.BackToMain += BackClick;
            settingView.ChangeData += ChangeNick;
            settingView.LeaveProfile += LeaveMainProfile;
        }

        private void TimerInit()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = "Время: " + DateTime.Now.ToString("HH:mm:ss");
            DateLabel.Content = "Дата: " + DateTime.Now.ToString("dd.MM.yy");
        }

        private void BackClick(object sender, EventArgs e)
        {
            ViewGrid.Children.Clear();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewGrid.Children.Clear();
            ViewGrid.Children.Add(settingView);
        }

        private void ChangeNick(object sender, string nick)
        {
            _driverName = nick;
            HelloLabel.Content = $"Добро пожаловать: {nick}";
        }

        private void LeaveMainProfile(object sender, MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                (new MainWindow()).Show();
                this.Close();
            }
        }
    }
}
