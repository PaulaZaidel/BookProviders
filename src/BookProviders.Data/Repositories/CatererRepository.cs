
using System;
using System.Threading.Tasks;
using BookProviders.Business.Interfaces;
using BookProviders.Business.Models;
using BookProviders.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookProviders.Data.Repositories
{
    public class CatererRepository : Repository<Caterer>, ICatererRepository
    {
        public CatererRepository(BookProvidersContext context) : base(context) {}

        public async Task<Caterer> GetCatererAddressAndProducs(Guid id)
        {
            return await Context.Caterers.AsNoTracking()
                .Include(c => c.Adress)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Caterer> GetCatererAndAddress(Guid id)
        {
            return await Context.Caterers.AsNoTracking()
                .Include(c => c.Adress)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
