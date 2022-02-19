using AutoMapper;
using ItServiceApp.Core.Identity;
using ItServiceApp.Core.ViewModels;
using System;

namespace ItServiceApp.Core.MapperProfiles
{
    public class AccountProfile :Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserProfileViewModel>().ReverseMap(); //application userdan user profile cevirme
            //CreateMap<UserProfileViewModel, ApplicationUser>(); //reversemap kullandıgımız için gerek yok 
        }

    }
}
