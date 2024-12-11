using CarGO_Control.DataBase;
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

namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : System.Windows.Controls.UserControl
    {
        public event EventHandler<Driver> DeleteButtonClick;
        public event EventHandler<Driver> EditButtonClick;

        private Driver _driver;
        public UserControl1(Driver driver)
        {
            InitializeComponent();
            using (var context = new CarGoDBContext())
            {
                RouteRepository routes = new(context);
                string? trackNum = routes.GetByIDrivers(driver.ID)?.TrackNumer;
                if (trackNum != null) RouteLabel.Content = "Трек-номер: " + trackNum;
                else RouteLabel.Content = "Трек-номер: отсутствует";

                TruckRepository trucks = new(context);
                if (driver.TruckID.HasValue)
                {
                    string? truck = trucks.GetByIDDriver(driver.TruckID!.Value)?.CarMake;
                    TruckLabel.Content = "Марка: " + truck;
                }
                else
                {
                    TruckLabel.Content = "Марка: отсутствует";
                }

            }

            NameLabel.Content = "Имя: " + driver.Name;
            ExpLabel.Content = "Опыт: " + driver.Experience;
            _driver = driver;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteButtonClick?.Invoke(this, _driver); 
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditButtonClick?.Invoke(this, _driver);     
        }
    }
}
