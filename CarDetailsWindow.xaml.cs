using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarCatalog
{
    /// <summary>
    /// Логика взаимодействия для CarDetailsWindow.xaml
    /// </summary>
    public partial class CarDetailsWindow : Window
    {
        public CarDetailsWindow(Car car)
        {
            InitializeComponent();
            DataContext = car;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
