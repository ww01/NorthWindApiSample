using NorthWindApiSample.Data.Repository.Orders.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace NorthWindApiSample.Api.Users.Orders.Request
{
    public class OrdersLisRequest
    {
        [Range(0, int.MaxValue)]
        public int? Page { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int? PerPage { get; set; } = 20;
        public DateTime? DateFrom { get; set; } = null;

        public DateTime? DateTo { get; set; } = null;

        public string? UserName { get; set; } = null;

        public OrderStatisticsSortingOrder? SortingOrder { get; set; }
    }
}
