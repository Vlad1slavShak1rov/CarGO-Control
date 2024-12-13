using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using GMap.NET;
using GMap.NET.MapProviders;
using CarGO_Control.Views;
using GMap.NET.WindowsForms;
using CarGO_Control.Windows;
using Microsoft.Ajax.Utilities;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using GMapRoute = GMap.NET.WindowsForms.GMapRoute;
using GMapControl = GMap.NET.WindowsForms.GMapControl;

namespace CarGO_Control.Tools
{
    public class ApiGetCity
    {
        protected HttpResponseMessage response;
        PointLatLng pointLat;
        public static string _urlRoute = "";
        public static JToken Distance { get; set; } 
        private async Task GetResponse(string q)
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
                    _urlRoute = query;
                    result = await response.Content.ReadAsStringAsync();
                    var jsonArray = JArray.Parse(result);
                    var coordsArr = jsonArray.First as JObject;
                    var lat = coordsArr?["lat"].ToString().Replace('.',',');
                    var lon = coordsArr?["lon"].ToString().Replace('.', ',');
                    if (lat != null && lon != null)
                    {
                        double latitude = double.Parse(lat);
                        double longitude = double.Parse(lon);
                        pointLat = new PointLatLng(latitude, longitude);
                    }
                }
                else
                {
                    SMB.ShowWarningMessageBox($"Ошибка: {response.StatusCode}"); 
                    
                }
                await Task.Delay(1000);
            }
        }

        private async Task<GMapControl> GetRoute(GMapControl gmap, string startCity, string endCity)
        {
            using var client = new HttpClient();
            PointLatLng start = await ReturnResponse(startCity);
            PointLatLng end = await ReturnResponse(endCity);

            gmap.Overlays.Clear();
            gmap = AddMarker(gmap, start.Lat, start.Lng, "Откуда", $"{startCity}", GMarkerGoogleType.blue_dot);
            gmap = AddMarker(gmap, end.Lat, end.Lng, "Куда", $"{endCity}", GMarkerGoogleType.red_dot);

            client.BaseAddress = new Uri("http://router.project-osrm.org/");
            string requestUrl = $"route/v1/driving/{start.Lng.ToString().Replace(',', '.')},{start.Lat.ToString().Replace(',', '.')};{end.Lng.ToString().Replace(',', '.')},{end.Lat.ToString().Replace(',', '.')}?overview=full";
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(result);
                if (jsonResult != null)
                {
                    var route = jsonResult["routes"].First;
                    var geometry = route["geometry"]?.ToString();
                    Distance = route["distance"].ToString();
                    if (!string.IsNullOrEmpty(geometry))
                    {
                        var points = DecodePolyline(geometry);
                        DrawRoute(points, ref gmap);
                    }
                }

            }
            else
            {
                SMB.ShowWarningMessageBox("Маршрут не найден");
            }
            return gmap;
        }
        private void DrawRoute(PointLatLng[] points, ref GMapControl gmap)
        {
            var routesLayer = new GMapRoute(points, "MyRoute")
            {
                Stroke = new Pen(new SolidBrush(Color.Red), 3)
            };

            var routesOverlay = new GMapOverlay("routes");
            routesOverlay.Routes.Add(routesLayer);
            
            gmap.Overlays.Add(routesOverlay);
            if (points.Length > 0)
            {
                gmap.Position = points[0];
            }
        }
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

        private GMapControl AddMarker(GMapControl gmap, double lat, double lng, string title, string tooltip, GMarkerGoogleType markerType)
        {
            
            var marker = new GMarkerGoogle(new PointLatLng(lat, lng), markerType)
            {
                ToolTipText = tooltip,
                ToolTipMode = MarkerTooltipMode.Always
            };

            var markersOverlay = new GMapOverlay("markers");
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);
            return gmap;
        }   

        public async Task<PointLatLng> ReturnResponse(string query)
        {
            await GetResponse(query);
            return pointLat;
        }

        public async Task<GMapControl> ReturnRoute(GMapControl gmap, string start, string end)
        {
            gmap = await GetRoute(gmap, start, end);
            return gmap;
        }
    }
}
