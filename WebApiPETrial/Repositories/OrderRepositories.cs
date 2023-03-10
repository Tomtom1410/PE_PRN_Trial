using BussinessLogic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiPETrial.Models;
using WebApiPETrial.Repositories.Interface;

namespace WebApiPETrial.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private readonly PRN_Sum22_B1Context _dbContext;

        public OrderRepositories(PRN_Sum22_B1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDeleteDto> DeleteCustomer(Customer customer)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var result = new CustomerDeleteDto();
                var ordersByCustomer = await _dbContext.Orders.Where(x => x.CustomerId.Equals(customer.CustomerId)).ToListAsync();
                result.OrderDeleteCount = ordersByCustomer.Count();

                var totalOrderDetailDelete = 0;

                foreach (var order in ordersByCustomer)
                {
                    var orderDetails = await _dbContext.OrderDetails.Where(x => x.OrderId == order.OrderId).ToListAsync();
                    totalOrderDetailDelete += orderDetails.Count();
                    _dbContext.RemoveRange(orderDetails);
                }
                result.CustomerDeleteCount = 1;
                result.OrderDetailDeleteCount = totalOrderDetailDelete;

                _dbContext.RemoveRange(ordersByCustomer);
                _dbContext.Remove(customer);
                var response = await _dbContext.SaveChangesAsync() > 0 ? result : null;
                await transaction.CommitAsync();
                return response;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<List<OrderDetailsDto>> GetAllOrder()
        {
            List<Order> orders = await _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee).ThenInclude(e => e.Department)
                .ToListAsync();
            var result = new List<OrderDetailsDto>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDetailsDto()
                {
                    CustomerId = order.CustomerId,
                    OrderId = order.OrderId,
                    CustomerName = order.Customer?.CompanyName,
                    EmployeeId = (int)order.EmployeeId,
                    EmployeeName = order.Employee?.FirstName + " " + order.Employee?.LastName,
                    EmployeeDepartmentId = (int)order.Employee?.DepartmentId,
                    EmployeeDepartmentName = order.Employee?.Department.DepartmentName,
                    OrderDate = order.OrderDate.ToString(),
                    RequiredDate = order.RequiredDate.ToString(),
                    ShippedDate = order.ShippedDate.ToString(),
                    Freight = (double)order.Freight,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipCity = order.ShipCity,
                    ShipConutry = order.ShipCity,
                    ShipRegion = order.ShipRegion,
                    ShipPostalCode = order.ShipPostalCode
                };
                result.Add(orderDto);
            }
            return result;
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId));
        }

        public async Task<List<OrderDetailsDto>> GetOrderByDate(DateTime? from, DateTime? to)
        {
            List<Order> orders = await _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Employee).ThenInclude(e => e.Department)
                .Where(x => x.OrderDate >= from && x.OrderDate <= to)
                .ToListAsync();
            var result = new List<OrderDetailsDto>();
            foreach (var order in orders)
            {
                var orderDto = new OrderDetailsDto()
                {
                    CustomerId = order.CustomerId,
                    OrderId = order.OrderId,
                    CustomerName = order.Customer?.CompanyName,
                    EmployeeId = (int)order.EmployeeId,
                    EmployeeName = order.Employee?.FirstName + " " + order.Employee?.LastName,
                    EmployeeDepartmentId = (int)order.Employee?.DepartmentId,
                    EmployeeDepartmentName = order.Employee?.Department.DepartmentName,
                    OrderDate = order.OrderDate.ToString(),
                    RequiredDate = order.RequiredDate.ToString(),
                    ShippedDate = order.ShippedDate.ToString(),
                    Freight = (double)order.Freight,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipCity = order.ShipCity,
                    ShipConutry = order.ShipCity,
                    ShipRegion = order.ShipRegion,
                    ShipPostalCode = order.ShipPostalCode
                };
                result.Add(orderDto);
            }
            return result;
        }
    }
}
