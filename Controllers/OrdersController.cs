using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWindApiSample.Api.Users.Orders.Request;
using NorthWindApiSample.Data.Repository.Orders.Filters;
using NorthWindApiSample.Data.Repository.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        protected ILogger<OrdersController> logger;
        protected IOrdersRepository ordersRepository;

        public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository)
        {
            this.logger = logger;
            this.ordersRepository = ordersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] OrdersLisRequest? request = null)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int page = request.Page.GetValueOrDefault(0);
            int perPage = request.PerPage.GetValueOrDefault(20);

            OrderStatisticsFilter? filter = null;

            if(request != null)
            {
                filter = new OrderStatisticsFilter();

              /*  OrderStatisticsSortingOrder? sort = null;

                if (!string.IsNullOrWhiteSpace(request.SortingOrder))
                {
                    sort = Enum.Parse<OrderStatisticsSortingOrder>(request.SortingOrder.Trim());
                }
              */

                filter.DateFrom = request.DateFrom;
                filter.DateTo = request.DateTo;
                filter.SortingOrder = request.SortingOrder;
                filter.UserName = request.UserName;
            }

            return new JsonResult( await ordersRepository.GetOrdersStatistics(filter, page, perPage));
        }

    }
}
