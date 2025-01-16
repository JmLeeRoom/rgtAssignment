using Newtonsoft.Json;
using OrderManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OrderManagementApp.Views;

using OrderManagementApp.Services;



namespace OrderManagementApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly FileWatcherService _fileWatcherService = new();
        private readonly string _orderDirectory = "D:\\Orders";

        public ObservableCollection<Order> Orders { get; private set; } = new();

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ShowOrderViewCommand { get; }
        public ICommand ShowDashboardViewCommand { get; }

        public MainViewModel()
        {
            ShowOrderViewCommand = new RelayCommand(ShowOrderView);
            ShowDashboardViewCommand = new RelayCommand(ShowDashboardView);

            LoadOrders(); // JSON 데이터 로드
            ShowOrderView(); // 기본 화면 설정
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _fileWatcherService.LoadOrders(_orderDirectory);
                Orders = new ObservableCollection<Order>(orders);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Orders.Clear();
            }
        }

        private void ShowOrderView() => CurrentView = new OrderView();
        private void ShowDashboardView()
        {
            CurrentView = new DashboardView
            {
                DataContext = new DashboardViewModel()
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute) => _execute = execute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();
    }
}
