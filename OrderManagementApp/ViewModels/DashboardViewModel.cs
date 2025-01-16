using OrderManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementApp.Models;

namespace OrderManagementApp.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly FileWatcherService _fileWatcherService = new();
        private readonly string _orderDirectory = "D:\\Orders";

        public ObservableCollection<Order> Orders { get; private set; } = new();

        public DashboardViewModel()
        {
            LoadOrders();
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}