using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Data.Repository.Orders.Dto
{

    public class Supplier
    {
        public long? Id { get; set; }

        public string? Name { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }

        public long Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public Supplier Supplier { get; set; }
    }

    public class OrderInfo
    {
        public long Id { get; set; }
        public DateTime? DateOrdered { get; set; }

        public Customer User { get; set; }

        

        public IEnumerable<OrderItem> Items { get; set; }
    }

    public class OrderStatisticsDto
    {

        public List<OrderInfo> Orders { get; set; }

        public long TotalCount { get; set; }

    }
}
