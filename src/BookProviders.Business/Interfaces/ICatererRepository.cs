
using BookProviders.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookProviders.Business.Interfaces
{
    public interface ICatererRepository : IRepository<Caterer>
    {
        Task<Caterer> GetCatererAndAddress(Guid id);
        Task<Caterer> GetCatererAddressAndProducs(Guid id);
    }
}
