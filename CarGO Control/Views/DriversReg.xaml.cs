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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CarGO_Control.Views
{
    /// <summary>
    /// Логика взаимодействия для DriversReg.xaml
    /// </summary>
    public partial class DriversReg : UserControl
    {
        public event RoutedEventHandler BackButtonClicked;
        public event EventHandler LoadedFile;
        public event EventHandler Search;
        public EventHandler LoadDataBase;
        
        public DriversReg()
        {
            InitializeComponent();
            LoadDataAsync();
            
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked?.Invoke(this, new RoutedEventArgs());
        }

        private async void LoadDataAsync()
        {
            LoadingDataBar.Visibility = Visibility.Visible;
            LoadingDataBar.Value = 0;
            Random random = new();
            while (LoadingDataBar.Value < 100)
            {
                LoadingDataBar.Value += random.Next(10, 35);
                ProgressLabel.Content = $"{LoadingDataBar.Value} %";
                await Task.Delay(700);
            }
            ProgressLabel.Visibility = Visibility.Hidden;
            LoadingDataBar.Visibility = Visibility.Hidden;
            LoadedFinish(EventArgs.Empty);
            LoadDataBase?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void LoadedFinish(EventArgs e)
        {
            LoadedFile?.Invoke(this, e);
        }

        private void SearchBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CheckTextBox.CheckText(e);
            Search?.Invoke(this, e);
        }
    }
}
