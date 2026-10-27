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
    /// Логика взаимодействия для CarEditWindow.xaml
    /// </summary>
    public partial class CarEditWindow : Window
    {
        public CarEditWindow(Car car)
        {
            InitializeComponent();
            DataContext = car;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var car = DataContext as Car;
            if (car == null) return;

            // Проверка валидации через IDataErrorInfo
            if (!string.IsNullOrEmpty(car[nameof(Car.Year)]) ||
                !string.IsNullOrEmpty(car[nameof(Car.Price)]) ||
                !string.IsNullOrEmpty(car[nameof(Car.Mileage)]))
            {
                MessageBox.Show("Исправьте ошибки валидации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}