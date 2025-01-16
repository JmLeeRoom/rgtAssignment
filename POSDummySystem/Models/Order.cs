using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSDummySystem.Models
{
    public class Order
    {
        public string OrderId { get; set; } // 주문 ID
        public int TableNumber { get; set; } // 테이블 번호
        public List<OrderItem> Items { get; set; } // 주문 항목
        public double TotalPrice { get; set; } // 총 가격
        public DateTime OrderTime { get; set; } // 주문 시간
    }

    public class OrderItem
    {
        public string Name { get; set; } // 상품 이름
        public int Quantity { get; set; } // 수량
    }
}
