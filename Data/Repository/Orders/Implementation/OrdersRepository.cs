using Microsoft.EntityFrameworkCore;
using NorthWindApiSample.Data.Database.Models;
using NorthWindApiSample.Data.Repository.Orders.Dto;
using NorthWindApiSample.Data.Repository.Orders.Filters;
using NorthWindApiSample.Data.Repository.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Data.Repository.Orders.Implementation
{
    public class OrdersRepository : IOrdersRepository
    {

        protected NorthWindContext context;

        public OrdersRepository(NorthWindContext context)
        {
            this.context = context;
        }

        public async Task<OrderStatisticsDto> GetOrdersStatistics(OrderStatisticsFilter? filter = null, int? page = 0, int? perPage = 20)
        {
            IQueryable<Order> query = context.Orders
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product).ThenInclude(x => x.Supplier)
                .Include(x => x.Customer)
                ;

            query = Filter(query, filter);

            IQueryable<OrderInfo> orders = query.Select(x => new OrderInfo
            {
                Id = x.OrderId,
                DateOrdered = x.OrderDate,
                Items = x.OrderDetails.Select(d => new OrderItem
                {
                    OrderId = d.OrderId,
                    ProductId = d.ProductId,
                    Name = d.Product.ProductName,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    Supplier = new Dto.Supplier
                    {
                        Id = d.Product.SupplierId,
                        Name = d.Product.Supplier.CompanyName
                    }
                })
                
            });

            return new OrderStatisticsDto
            {
                TotalCount = await orders.LongCountAsync(),
                Orders = await orders.Skip(page.GetValueOrDefault(0) * perPage.GetValueOrDefault(0))
                .Take(perPage.GetValueOrDefault(20)).ToListAsync()
            };
        }


        protected IQueryable<Order> Filter(IQueryable<Order> query, OrderStatisticsFilter? filter = null)
        {
            if(filter == null)
            {
                return query;
            }


            if(filter.DateFrom != null)
            {
                query = query.Where(x => x.OrderDate >= filter.DateFrom);
            }

            if(filter.DateTo != null)
            {
                query = query.Where(x => x.OrderDate <= filter.DateTo);
            }

            

            if(filter.UserName != null)
            {
                query = query.Where(x => x.Customer.CompanyName.StartsWith(filter.UserName));
            }


            return query;
        }


        protected IQueryable<Order> OrderBy(IQueryable<Order> query, OrderStatisticsSortingOrder? orderBy = null)
        {
            if(orderBy == null)
            {
                return query.OrderBy(x => x.OrderDate);
            }

            switch (orderBy.Value)
            {
                case OrderStatisticsSortingOrder.DateAsc:
                    return query.OrderBy(x => x.OrderDate);

                case OrderStatisticsSortingOrder.DateDesc:
                    return query.OrderByDescending(x => x.OrderDate);

                case OrderStatisticsSortingOrder.UserNameAsc:
                    return query.OrderBy(x => x.Customer.CompanyName);

                case OrderStatisticsSortingOrder.UserNameDesc:
                    return query.OrderByDescending(x => x.Customer.CompanyName);

                default:
                    return query.OrderBy(x => x.OrderDate);
            }
        }

    }
}
