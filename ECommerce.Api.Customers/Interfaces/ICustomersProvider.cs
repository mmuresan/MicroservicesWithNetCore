using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customer, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
