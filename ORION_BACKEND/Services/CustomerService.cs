using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ORION_BACKEND.Context;
using ORION_BACKEND.DTO;
using ORION_BACKEND.Models;

namespace ORION_BACKEND.Services
{
    public interface ICustomerRepository 
    {
        Task<List<Customer>> GetList();
        Task<Customer?> GetById(int id);
        Task<ReturnModel> Insert(CustomerDTO model);
        Task<ReturnModel> Update(CustomerDTO model);
    }
    public class CustomerService : ICustomerRepository
    {
        private readonly OrionContext _context;
        public CustomerService(OrionContext context) 
        { 
            _context = context;
        }

        public async Task<List<Customer>> GetList()
        {
            var list = await _context.Customers.ToListAsync();
            return list;
        }

        public async Task<Customer?> GetById(int id)
        {
            var customer = await _context.Customers
                .SingleOrDefaultAsync(x=> x.Id == id);

            var address = await _context.CustomerAddresses.Where(x => x.CustomerId == id).ToListAsync();

            if(customer != null)
                customer.CustomerAddresses = address;
        
            return customer;
        }

        public async Task<ReturnModel> Insert(CustomerDTO model)
        {
            var response = new ReturnModel();
            IDbContextTransaction? transaction = null;
            try
            {
                if (_context.Database.CurrentTransaction == null)
                {
                    transaction = await _context.Database.BeginTransactionAsync();
                }

                var customer = new Customer()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                foreach(var address in model.Addresses)
                {
                    var cosAddress = new CustomerAddress()
                    {
                        CustomerId = customer.Id,
                        Name = address.Name,
                        Street = address.Street,
                        PostalCode = address.PostalCode,
                        City = address.City,    
                        Country = address.Country,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _context.CustomerAddresses.AddAsync(cosAddress);
                }

                await _context.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                response.Id = customer.Id;
            }
            catch(Exception ex)
            {
                if(transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                throw new Exception(ex.Message);
            }

            return response;
        }

        public async Task<ReturnModel> Update(CustomerDTO model)
        {
            var response = new ReturnModel();
            IDbContextTransaction? transaction = null;
            try
            {
                if (_context.Database.CurrentTransaction == null)
                {
                    transaction = await _context.Database.BeginTransactionAsync();
                }

                await _context.Customers.Where(x => x.Id == model.Id)
                    .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, model.Name)
                    .SetProperty(p => p.Description, model.Description));

                await _context.SaveChangesAsync();

                foreach (var address in model.Addresses)
                {

                    if(address.Id > 0)
                    {
                        await _context.CustomerAddresses.Where(x => x.Id == address.Id)
                            .ExecuteUpdateAsync(s => s
                            .SetProperty(p => p.Name, address.Name)
                            .SetProperty(p => p.Street, address.Street)
                            .SetProperty(p => p.PostalCode, address.PostalCode)
                            .SetProperty(p => p.City, address.City)
                            .SetProperty(p => p.Country, address.Country));

                    }
                    else
                    {
                        var cosAddress = new CustomerAddress()
                        {
                            CustomerId = model.Id,
                            Name = address.Name,
                            Street = address.Street,
                            PostalCode = address.PostalCode,
                            City = address.City,
                            Country = address.Country,
                            CreatedAt = DateTime.UtcNow
                        };

                        await _context.CustomerAddresses.AddAsync(cosAddress);
                    }
                    
                }

                await _context.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                response.Id = model.Id;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                throw new Exception(ex.Message);
            }

            return response;
        }
    }
}
