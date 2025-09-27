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
                .AfterMap((src, dest, context) =>
                {
                    // Use context.Items to pass in the value
                    if (context.Items.ContainsKey("LikesNumberValue"))
                    {
                        dest.LikesNumber = int.Parse(context.Items["LikesNumberValue"].ToString());
                    }
                }).ReverseMap();

            //CreateMap<Product, ProductDTO>()
            //    .ForMember(dest => dest.MainImagePath,
            //        opt => opt.MapFrom(src => src.FileManagement != null ? src.FileManagement.path : null))
            //    .ForMember(dest => dest.OtherImagesPath,
            //        opt => opt.MapFrom(src => src.ProductImages != null
            //            ? src.ProductImages.Select(pi => pi.FileManagement.path).ToList()
            //            : new List<string>()))
            //    .AfterMap((src, dest, context) =>
            //    {
            //        if (context.Items.ContainsKey("LikesNumberValue"))
            //        {
            //            dest.LikesNumber = Convert.ToInt32(context.Items["LikesNumberValue"]);
            //        }
            //        else
            //        {
            //            dest.LikesNumber = src.ProductLikes?.Count ?? 0;
            //        }
            //    })
            //    .ReverseMap();


            CreateMap<ProductComment, UserDTO>()
                .ReverseMap();
        }
    }
}
