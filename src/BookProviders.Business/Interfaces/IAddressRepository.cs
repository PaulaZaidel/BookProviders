
using BookProviders.Business.Models;
using System;
using System.Threading.Tasks;

namespace BookProviders.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByCaterer(Guid catererId);
    }
}
