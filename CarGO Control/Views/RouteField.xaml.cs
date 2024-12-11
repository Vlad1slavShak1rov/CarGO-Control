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
using CarGO_Control.Windows;
using UserControl = System.Windows.Controls.UserControl;

namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для RouteField.xaml
    /// </summary>
    public partial class RouteField : UserControl
    {
        private Route _route;
        private DriverRepository _driverRepository;
        private TruckRepository _truckRepository;
        private CargoRepository _cargoRepository;
        public RouteField(Route route)
        {
            InitializeComponent();
            _route = route;
            InitData();
        }

        private void InitData()
        {
            using (var context = new CarGoDBContext()) 
            {
                _driverRepository = new(context);
                _truckRepository = new(context);
                _cargoRepository = new(context);
                var driver = _driverRepository.GetByID(_route.DriverID!.Value);
                DriverBox.Text = driver.Name;

                var truck = _truckRepository.GetByID(_route.IDTruck!.Value);
                TruckBox.Text = $"{truck.CarMake} ({truck.LicensePlate})";

                var cargo = _cargoRepository.GetByID(_route.IDCarGo!.Value);
                LoadBox.Text = cargo.Contents;
                LoadTypeBox.Text = cargo.CargoType;
            }
           
            ArrivalBox.Text = $"{_route.CityFrom}";
            DataArrivaltBox.Text = $"{ _route.DataOut.Date}";
            DepartBox.Text = $"{_route.CityTo}";
            DataDepartBox.Text = $"{_route.DataIn.Date}";
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (new MapForm(_route)).ShowDialog();
        }
    }
}
