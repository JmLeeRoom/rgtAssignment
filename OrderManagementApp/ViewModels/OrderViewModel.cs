using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementApp.Models;


namespace OrderManagementApp.ViewModels
{
    public class OrderViewModel
    {
        public required Order Order { get; set; }

        public string Display => $"{Order.OrderId} - {Order.TableNumber} (Status: {Order.Status})";
    }
}
