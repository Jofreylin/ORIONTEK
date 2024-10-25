using ORION_BACKEND.Models;

namespace ORION_BACKEND.DTO
{
    public class CustomerDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<CustomerAddressDTO> Addresses { get; set; } = new List<CustomerAddressDTO>();
    }

    public class ReturnModel
    {
        public int? Id { get; set; }
    }
}
