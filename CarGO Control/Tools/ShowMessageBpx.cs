using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace CarGO_Control.Tools
{
    public static class SMB
    {
        public static void ShowWarningMessageBox(string text) => MessageBox.Show(text, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        public static void SuccessfulMSG(string text) => MessageBox.Show(text, "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

        public static MessageBoxResult QuestionMSG(string text) => MessageBox.Show($"Внимание: {text}", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
    }
}
