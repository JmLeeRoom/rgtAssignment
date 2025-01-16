using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public int TableNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        private static readonly string[] Statuses = { "Pending", "In Progress", "Completed" };
        private static readonly Random _random = new Random();

        private string _status = Statuses[_random.Next(Statuses.Length)];

        public string Status
        {
            get => _status;
            set => _status = value;
        }
    }

    
}
