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
using System.Windows.Forms;
using CarGO_Control.Views;
using CarGO_Control.DataBase;
using GMap.NET.MapProviders;
using Route = CarGO_Control.DataBase.Route;

namespace CarGO_Control.Windows
{
    public partial class MapForm : Form
    {
        ApiGetCity api = new();
        GMapControl gmap = new();

        Route _route;
        bool isPressed = false;
        public MapForm(Route route)
        {
            InitializeComponent();
            InitMap();
            _route = route;
            if (route != null) InitRoute();
        }

        private void InitMap()
        {
            gmap = new GMapControl
            {
                Dock = DockStyle.Fill,
                MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance,
                Position = new PointLatLng(64.6863136, 97.7453061),
                MinZoom = 2,
                MaxZoom = 18
            };
            gmap.MouseDown += gmap_MouseDown;
            gmap.MouseWheel += gmap_MouseWheel;
            MapPanel.Controls.Add(gmap);
        }

        private void InitRoute()
        {
            SaveButton.Enabled = false;
            SearchBox.Enabled = false;
            ShowButton.Enabled = false;
            SearchButton.Enabled = false;
            FromBox.Enabled = false;
            ToBox.Enabled = false;

            FromBox.Text = _route.CityFrom;
            ToBox.Text = _route.CityTo;
            ShowButton_Click(null,null);
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

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchBox.Text != string.Empty)
            {
                LoadProgressBar.Visible = true;
                var pos = await api.ReturnResponse(SearchBox.Text);
                while (LoadProgressBar.Value < 100)
                {
                    LoadProgressBar.Value += 20;
                    await Task.Delay(100);
                }

                LoadProgressBar.Visible = false;
                LoadProgressBar.Value = 0;
                gmap.Zoom = 10;
                gmap.Position = pos;
            }
        }

        private async void ShowButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FromBox.Text) && !string.IsNullOrEmpty(ToBox.Text))
            {
                LoadProgressBar.Visible = true;

                string FromWay;
                string ToWay;
                if(_route != null)
                {
                    FromWay = _route.CityFrom;
                    ToWay = _route.CityTo;
                }
                else
                {
                    FromWay = FromBox.Text;
                    ToWay = ToBox.Text;
                }

                PointLatLng start = await api.ReturnResponse(FromWay);
                PointLatLng end = await api.ReturnResponse(ToWay);
                if (start != null && end != null)
                {
                    gmap = await api.ReturnRoute(gmap, start.Lat, start.Lng, end.Lat, end.Lng);

                    while (LoadProgressBar.Value < 100)
                    {
                        LoadProgressBar.Value += 20;
                        await Task.Delay(100);
                    }

                    double distanceInKm = double.Parse(ApiGetCity.Distance.ToString()) / 1000;
                    distanceInKm = Math.Round(distanceInKm, 1);

                    DistanceLabel.Text = "Дистанция: " + distanceInKm + " км";


                    LoadProgressBar.Visible = false;
                    LoadProgressBar.Value = 0;


                    gmap.Position = new PointLatLng(((start.Lat + end.Lat) / 2), ((start.Lng + end.Lng) / 2));
                    gmap.Zoom = 10;
                    isPressed = true;
                }
                else
                {
                    MessageBox.Show("Не удалось получить координаты для указанных мест.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, укажите начальную и конечную точки.");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (isPressed)
            {
                CreateCarGo.UrlRoute = ApiGetCity._urlRoute;
                CreateCarGo.CiryFrom = FromBox.Text;
                CreateCarGo.CityTo = ToBox.Text;
                this.Close();
            }
            else SMB.ShowWarningMessageBox("Маршрут не построен!");
        }
    }
}
