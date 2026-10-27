using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarCatalog
{
    // INotifyPropertyChanged - свойство интерфейса, которое сообщает о изменении, а Binding - слушает только изменения
    // IDataErrorInfo работает только с INotifyPropertyChanged. Это механизм валидации, который получает строку ошибки и помечает элемент как невалидный (с ошибкой)
    public class Car : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _brand;
        private string _model;
        private int _year;
        private decimal _price;
        private int _mileage;

        public string Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(nameof(Model)); }
        }

        public int Year
        {
            get => _year;
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public int Mileage
        {
            get => _mileage;
            set { _mileage = value; OnPropertyChanged(nameof(Mileage)); }
        }
        // объявление события которое показывает что у твоего класса появилось свойство 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // IDataErrorInfo
        // метод который позволяет иметь пустое значение
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case nameof(Year):
                        if (Year <= 1900 || Year > DateTime.Now.Year)
                            error = "Год должен быть > 1900 и ≤ текущего года";
                        break;
                    case nameof(Price):
                        if (Price <= 0)
                            error = "Цена должна быть > 0";
                        break;
                    case nameof(Mileage):
                        if (Mileage < 0)
                            error = "Пробег должен быть ≥ 0";
                        break;
                }
                return error;
            }
        }
    }
}
