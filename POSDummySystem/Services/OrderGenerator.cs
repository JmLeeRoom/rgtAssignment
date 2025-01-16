using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POSDummySystem.Models;

namespace POSDummySystem.Services
{
    public class OrderGenerator
    {
        private readonly Random _random = new Random();

        // 1분 간격으로 주문 생성
        public async Task StartGeneratingOrders(string outputPath)
        {
            while (true)
            {
                int orderCount = _random.Next(1, 4); // 1~3개의 주문 생성
                for (int i = 0; i < orderCount; i++)
                {
                    var order = GenerateRandomOrder(); // 무작위 주문 생성
                    SaveOrderAsJson(order, outputPath); // JSON으로 저장
                }

                Console.WriteLine($"Generated {orderCount} orders.");
                await Task.Delay(TimeSpan.FromMinutes(1)); // 1분 대기
            }
        }

        // 무작위 주문 생성
        private Order GenerateRandomOrder()
        {
            var items = new List<OrderItem>
            {
                new OrderItem { Name = "Burger", Quantity = _random.Next(1, 3) },
                new OrderItem { Name = "Fries", Quantity = _random.Next(1, 3) },
                new OrderItem { Name = "Coke", Quantity = _random.Next(1, 2) }
            };

            return new Order
            {
                OrderId = $"ORD{_random.Next(1000, 9999)}",
                TableNumber = _random.Next(1, 10),
                Items = items,
                TotalPrice = CalculateTotalPrice(items),
                OrderTime = DateTime.Now
            };
        }

        // JSON으로 주문 저장
        private void SaveOrderAsJson(Order order, string outputPath)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath); // 디렉터리 생성

            string fileName = $"{order.OrderId}.json";
            string json = JsonConvert.SerializeObject(order, Formatting.Indented);

            File.WriteAllText(Path.Combine(outputPath, fileName), json);
            Console.WriteLine($"Order saved: {fileName}");
        }

        // 총 가격 계산
        private double CalculateTotalPrice(List<OrderItem> items)
        {
            double total = 0;
            foreach (var item in items)
            {
                total += item.Quantity * GetItemPrice(item.Name);
            }
            return total;
        }

        // 상품별 가격 반환
        private double GetItemPrice(string itemName)
        {
            return itemName switch
            {
                "Burger" => 10.0,
                "Fries" => 5.0,
                "Coke" => 2.0,
                _ => 0.0
            };
        }

        // JSON 파일 읽기
        public List<Order> LoadOrdersFromJson(string directoryPath)
        {
            var orders = new List<Order>();

            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

            // 모든 JSON 파일 읽기
            var files = Directory.GetFiles(directoryPath, "*.json");

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                var order = JsonConvert.DeserializeObject<Order>(json);

                if (order != null)
                {
                    orders.Add(order);
                    Console.WriteLine($"Order loaded: {order.OrderId}");
                }
            }

            return orders;
        }
    }
}