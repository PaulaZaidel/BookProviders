
using BookProviders.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookProviders.Business.Interfaces
{
    public interface ICatererService : IDisposable
    {
        Task Add(Caterer caterer);
        Task Update(Caterer caterer);
        Task Remove(Guid id);
        Task UpdateAddress(Address address);
    }
}
