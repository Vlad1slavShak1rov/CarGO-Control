using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarGO_Control
{
    public static class SMB
    {
        public static void ShowWarningMessageBox(string text) => MessageBox.Show(text, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        public static void SuccessfulMSG(string text ) => MessageBox.Show(text, "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
