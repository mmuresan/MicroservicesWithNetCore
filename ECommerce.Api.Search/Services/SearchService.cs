using ECommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync();
            if (ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? 
                            productsResult.Products.FirstOrDefault(productsResult => productsResult.Id == item.ProductId)?.Name :
                            "Product information is not available";
                    }
                    order.CustomerName = customersResult.IsSuccess ?
                        customersResult.Customers.FirstOrDefault(customersResult => customersResult.Id == order.CustomerId)?.Name :
                        "Customer information is not available";
                }

                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
