namespace BussinessLogic
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public string? EmployeeDepartmentName { get; set; }
        public string? OrderDate { get; set; }
        public string? RequiredDate { get; set; }
        public string? ShippedDate { get; set; }
        public double Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipConutry { get; set; }

    }
}