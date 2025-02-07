using AutoMapper;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Project.Application.DTOs.Course;
using Project.Application.DTOs.News;
using Project.Application.DTOs.NewsCategory;
using Project.Application.DTOs.Personnel;
using Project.Application.DTOs.RequestPermission;


namespace Project.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            //#region User
            //CreateMap<User, UserDTO>().ReverseMap();
            //CreateMap<User, WrestlingClubDTO>()
            //      .ForMember(dest => dest.CompleteTheProfile, opt => opt.MapFrom(src => (
            //      src.ClubName == null &&
            //      src.ClubRegistrationNumber == null &&
            //      src.ClubAddress == null &&
            //      src.NationalCodeFile == null &&
            //      src.NationalCode == null &&
            //      src.FirstName == null &&
            //      src.LastName == null &&
            //      src.BirthDate == null
            //      ) ? false : true))
            //      .ReverseMap();
            //CreateMap<User, PlayerDTO>()
            //      .ForMember(dest => dest.CompleteTheProfile, opt => opt.MapFrom(src => (
            //      src.ShipStyle == null &&
            //      src.NationalCodeFile == null &&
            //      src.NationalCode == null &&
            //      src.FirstName == null &&
            //      src.LastName == null &&
            //      src.BirthDate == null
            //      ) ? false : true))
            //      .ReverseMap();
            //CreateMap<User, VerifyUserDTO>().ReverseMap();
            //CreateMap<User, TotalUserDTO>()
            //      .ForMember(dest => dest.CompleteTheProfile, opt => opt.MapFrom(src => (
            //      src.ShipStyle == null &&
            //      src.NationalCodeFile == null &&
            //      src.NationalCode == null &&
            //      src.FirstName == null &&
            //      src.LastName == null &&
            //      src.BirthDate == null
            //      ) ? false : true)).ReverseMap();
            //CreateMap<User, UpsertUserProfile>().ReverseMap();
            //CreateMap<User, UpsertPlayer>().ReverseMap();
            //CreateMap<User, UpsertWrestlingClub>().ReverseMap();
            //CreateMap<MedalsHistory, MedalDTO>().ReverseMap();
            //#endregion

            //#region Admin
            //CreateMap<Admin, AdminDTO>().ForMember(dest => dest.PermissionToEdit, opt => opt.MapFrom(src => (src.PermissionToEditDate != null && src.PermissionToEditDate > DateTime.Now) ? true : false)).ReverseMap();
            //CreateMap<AdminRole, AdminRoleDTO>().ReverseMap();
            //CreateMap<Role, RoleDTO>().ReverseMap();
            //CreateMap<Admin, UpsertAdminProfile>().ReverseMap();
            //CreateMap<Admin, CreateAdmin>().ReverseMap();
            //#endregion

            //#region News Categroy
            //CreateMap<NewsCategory, UpsertNewsCategory>().ReverseMap();
            //CreateMap<NewsCategory, NewsCategoryDTO>()
            //      .ForMember(dest => dest.NewsCount, opt => opt.MapFrom(src => src.News.Count()))
            //      .ReverseMap();
            //#endregion

            //#region News
            //CreateMap<News, NewsDTO>()
            //    .ReverseMap();
            //CreateMap<News, UpsertNews>()
            //    .ForMember(dest => dest.Time, opt => opt.Ignore())
            //    .ForMember(dest => dest.Date, opt => opt.Ignore())
            //    .ForMember(dest => dest.Image, opt => opt.Ignore())
            //    .ForMember(dest => dest.Url, opt => opt.Ignore())
            //    .ReverseMap();
            //#endregion

            //#region Course
            //CreateMap<Course, CourseDTO>()
            //    .AfterMap((src, dest, context) =>
            //    {
            //        // Use context.Items to pass in the value
            //        if (context.Items.ContainsKey("LikesNumberValue"))
            //        {
            //            dest.LikesNumber = int.Parse(context.Items["LikesNumberValue"].ToString());
            //        }
            //    }).ReverseMap();
            //CreateMap<Course, CourseDetailsDTO>()
            //    .AfterMap((src, dest, context) =>
            //    {
            //        // Use context.Items to pass in the value
            //        if (context.Items.ContainsKey("LikesNumberValue"))
            //        {
            //            dest.LikesNumber = int.Parse(context.Items["LikesNumberValue"].ToString());
            //        }
            //    }).ReverseMap();
            //CreateMap<CourseComment, CommentDTO>().ReverseMap();
            //#endregion

            //#region RequestPermission
            //CreateMap<RequestPermission, RequestPermissionDTO>()
            //    .ReverseMap();
            //CreateMap<RequestPermission, AddRequestPermission>()
            //    .ReverseMap();
            //#endregion
        }
    }
}
