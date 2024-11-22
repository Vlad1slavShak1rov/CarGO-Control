using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using CarGO_Control.Views;

namespace CarGO_Control.Windows
{
    /// <summary>
    /// Логика взаимодействия для OperatorMainWindow.xaml
    /// </summary>
    public partial class OperatorMainWindow : Window
    {
        private DispatcherTimer _timer;
        private List<Driver> _drivers = new List<Driver>();
        private string _name;
        public OperatorMainWindow(string nick)
        {
            _name = nick;
            TimerInit();
            InitializeComponent();
            HelloLabel.Content = $"Добро пожаловать: {nick}";
            
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

        private void ManagementButton_Click(object sender, RoutedEventArgs e)
        {
            RegDriversButton.Visibility = Visibility.Hidden;
            ManagementButton.Visibility = Visibility.Hidden;
            DriversList.Visibility = Visibility.Visible;
            TextStack.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Visible;

            if (_drivers.Count == 0)
            {
                LoadDB();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            RegDriversButton.Visibility = Visibility.Visible;
            ManagementButton.Visibility = Visibility.Visible;
            DriversList.Visibility = Visibility.Hidden;
            TextStack.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Hidden;

        }

        private void LoadDB()
        {
                using (var db = new CarGoDBContext())
                {
                    var drivers = db.Drivers.ToList();
                    if (drivers != null)
                    {
                        LoadDriversField(drivers);
                    }
                    else
                    {
                        SMB.ShowWarningMessageBox("Водители отсувствуют!");
                        return;
                    }
                }
        }

        private void LoadDriversField(List<Driver> drivers)
        {
            foreach (var driver in drivers)
            {
                UserControl1 userControl1 = new();
                userControl1.Width = 600;
                userControl1.Height = 80;
                userControl1.BackGround.Fill = Brushes.White;
                userControl1.NameLabel.Content = "Имя: " + driver.Name;
                userControl1.ExpLabel.Content =  "Опыт: " + driver.Experience.ToString();
                userControl1.TruckLabel.Content = "Марка грузовика: " + "-";
                userControl1.RouteLabel.Content = "Трек-номер" + "-";
                userControl1.Margin = new Thickness(0, 0, 0, 10);

                DriversList.Children.Add(userControl1);
                _drivers.Add(driver);
                
            }
        }
    }
}
