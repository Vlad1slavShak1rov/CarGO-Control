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

namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public event RoutedEventHandler BackToCreateCarGo;
        private static readonly HttpClient httpClient = new HttpClient();
        public Map()
        {
            InitializeComponent();
            InitializeMap();
            SearchButton_Click();
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MyMaps.MapProvider = GMapProviders.GoogleMap;
            MyMaps.MinZoom = 2;
            MyMaps.MaxZoom = 17;
            MyMaps.Zoom = 10;
            
        }

        

        public async Task<PointLatLng> GetCoordinatesAsync(string cityName)
        {
            try
            {
                string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(cityName)}&format=json&addressdetails=1";

                var response = await httpClient.GetStringAsync(url);
                var results = JArray.Parse(response);

                if (results.Count > 0)
                {
                    double lat = (double)results[0]["lat"];
                    double lon = (double)results[0]["lon"];
                    return new PointLatLng(lat, lon);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Error retrieving coordinates: {ex.Message}");
            }

            return new PointLatLng(0,0); 
        }

        private async void SearchButton_Click()
        {
            string cityName = "Новосибирск";

            var coordinates = await GetCoordinatesAsync(cityName);
            if (coordinates != null)
            {
                MyMaps.Position = coordinates;
                MyMaps.Zoom = 10;
            }
            else
            {
                MessageBox.Show("Координаты не найдены.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BackToCreateCarGo?.Invoke(null, null);
        }
    }
}
