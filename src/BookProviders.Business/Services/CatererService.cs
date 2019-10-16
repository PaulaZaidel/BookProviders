using BookProviders.Business.Interfaces;
using BookProviders.Business.Models;
using BookProviders.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookProviders.Business.Services
{
    public class CatererService : BaseService, ICatererService
    {
        private readonly ICatererRepository _repoCaterer;
        private readonly IAddressRepository _repoAddres;

        public CatererService(ICatererRepository repoCaterer, IAddressRepository repoAddres, INotifier notifier)
            : base(notifier)
        {
            _repoCaterer = repoCaterer;
            _repoAddres = repoAddres;
        }

        public async Task Add(Caterer caterer)
        {
            if (!ExecuteValidation(new CatererValidation(), caterer) ||
                !ExecuteValidation(new AddressValidation(), caterer.Address))
                return;

            if (_repoCaterer.Search(c => c.Document == caterer.Document).Result.Any())
            {
                Notify("A caterer with this document already exists.");
                return;
            }

           await _repoCaterer.Add(caterer);
        }

        public async Task Update(Caterer caterer)
        {
            if (!ExecuteValidation(new CatererValidation(), caterer))
                return;

            if (_repoCaterer.Search(c => c.Document == caterer.Document && c.Id != caterer.Id).Result.Any())
            {
                Notify("A caterer with this document already exists.");
                return;
            }

            await _repoCaterer.Update(caterer);

        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _repoAddres.Update(address);

        }

        public async Task Remove(Guid id)
        {
            if (_repoCaterer.GetCatererAddressAndProducs(id).Result.Products.Any())
            {
                Notify("A caterer has registered products.");
                return;
            }

            await _repoCaterer.Remove(id);
        }

        public void Dispose()
        {
            _repoCaterer?.Dispose();
            _repoAddres?.Dispose();
        }
    }
}
