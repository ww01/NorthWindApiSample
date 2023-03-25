using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Data.Repository.Orders.Filters
{

    public enum OrderStatisticsSortingOrder
    {
        DateAsc, DateDesc, UserNameAsc, UserNameDesc
    }

    public class OrderStatisticsFilter
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string? UserName { get; set; }

        public OrderStatisticsSortingOrder? SortingOrder { get; set; };
    }
}
