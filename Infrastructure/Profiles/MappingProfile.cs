using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Shop;


namespace Project.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<IdentityUser, UserDTO>()
            //.ForMember(dest => dest.Roles, opt => opt.Ignore())
            //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
            //    src.Claims.FirstOrDefault(c => c.ClaimType == "FirstName")?.ClaimValue))
            //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
            //    src.Claims.FirstOrDefault(c => c.ClaimType == "LastName")?.ClaimValue));



            CreateMap<IdentityUser, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()) // Handle roles separately
                .ReverseMap(); // Optional: Map back from UserDTO to IdentityUser

            CreateMap<IdentityUser, AdminDTO>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()) // Handle roles separately
                .ReverseMap(); // Optional: Map back from UserDTO to IdentityUser

            CreateMap<Product, ProductDTO>()
                .ReverseMap(); // Optional: Map back from ProductDTO to Product
        }
    }
}
