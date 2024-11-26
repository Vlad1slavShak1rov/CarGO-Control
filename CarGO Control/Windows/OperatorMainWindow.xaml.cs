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
        private DriversReg DriversReg = new();
        private string _name;
        
        public OperatorMainWindow(string nick)
        {
            _name = nick;
            TimerInit();
            InitializeComponent();
            HelloLabel.Content = $"Добро пожаловать: {nick}";

            DriversReg.BackButtonClicked += DriversReg_BackButtonClicked;
            DriversReg.LoadedFile += LoadDate;
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
            ViewGrid.Children.Clear();
            ViewGrid.Children.Add(DriversReg);

        }
        private void DriversReg_BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ViewGrid.Children.Clear(); 
            RegDriversButton.Visibility = Visibility.Visible;
            ManagementButton.Visibility = Visibility.Visible;
        }

        private void LoadDate(object sender, EventArgs e)
        {
            using(var db = new CarGoDBContext())
            {
                var drivers = db.Drivers;
                foreach (var driver in drivers)
                {
                    UserControl1 driver_field = new();
                    driver_field.NameLabel.Content = "Имя " + driver.Name;
                    driver_field.ExpLabel.Content = "Опыт " + driver.Experience;
                    driver_field.TruckLabel.Content = "нету ";
                    Grid.SetColumn(driver_field, 1);
                    Grid.SetRow(driver_field, 1);
                    DriversReg.DriversList.Children.Add(driver_field);
                    _drivers.Add(driver);
                }

            }
        }

    }
}
