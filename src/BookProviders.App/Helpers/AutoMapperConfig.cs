using AutoMapper;
using BookProviders.App.ViewModels;
using BookProviders.Business.Models;

namespace BookProviders.App.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Caterer, CatererViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModels>().ReverseMap();
        }
    }
}
