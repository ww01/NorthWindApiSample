using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Data.Repository.Orders.Filters
{

    public enum OrderStatisticsSortingOrder
    {
        DateAsc = 0, 
        DateDesc = 1, 
        UserNameAsc = 2, 
        UserNameDesc = 3
    }

    public class OrderStatisticsFilter
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string? UserName { get; set; }

        public OrderStatisticsSortingOrder? SortingOrder { get; set; } = null;
    }
}
