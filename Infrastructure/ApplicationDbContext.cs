using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "b70f0022-7ec7-4b94-a411-f918868b7588";
            var adminRoleId = "8de5be14-5181-40c6-9b46-8d1619b2a91a";
            var userRoleId = "672ed51d-a28d-457e-abe4-f39f4d4ee104";

            //Seed roles (User, Admin, Super Admin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Super Admin User
            var superAdminId = "525e0169-6a01-493a-b4d7-1615bccd364d";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "super_admin",
                NormalizedUserName = "super_admin".ToUpper()
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "123456789Aa.");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to Super Admin User
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

            //builder.Entity<Area>().HasData(
            //    new Area
            //    {
            //        Id = 1,
            //        Description = "Header"
            //    },
            //    new Area
            //    {
            //        Id = 2,
            //        Description = "Body"
            //    },
            //    new Area
            //    {
            //        Id = 3,
            //        Description = "Footer"
            //    }
            //);
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }

        public DbSet<FileManagement> FileManagement { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleAccessBanned> RoleAccessBanned { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public DbSet<AudioConfigs> AudioConfigs { get; set; }
        public DbSet<DocumentConfigs> DocumentConfigs { get; set; }
        public DbSet<ImageConfigs> ImageConfigs { get; set; }
        public DbSet<VideoConfigs> VideoConfigs { get; set; }

        public DbSet<AudioSettings> AudioSettings { get; set; }
        public DbSet<DocumentSettings> DocumentSettings { get; set; }
        public DbSet<ImageSettings> ImageSettings { get; set; }
        public DbSet<VideoSettings> VideoSettings { get; set; }

    }
}
