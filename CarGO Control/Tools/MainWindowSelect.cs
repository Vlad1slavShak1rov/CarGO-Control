using CarGO_Control.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarGO_Control.Tools 
{
    public class OperatorMain : IMainWindow
    {
        public void Show()
        {
            (new OperatorMainWindow()).Show();
            return;
        }
    }

    public class DriverMain : IMainWindow
    {
        public void Show()
        {
            (new DriverMainWindow()).Show();
            return;
           
        }
    }
}
