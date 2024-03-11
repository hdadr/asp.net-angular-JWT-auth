using AutoMapper;
using backend.APIs.auth;
using backend.Features.users;
using backend.Models.Entities;
using backend.Models.ViewModels;
using System;

namespace backend.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserViewModel>();
            CreateMap<CreateUserRequest, AppUser>();
            CreateMap<RefreshToken, RefreshTokenViewModel>()
                .ForMember(rt => rt.Created, opt => opt.MapFrom(rtvm => ((DateTimeOffset)rtvm.Created).ToUnixTimeMilliseconds()))
                .ForMember(rt => rt.Expires, opt => opt.MapFrom(rtvm => ((DateTimeOffset)rtvm.Expires).ToUnixTimeMilliseconds()));
        }

    }
}
