using Application.DataProviders;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syfora_Test.Domain;
using XmlData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            var frontendUrl = builder.Configuration["FrontendUrl"];
            policy.WithOrigins(frontendUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
})
    .Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
    }); 

var provider = builder.Configuration["DataProvider:Provider"];

if (provider == "InMemory")
{
    builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("Syfora_Test_Db"));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
}
else if (provider == "Xml")
{
    builder.Services.AddScoped<IUserService, XmlUserService>();
    builder.Services.AddScoped<XmlUserReader>();
}
else if (provider == "MsSql")
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
