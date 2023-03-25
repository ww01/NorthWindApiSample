using NorthWindApiSample.Data.Repository.Orders.Dto;
using NorthWindApiSample.Data.Repository.Orders.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Data.Repository.Orders.Models
{
    public interface IOrdersRepository
    {

        Task<OrderStatisticsDto> GetOrdersStatistics(OrderStatisticsFilter? filter = null, int? page = 0, int? perPage = 20);
    }
}
