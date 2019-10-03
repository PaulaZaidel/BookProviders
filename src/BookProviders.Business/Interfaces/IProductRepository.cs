
using BookProviders.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProviders.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCaterer(Guid catererId);
        Task<IEnumerable<Product>> GetProductsAndCaterers();
        Task<Product> GetProductAndCaterer(Guid id);
    }
}
