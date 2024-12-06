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

namespace CarGO_Control.Windows
{
    public partial class MapForm : Form
    {
        ApiGetCity api = new();
        GMapControl gmap = new();
        public MapForm()
        {
            InitializeComponent();
            InitMap();
        }

        private void InitMap()
        {
            gmap = new GMapControl
            {
                Dock = DockStyle.Fill,
                MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance,
                Position = new PointLatLng(55.7558, 37.6173),
                MinZoom = 2, 
                MaxZoom = 18 
            };
            gmap.MouseDown += gmap_MouseDown;
            gmap.MouseWheel += gmap_MouseWheel;
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

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FromBox.Text) && !string.IsNullOrEmpty(ToBox.Text))
            {
                LoadProgressBar.Visible = true;
                PointLatLng start = await api.ReturnResponse(FromBox.Text);
                PointLatLng end = await api.ReturnResponse(ToBox.Text);
                if (start != null && end != null)
                {
                    gmap = await api.ReturnRoute(gmap, start.Lat, start.Lng, end.Lat, end.Lng);
                    if (gmap != null)
                    {
                        
                        while (LoadProgressBar.Value < 100)
                        {
                            LoadProgressBar.Value += 20;
                            await Task.Delay(100);
                        }

                        LoadProgressBar.Visible = false;
                        LoadProgressBar.Value = 0;


                        gmap.Position = new PointLatLng(((start.Lat + end.Lat) / 2), ((start.Lng + end.Lng) / 2));
                        gmap.Zoom = 10;
                    }
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
    }
}
