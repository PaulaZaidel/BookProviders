
using System;
using System.Threading.Tasks;
using BookProviders.Business.Interfaces;
using BookProviders.Business.Models;
using BookProviders.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookProviders.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(BookProvidersContext context) : base(context) {}

        public async Task<Address> GetAddressByCaterer(Guid catererId)
        {
            return await Context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.CatererId == catererId);
        }
    }
}
