using AutoMapper;
using ItServiceApp.Core.Entities;
using ItServiceApp.Core.ViewModels;

namespace ItServiceApp.Core.MapperProfiles
{
    public class SubscriptionProfile :Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionType, SubscriptionTypeViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();

        }
    }
}
