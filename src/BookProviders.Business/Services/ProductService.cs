using BookProviders.Business.Interfaces;
using BookProviders.Business.Models;
using BookProviders.Business.Validations;
using System;
using System.Threading.Tasks;

namespace BookProviders.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo, INotifier notifier) : base(notifier)
        {
            _repo = repo;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _repo.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _repo.Update(product);
        }

        public async Task Remove(Guid id)
        {
            await _repo.Remove(id);
        }

        public void Dispose()
        {
            _repo?.Dispose();
        }
    }
}
