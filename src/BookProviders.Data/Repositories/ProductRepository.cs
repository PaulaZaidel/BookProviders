
using BookProviders.Business.Models;
using BookProviders.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BookProviders.Data.Context;

namespace BookProviders.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BookProvidersContext context) : base(context) {}

        public async Task<Product> GetProductAndCaterer(Guid id)
        {
            return await Context.Products.AsNoTracking()
                .Include(c => c.Caterer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCaterer(Guid catererId)
        {
            return await Search(p => p.CatererId == catererId);
        }

        public async Task<IEnumerable<Product>> GetProductsAndCaterers()
        {
            return await Context.Products.AsNoTracking()
                .Include(c => c.Caterer)
                .OrderBy(p => p.Name).ToListAsync();
        }
    }
}
