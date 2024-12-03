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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using CarGO_Control.Tools;


namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public event RoutedEventHandler BackToCreateCarGo;
        Random rand = new Random();
        private static readonly HttpClient httpClient = new HttpClient();
        ApiGetCity apiGetCity = new();
        public Map()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MyMaps.MapProvider = GMapProviders.GoogleMap;
            MyMaps.MinZoom = 2;
            MyMaps.MaxZoom = 17;
            MyMaps.Zoom = 10;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackToCreateCarGo?.Invoke(null, null);
        }

        private async void SearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            MyProgressBar.Visibility = Visibility.Visible;
            MyProgressBar.Value = 0;
            var coord = await apiGetCity.ReturnResponse(SearchBox.Text);
            while(MyProgressBar.Value < 100)
                 MyProgressBar.Value += rand.Next(10, 20); await Task.Delay(100);

            MyProgressBar.Visibility = Visibility.Hidden;
            DataHandler(coord); 
        }

        private void DataHandler(PointLatLng data)
        {
            MyMaps.Position = data;
            MyMaps.Zoom = 10;
        }

    }
}
