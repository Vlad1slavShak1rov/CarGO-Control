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
namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateCarGo.xaml
    /// </summary>
    public partial class CreateCarGo : System.Windows.Controls.UserControl
    {
        public event RoutedEventHandler BackButtonClick;
        public event RoutedEventHandler SelectRouteClick;
        public CreateCarGo()
        {
            InitializeComponent();
            //InitializeMap();
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClick?.Invoke(null, null);
        }

        private void SelectRoute_Click(object sender, RoutedEventArgs e)
        {
            SelectRouteClick?.Invoke(null, null);
        }
    }
}
