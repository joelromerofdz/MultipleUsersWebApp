using API.Middleware;
using Application.Services;
using Application.Services.Interfaces.Country;
using Application.Services.Interfaces.ICityService;
using Application.Services.Interfaces.Province;
using Application.Services.Interfaces.Users;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Shared;
using Shared.Dtos.City;
using Shared.Dtos.Country;
using Shared.Dtos.Province;
using Shared.Dtos.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IMultipleUserDbContext, MultipleUserDbContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    return new MultipleUserDbContext(connectionString);
});

builder.Services.AddScoped<IRepository<User, UserResponse>, UserRepository>();
builder.Services.AddScoped<ILoginRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRepository<Country, CountryResponse>, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddScoped<IRepository<Province, ProvinceResponse>, ProvinceRepository>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();

builder.Services.AddScoped<IRepository<City, CityResponse>, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
