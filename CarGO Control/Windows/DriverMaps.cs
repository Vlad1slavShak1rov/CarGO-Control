using CarGO_Control.DataBase;
using CarGO_Control.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CarGO_Control.Windows
{
    public partial class DriverMaps : Form
    {
        Window _window;
        Route _route;
        GMapControl gmap;
        ApiGetCity api = new();

        public DriverMaps(Window window, Route route)
        {
            InitializeComponent();
            MapPanel.Controls.Add(LoadBar);
            _window = window;
            _route = route;
        }

        private async void InitMap()
        {
            LoadBar.Visible = true;
            LoadBar.Value = 0;

            gmap = new GMapControl
            {
                Dock = DockStyle.Fill,
                MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance,
                Position = new PointLatLng(64.6863136, 97.7453061),
                MinZoom = 2,
                MaxZoom = 18
            };

            LoadBar.Value += 50;
            gmap.MouseDown += gmap_MouseDown;
            gmap.MouseWheel += gmap_MouseWheel;

            string FromWay = _route.CityFrom;
            string ToWay = _route.CityTo;

            PointLatLng start = await api.ReturnResponse(FromWay);
            PointLatLng end = await api.ReturnResponse(ToWay);
            LoadBar.Value += 25;
            Task.Delay(100);
            if (start != null && end != null)
            {
                gmap = await api.ReturnRoute(gmap, start.Lat, start.Lng, end.Lat, end.Lng);

                double distanceInKm = double.Parse(ApiGetCity.Distance.ToString()) / 1000;
                distanceInKm = Math.Round(distanceInKm, 1);

                gmap.Position = new PointLatLng(((start.Lat + end.Lat) / 2), ((start.Lng + end.Lng) / 2));
                gmap.Zoom = (distanceInKm + 1) % 18;

                LoadBar.Value += 15;
                Task.Delay(100);
            }
            else
            {
                SMB.ShowWarningMessageBox("Не удалось получить координаты для указанных мест.");
            }

            LoadBar.Value += 10;
            Task.Delay(100);
            LoadBar.Visible = false;
            MapPanel.Controls.Add(gmap);
        }
        private void gmap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var point = e.Location;
                var latLng = gmap.FromLocalToLatLng(point.X, point.Y);
                gmap.Position = latLng;
            }
        }
        private void gmap_MouseWheel(object sender, MouseEventArgs e)
        {
            var currentZoom = gmap.Zoom;
            if (e.Delta > 0)
            {
                if (currentZoom < gmap.MaxZoom)
                {
                    gmap.Zoom++;
                }
            }
            else if (e.Delta < 0)
            {
                if (currentZoom > gmap.MinZoom)
                {
                    gmap.Zoom--;
                }
            }
        }

        private void DriverMaps_Load(object sender, EventArgs e)
        {
            MapPanel.Controls.Clear();
            InitMap();
        }

        private void BackButon_Click(object sender, EventArgs e)
        {
            _window.Show();
            this.Close();
        }
    }
}
