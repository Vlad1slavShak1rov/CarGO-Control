using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using CarGO_Control.Views;
using System.Windows.Media;

namespace CarGO_Control.Tools
{
    public class ApiGetCity
    {
        protected HttpResponseMessage response;
        PointLatLng pointLat;
        private async void GetResponse(string q)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
                client.DefaultRequestHeaders.Add("User-Agent", "vladssssss/6.0 (sakirovvladislav832@gmail.com)");
                string query = $"search?q={Uri.EscapeDataString(q)}&format=json";
                response = await client.GetAsync(query);
                string result;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    var jsonArray = JArray.Parse(result);
                    var coordsArr = jsonArray.First as JObject;
                    var lat = coordsArr?["lat"].ToString().Replace('.',',');
                    var lon = coordsArr?["lon"].ToString().Replace('.', ',');
                    //var name = coordsArr?["name"];
                    double latitude = double.Parse(lat);
                    double longitude = double.Parse(lon);

                    pointLat = new PointLatLng(latitude, longitude);
                    
                }
                else
                {
                    SMB.ShowWarningMessageBox($"Ошибка: {response.StatusCode}"); 
                    
                }
                await Task.Delay(1500);
            }
        }
        
        private async void GetRoute(GMapControl gMapControl, double startLat, double startLon, double endLat, double endLon)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://router.project-osrm.org/");
            string requestUrl = $"route/v1/driving/{startLon},{startLat};{endLon},{endLat}?overview=full";
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(result);
                var route = jsonResult["route"].First;
                if (route != null)
                {
                    var geometry = route["geometry"]?.ToString();
                    if (!string.IsNullOrEmpty(geometry))
                    {
                        var points = DecodePolyline(geometry);
                        //DrawRoute(points, gMapControl);
                    }
                }
            }
            else 
            {
                //todo
            }
        }
        /*
        private void DrawRoute(PointLatLng[] points, GMapControl gMapControl)
        {
            

            // Создаем маршрут на основе переданных точек
            var routesLayer = new GMapRoute(points)
            {
                Stroke = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 3) // Задаем цвет и толщину линии
            };

            // Создаем новый слой для маршрута
            var routesOverlay = new GMapOverlay("routes");
            routesOverlay.Routes.Add(routesLayer);

            // Очищаем предыдущие слои (если нужно)
            gMapControl.Markers.Clear(); // Если у вас есть маркеры, очищаем их
            gMapControl.Overlays.Clear(); // Очищаем предыдущие слои

            // Добавляем новый слой на карту
            gMapControl.Overlays.Add(routesOverlay);

            // Устанавливаем позицию карты на первую точку маршрута
            if (points.Length > 0)
            {
                gMapControl.Position = points[0];
            }
        }
        */
        private PointLatLng[] DecodePolyline(string encoded)
        {
            var polylinePoints = new List<PointLatLng>();
            int index = 0, len = encoded.Length;
            int lat = 0, lng = 0;

            while (index < len)
            {
                int b, shift = 0, result = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                int dlat = ((result >> 1) ^ -(result & 1));
                lat += dlat;

                shift = 0;
                result = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                int dlng = ((result >> 1) ^ -(result & 1));
                lng += dlng;

                var point = new PointLatLng((double)lat / 1E5, (double)lng / 1E5);
                polylinePoints.Add(point);
            }
            return polylinePoints.ToArray();
        }

        public PointLatLng ReturnResponse(string query)

        {
            GetResponse(query);
            return pointLat;
        }
    }
}
