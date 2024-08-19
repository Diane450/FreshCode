using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.MiddleWare;
using FreshCode.Models;
using FreshCode.Repositories;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FreshCodeContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<PetsUseCase>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();

builder.Services.AddScoped<ShopUseCase>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();

builder.Services.AddScoped<UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<CreatePetUseCase>();
builder.Services.AddScoped<IEyesRepository, EyesRepository>();
builder.Services.AddScoped<IBodyRepository, BodyRepository>();
builder.Services.AddScoped<ClanUseCase>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<ClanUseCase>();
builder.Services.AddScoped<IClanRepository, ClanRepository>();

builder.Services.AddScoped<PurchaseUseCase>();

builder.Services.AddScoped<InventoryUseCase>();

builder.Services.AddScoped<VkLaunchParamsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Ќастраиваем Swagger дл€ добавлени€ заголовка Authorization
    c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter your custom authorization string",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Authorization"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    }
                },
                new string[] { }
            }});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<HeaderValidationMiddleware>();

app.Run();
