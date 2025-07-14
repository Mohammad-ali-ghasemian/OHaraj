using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using OHaraj.Infrastructure;
using OHaraj.Repositories;
using OHaraj.Services;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Middlewares;
using Project.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

// for Current()
builder.Services.AddHttpContextAccessor();

// Add services to the container.
// add database
builder.Services.AddDbContext<ApplicationDbContext>
    (
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbConnectionString")
        )
    );
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Configure Password
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Configure Identity cookie settings for API behavior
builder.Services.ConfigureApplicationCookie(options =>
{
    // Set cookie expiration and sliding expiration if needed
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.SlidingExpiration = true;

    // Instead of redirecting, return a 401 when not authenticated
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    // Instead of redirecting, return a 403 when access is forbidden
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

// Scopes
builder.Services.AddScoped<IFileStorageService, InAppStorageService>();

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// in the absent of cloud storage
app.UseStaticFiles();

app.MapControllers();

app.Run();
