using GMap.NET.MapProviders;
using GMap.NET;
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
using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using CarGO_Control.Windows;
using Route = CarGO_Control.DataBase.Route;
namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateCarGo.xaml
    /// </summary>
    public partial class CreateCarGo : System.Windows.Controls.UserControl
    {
        public event RoutedEventHandler BackButtonClick;
        OperatorMainWindow operatorMainWindow;

        DriverRepository _driversRepository;
        TruckRepository _truckRepository;
        CargoRepository _cargoRepository;
        RouteRepository _routeRepository;

        List<Driver> _drivers = new();
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        Random random = new Random();
        int _driverId;
        public static string UrlRoute { get; set; }
        public static string CityTo { get; set; }
        public static string CiryFrom {  get; set; }
        public CreateCarGo(OperatorMainWindow operatorMainWindow)
        {
            operatorMainWindow.LoadDataHandler += OperatorMainWindow_LoadDataHandler; ;
            InitializeComponent();
        }

        private void OperatorMainWindow_LoadDataHandler(object? sender, EventArgs e)
        {
            InitComboBox();
            TrackNumGenerator();
        }

        private void InitComboBox()
        {
            var items = new List<string>
            {
                "Хрупкие грузы",
                "Негабаритные грузы",
                "Сыпучие грузы",
                "Жидкие грузы",
                "Мягкие грузы",
                "Сухие грузы",
                "Перевозимые товары"
            };

            foreach (string item in items)
            {
                TypeLoadBox.Items.Add(item);
            }

            using (var db = new CarGoDBContext())
            {
                _driversRepository = new(db);
                _truckRepository = new(db);
                var drivers = _driversRepository.GetAll().ToList();
                _drivers = drivers;
                foreach (var driver in drivers)
                {
                    if (driver?.Name != null) 
                    {
                        DriversNameBox.Items.Add(driver.Name);
                    }
                }

                var trucks = _truckRepository.GetAll().ToList();
                foreach (var truck in trucks)
                {
                    if (truck?.CarMake != null)
                    {
                        TruckMark.Items.Add($"{truck.CarMake} {truck.LicensePlate}");
                    }
                }
            }

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClick?.Invoke(null, null);
        }

        private void SelectRoute_Click(object sender, RoutedEventArgs e)
        {
            (new MapForm()).ShowDialog();
            BackGroundCity.Content = $"{CiryFrom} - {CityTo}";
            ForeGroundCity.Content = $"{CiryFrom} - {CityTo}";
        }

        private void LoadBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CheckTextBox.CheckText(e);
        }

        private void TrackNumGenerator()
        {
            string trackNum = TrackNumberBox.Text;
            while (TrackNumberBox.Text.Count() <= TrackNumberBox.MaxLength)
            {
                int select = random.Next(0, 3);
                int symbol;
                switch (select)
                {
                    case 0:
                        symbol = random.Next(0, alphabet.Length - 1);
                        trackNum += char.ToUpper(alphabet[symbol]);
                        break;
                    case 1:
                        symbol = random.Next(0, alphabet.Length - 1);
                        trackNum += alphabet[symbol];
                        break;
                    case 2:
                        symbol = random.Next(0, 9);
                        trackNum += symbol.ToString();
                        break;
                }
                TrackNumberBox.Text = trackNum;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypeLoadBox.Text != string.Empty && LoadBox.Text != string.Empty && DateDepartBox.Text != string.Empty
                && DateArrivalBox.Text != string.Empty && TruckMark.Text != string.Empty && LoadBox.Text != string.Empty
                && WeightBox.Text != string.Empty)
            {
                using (var db = new CarGoDBContext())
                {
                    _cargoRepository = new(db);
                    _driversRepository = new(db);
                    _routeRepository = new(db);
                    _truckRepository = new(db);
                    var cargo = new Cargo()
                    {
                        CargoType = TypeLoadBox.Text,
                        Contents = LoadBox.Text,
                        Weight = int.Parse(WeightBox.Text)
                    };

                    _cargoRepository.Add(cargo);
                    int cargoId = cargo.ID;

                    string licensePlate = TruckMark.Text.Split(' ')[1];
                    var truck = _truckRepository.GetBySignleLicensePlate(licensePlate);
                    var driver = _driversRepository.GetByLogin(DriversNameBox.Text);
                    driver.TruckID = truck.ID;
                   

                    var route = new Route()
                    {
                        DriverID = _driverId,
                        IDCarGo = cargoId,
                        IDTruck = truck.ID,
                        TrackNumer = TrackNumberBox.Text,
                        CityFrom = CiryFrom,
                        CityTo = CityTo,
                        RouteHTTP = UrlRoute,
                        DataOut = DateTime.Parse(DateDepartBox.Text),
                        DataIn = DateTime.Parse(DateArrivalBox.Text)

                    };
                    _routeRepository.Add(route);
                }
            }
            else SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");            
        }

        private async void DriversNameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var driver in _drivers)
            {
                if(DriversNameBox.SelectedItem == null) return;
                if (driver.Name == DriversNameBox.SelectedItem.ToString())
                {
                    ExpDriversLabel.Text = driver.Experience.ToString();
                    _driverId = driver.Id;
                }
                await Task.Delay(100);
            }

        }

        private void WeightBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CheckTextBox.CheckNum(e);
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (sender as Calendar)?.SelectedDate ?? DateTime.MinValue;
            if (selectedDate >= DateTime.Now)
            {
                if (DateArrivalBox.IsEnabled)
                {

                    DateArrivalBox.Text = selectedDate.Date.ToString("dd.MM.yyyy");
                    DateArrivalBox.IsEnabled = false;

                }
                if (DateDepartBox.IsEnabled)
                {
                    DateDepartBox.Text = selectedDate.Date.ToString("dd.MM.yyyy");
                    DateDepartBox.IsEnabled = false;
                    DateArrivalBox.IsEnabled = true;
                }
            }
            else SMB.ShowWarningMessageBox($"Выбранная вами даты не может быть ниже {DateTime.Now.Date}");
        }

        private void ChangeDate_Click(object sender, RoutedEventArgs e)
        {
            DateDepartBox.Text = "";
            DateArrivalBox.Text = "";
            DateDepartBox.IsEnabled = true;
            DateArrivalBox.IsEnabled = false;
        }
    }
}
