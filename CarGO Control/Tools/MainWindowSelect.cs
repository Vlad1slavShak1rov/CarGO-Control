using CarGO_Control.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CarGO_Control.Tools 
{
    public class OperatorMain : IMainWindow
    {
        public void Show(string name)
        {
            (new OperatorMainWindow(name)).Show();
            return;
        }
    }

    public class DriverMain : IMainWindow
    {
        public void Show(string name)
        {
            (new DriverMainWindow(name)).Show();
            return;
           
        }
    }
}
