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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

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
        private EditDriver editDriver;
        private SettingView settingView;
        private string _name;
        public event EventHandler<Driver> DriverChanged;
        private bool _run = false;
        public OperatorMainWindow(string nick)
        {
            
            TimerInit();
            InitializeComponent();
            InitDrivers();

            _name = nick;
            HelloLabel.Content = $"Добро пожаловать: {nick}";

            editDriver = new(this);
            settingView = new SettingView(_name);

            DriversReg.BackButtonClicked += BackMenuClick;
            editDriver.ReloadList += AcceptData;
            DriversReg.LoadedFile += LoadData;
            DriversReg.Search += SearcDrivers;
            editDriver.BackClick += BackToTable;
            settingView.ChangeData += ChangeNick;
            settingView.BackToMain += BackMenuClick;
            settingView.LeaveProfile += LeaveMainProfile;
            
        }

        private void TimerInit()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void UpdateDataBaseEvent(object sender, EventArgs e)
        {
            UpdateDataBase();
        }

        private async void UpdateDataBase()
        {
            while (_run)
            {
                if (DriversReg.SearchBox.Text == string.Empty)
                {
                    LoadData(null, null);
                }
                await Task.Delay(100); 
            }
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
            DriversReg.LoadDataBase += UpdateDataBaseEvent;
            _run = true;
        }

        private void BackMenuClick(object sender, RoutedEventArgs e)
        {
            DriversReg.LoadDataBase -= UpdateDataBaseEvent;
            _run = false;
            LoadData(null, null);
            ViewGrid.Children.Clear();
            RegDriversButton.Visibility = Visibility.Visible;
            ManagementButton.Visibility = Visibility.Visible;
        }

        private void BackToTable(object sender, RoutedEventArgs e)
        {
            ViewGrid.Children.Clear();
            LoadData(null, null);
            ViewGrid.Children.Add(DriversReg);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewGrid.Children.Clear(); 
            ViewGrid.Children.Add(settingView);
        }

        private void ChangeNick(object sender, string nick)
        {
            _name = nick;
            HelloLabel.Content = $"Добро пожаловать: {nick}";
        }

        private void LeaveMainProfile(object sender, EventArgs e)
        {
            var result = SMB.QuestionMSG("Вы действительно хотите выйти?");
            if (result == MessageBoxResult.Yes)
            {
                (new MainWindow()).Show();
                this.Close();
            }
        }

        private void AcceptData(object sender, EventArgs e)
        {
            _drivers.Clear();
            InitDrivers();
        }

        private void SearcDrivers(object sender, EventArgs e)
        {
            DriversReg.DriversList.Children.Clear();
            string query = DriversReg.SearchBox.Text.ToLower();
            foreach (var driver in _drivers)
            {
                if (driver.Name.ToLower().StartsWith(query))
                {
                    AddUseControl(driver);
                }
            }
        }
        private void DeleteButton_Click(object sender, Driver driver)
        {
            var result = SMB.QuestionMSG($"Вы действительно хотите удалить водителя {driver.Name}?");

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new CarGoDBContext())
                {
                    var driverToRemove = db.Drivers
                        .Include(d => d.Users)
                        .FirstOrDefault(d => d.Id == driver.Id);

                    if (driverToRemove != null)
                    {
                        if (driverToRemove.Users != null)
                        {
                            db.Users.Remove(driverToRemove.Users);
                        }
                        db.Drivers.Remove(driverToRemove);
                        db.SaveChanges();
                    }
                    LoadData(null, null);
                }
            }
        }

        private void EditButton_Click(object sender, Driver driver)
        {
            ViewGrid.Children.Clear();
            ViewGrid.Children.Add(editDriver);
            DriverChanged?.Invoke(this, driver);
        }

        private void LoadData(object sender, EventArgs e)
        {
            DriversReg.DriversList.Children.Clear();
            using (var db = new CarGoDBContext())
            {
                var drivers = db.Drivers.ToList();
                foreach (var driver in drivers)
                {
                    AddUseControl(driver);
                }
            }
        }

        private void AddUseControl(Driver driver)
        {
            UserControl1 driver_field = new UserControl1(driver);
            driver_field.DeleteButtonClick += DeleteButton_Click;
            driver_field.EditButtonClick += EditButton_Click;
            driver_field.Margin = new Thickness(0, 0, 0, 10);
            DriversReg.DriversList.Children.Add(driver_field);
        }

        private void InitDrivers()
        {
            using (var db = new CarGoDBContext())
            {
                var drivers = db.Drivers.ToList();
                foreach (var driver in drivers)
                {
                    _drivers.Add(driver);
                }
            }
        }
    }
}