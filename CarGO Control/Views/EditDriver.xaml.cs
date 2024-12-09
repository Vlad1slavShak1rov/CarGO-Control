using CarGO_Control.Windows;
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
using UserControl = System.Windows.Controls.UserControl;
namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для EditDriver.xaml
    /// </summary>
    public partial class EditDriver : UserControl
    {

        public event RoutedEventHandler BackClick;
        public event EventHandler ReloadList;
        string _tempName;
        string _tempExp;
        private List<Driver> _drivers = new List<Driver>();
        DriverRepository _driversRepositroy;
        public EditDriver(OperatorMainWindow operatorMain)
        {
            InitializeComponent();
            operatorMain.DriverChanged += AcceptData;
        }

        private void AcceptData(object sender, Driver driver)
        {
            _tempName = driver.Name;
            _tempExp = driver.Experience.ToString();
            NameBox.Text = driver.Name;
            ExpBox.Text = driver.Experience.ToString();
        }

        private void BackButtonClick_Click(object sender, RoutedEventArgs e)
        {
            BackClick?.Invoke(this, e);
        }

        private void SaveButtonClick_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != String.Empty && ExpBox.Text != String.Empty && TrackBox.Text != String.Empty && TruckMarkBox.Text != String.Empty)
            {
                using(var db = new CarGoDBContext())
                {
                    _driversRepositroy = new(db);
                    var drivers = _driversRepositroy.GetAll().
                        Where(dr => dr.Name == _tempName).
                        ToList();

                    foreach(var driver in drivers)
                    {
                        driver.Name = NameBox.Text;
                        driver.Experience = int.Parse(ExpBox.Text);
                        _driversRepositroy.Update(driver);
                    }
                    SMB.SuccessfulMSG("Данные успешно обновлены!");
                    ReloadList?.Invoke(null, EventArgs.Empty);
                    BackButtonClick_Click(null, null);
                }
            }
            else SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");
        }
    }
}
