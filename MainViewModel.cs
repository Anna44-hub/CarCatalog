using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace CarCatalog
{
    public class MainViewModel : INotifyPropertyChanged
    {// наблюдаемая коллекция машин, это как обычный список, но при этом он умеет сообщать интерфейсу, что его содержимое изменилось
     // Оно содержит внутри себя событие, которое автоматически сообщает об изменении, удалении или очещении элементов
        public ObservableCollection<Car> Cars { get; } = new ObservableCollection<Car>();
        public ICollectionView CarsView { get; }

        // Хранит реальное значение
        private Car _selectedCar;
        // публичное свойсвто к которому привязываем интерфейс (binding)
        public Car SelectedCar
        {
            get => _selectedCar;
            set { _selectedCar = value; OnPropertyChanged(nameof(SelectedCar)); }
        }
        // Хранит реальное значение
        private string _filterText;
        // публичное свойсвто к которому привязываем интерфейс (binding)
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                CarsView.Refresh();
            }
        }

        // объявлениекоманд
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DetailsCommand { get; }

        //Конструктор ViewModel
        public MainViewModel()
        {
            // Тестовые данные
            Cars.Add(new Car { Brand = "Toyota", Model = "Camry", Year = 2020, Price = 3_000_000, Mileage = 50_000 });
            Cars.Add(new Car { Brand = "BMW", Model = "X5", Year = 2019, Price = 5_000_000, Mileage = 80_000 });
            
            // настройка фильтрации
            CarsView = CollectionViewSource.GetDefaultView(Cars);
            CarsView.Filter = FilterCars;
            // инициализация
            AddCommand = new RelayCommand(_ => AddCar());
            // Кнопки будут активны только если SelectedCar != null
            EditCommand = new RelayCommand(_ => EditCar(), _ => SelectedCar != null);
            DeleteCommand  = new RelayCommand(_ => DeleteCar(), _ => SelectedCar != null);
            DetailsCommand = new RelayCommand(_ => ShowDetails(), _ => SelectedCar != null);
        }

        // фильтрация, если он пустой, то показываем все, а при вводе показываем, только то что подходит по переменной бренд
        private bool FilterCars(object obj)
        {
            if (obj is Car car && !string.IsNullOrWhiteSpace(FilterText))
                return car.Brand?.IndexOf(FilterText, System.StringComparison.OrdinalIgnoreCase) >= 0;
            return true;
        }

        private void AddCar()
        {
            var newCar = new Car();
            var editWindow = new CarEditWindow(newCar);
            if (editWindow.ShowDialog() == true)
                Cars.Add(newCar);
        }

        // редактирование
        private void EditCar()
        {
            if (SelectedCar == null) return;

            // Копия для возможности отмены
            var editedCar = new Car
            {
                Brand   = SelectedCar.Brand,
                Model   = SelectedCar.Model,
                Year    = SelectedCar.Year,
                Price   = SelectedCar.Price,
                Mileage = SelectedCar.Mileage
            };

            var editWindow = new CarEditWindow(editedCar);
            if (editWindow.ShowDialog() == true)
            {
                SelectedCar.Brand   = editedCar.Brand;
                SelectedCar.Model   = editedCar.Model;
                SelectedCar.Year    = editedCar.Year;
                SelectedCar.Price   = editedCar.Price;
                SelectedCar.Mileage = editedCar.Mileage;
            }
        }
        // удалени
        private void DeleteCar()
        {
            if (SelectedCar != null)
                Cars.Remove(SelectedCar);
        }
        // показ деталей (подребнее)
        private void ShowDetails()
        {
            if (SelectedCar != null)
            {
                var detailsWindow = new CarDetailsWindow(SelectedCar);
                detailsWindow.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}