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
using UserControl = System.Windows.Controls.UserControl;

namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для TransManagement.xaml
    /// </summary>
    public partial class TransManagement : UserControl
    {
        public event RoutedEventHandler BackClick;
        public event RoutedEventHandler CreateCarGoClick;
        public event RoutedEventHandler AddTruck;
        public TransManagement()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            BackClick?.Invoke(this, e);
        }

        private void CreateCarGo_Click(object sender, RoutedEventArgs e)
        {
            CreateCarGoClick?.Invoke(this, e);
        }

        private void ViewCarGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTruckButton_Click(object sender, RoutedEventArgs e)
        {
            AddTruck?.Invoke(this, e);
        }
    }
}
