using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderManagementApp.Models;


namespace OrderManagementApp.Services
{
    public class FileWatcherService
    {
        private FileSystemWatcher _fileWatcher;

        // JSON 파일에서 모든 주문 읽기
        public List<Order> LoadOrders(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Directory not found: {path}");

            var orders = new List<Order>();
            var files = Directory.GetFiles(path, "*.json");

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                var order = JsonConvert.DeserializeObject<Order>(json);
                if (order != null)
                    orders.Add(order);
            }

            return orders;
        }
    }
}
