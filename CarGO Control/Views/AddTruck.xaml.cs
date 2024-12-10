using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using Microsoft.Ajax.Utilities;
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
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTruck.xaml
    /// </summary>
    public partial class AddTruck : UserControl
    {
        private const string FormatPattern = "A000AA|00";
        public event RoutedEventHandler BackButtonClick;
        TruckRepository _truckRepository;

        public AddTruck()
        {
            InitializeComponent();
          
            LicensePlatBox.MaxLength = FormatPattern.Length;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClick?.Invoke(sender, e);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CarMakeBox.Text.IsNullOrWhiteSpace() && !LicensePlatBox.Text.IsNullOrWhiteSpace())
            {
                using (var db = new CarGoDBContext())
                {
                    _truckRepository = new(db);

                    var Truck = new Truck
                    {
                        LicensePlate = LicensePlatBox.Text,
                        CarMake = CarMakeBox.Text
                    };

                    var trucks = _truckRepository.GetAll().FirstOrDefault(l => l.LicensePlate == Truck.LicensePlate);
                    if(trucks != null)
                    {
                        SMB.ShowWarningMessageBox("Грузовик с данным номером уже существует!");
                        return;
                    }
                    else
                    {
                        try
                        {

                            _truckRepository.Add(Truck);
                            SMB.SuccessfulMSG($"Успешно добавлено!");
                            BackButtonClick?.Invoke(null, null);
                        }
                        catch (Exception ex)
                        {
                            SMB.ShowWarningMessageBox($"{ex.Message}");
                        }
                    }
                }
            }
            else SMB.ShowWarningMessageBox("У вас есть незаполненные поля!");
        }

        private void LicensePlatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;

            tb.TextChanged -= LicensePlatBox_TextChanged;

            try
            {
                tb.Text = tb.Text.Replace("|", "");

                if (tb.Text.Length >= 6 && !tb.Text.Contains("|"))
                {
                    tb.Text = $"{tb.Text.Substring(0, 6)}|{tb.Text.Substring(6)}";
                }

                tb.Text = tb.Text.ToUpperInvariant();

                if (tb.Text.Length > FormatPattern.Length)
                {
                    tb.Text = tb.Text.Remove(FormatPattern.Length);
                }
                tb.SelectionStart = tb.Text.Length;
            }
            finally
            {
                tb.TextChanged += LicensePlatBox_TextChanged;
            }
        }
    }
}
