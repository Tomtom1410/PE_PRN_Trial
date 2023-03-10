using BussinessLogic;
using WebApiPETrial.Models;

namespace WebApiPETrial.Repositories.Interface
{
    public interface IOrderRepositories
    {
        Task <CustomerDeleteDto> DeleteCustomer(Customer customer);
        Task <List<OrderDetailsDto>> GetAllOrder();
        Task <Customer> GetCustomerById(string customerId);
        Task<List<OrderDetailsDto>> GetOrderByDate(DateTime? from, DateTime? to);
    }
}
