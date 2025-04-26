using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using GM.ShopFlow.Order.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Infra.Data.Repositories;

public class CustomerRepository(OrderDbContext context) : ICustomerRepository
{
    private readonly OrderDbContext _context = context;

    private DbSet<Customer> Customers => _context.Customers;

    public async Task CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        await Customers.AddAsync(customer, cancellationToken);
    }

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return customer is null ? 
            throw new InvalidDataException($"There is not customer with id: {id}") : 
            customer;
    }
}
