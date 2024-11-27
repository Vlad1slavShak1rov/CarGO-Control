using CarGO_Control.Tools;
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
using System.Windows.Shapes;

namespace CarGO_Control.Windows
{
    /// <summary>
    /// Логика взаимодействия для questionWin.xaml
    /// </summary>
    public partial class questionWin : Window
    {
        public questionWin()
        {
            InitializeComponent();
        }
        public int Result { get; private set; }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (ExpBox.Text != string.Empty && int.Parse(ExpBox.Text) < 100)
            {
                Result = int.Parse(ExpBox.Text);
                this.DialogResult = true;
                this.Close();
            }
            else SMB.ShowWarningMessageBox("Введите нормальный стаж вождения!");
        }

        private void ExpBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
