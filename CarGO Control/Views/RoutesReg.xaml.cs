using CarGO_Control.DataBase;
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
using CarGO_Control.DataBase;
using UserControl = System.Windows.Controls.UserControl;
using CarGO_Control.Windows;
using System.Data.Entity;

namespace CarGO_Control.Views
{
    
    public partial class RoutesReg : UserControl
    {
        public event RoutedEventHandler BackButtonClicked;
        
        private RouteField _routeField;
        private RouteRepository _routeRepository;
        public RoutesReg(OperatorMainWindow @operator)
        {
            InitializeComponent();
            @operator.LoadRoutes += LoadData;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked?.Invoke(null, null);
        }

        private void LoadData(object sender, EventArgs e)
        {
            RouteList.Children.Clear();
            using (var context = new CarGoDBContext())
            {
                _routeRepository = new(context);
                var routes = _routeRepository.GetAll();

                foreach (var route in routes)
                {
                    _routeField = new(route);
                    _routeField.Margin = new Thickness(0, 15, 0, 0);
                    RouteList.Children.Add(_routeField);
                }
            }
        }
    }
}
