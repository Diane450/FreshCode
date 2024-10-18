using FreshCode.DbModels;
using FreshCode.Fabrics;
using FreshCode.Filters;
using FreshCode.Hubs;
using FreshCode.Interfaces;
using FreshCode.MiddleWare;
using FreshCode.Repositories;
using FreshCode.Services;
using FreshCode.Settings;
using FreshCode.UseCases;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<BattleStateFilter>();
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FreshCodeContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSignalR();

builder.Services.AddScoped<PetsUseCase>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();

builder.Services.AddScoped<ShopUseCase>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();

builder.Services.AddScoped<UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<PetPartsUseCase>();
builder.Services.AddScoped<IEyesRepository, EyesRepository>();
builder.Services.AddScoped<IBodyRepository, BodyRepository>();
builder.Services.AddScoped<ClanUseCase>();
builder.Services.AddScoped<TransactionRepository>();

builder.Services.AddScoped<IClanRepository, ClanRepository>();
builder.Services.AddScoped<IArtifactRepository, ArtifactRepository>();

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddScoped<PurchaseUseCase>();

builder.Services.AddScoped<BlogUseCase>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

builder.Services.AddScoped<BannerUseCase>();
builder.Services.AddScoped<IBanerRepository, BanerRepository>();
builder.Services.AddScoped<ArtifactDropService>();
builder.Services.AddScoped<FortuneWheelBonusDropService>();
builder.Services.AddScoped<IArtifactService, ArtifactService>();
builder.Services.AddScoped<IFortuneRepository, FortuneWheelRepository>();
builder.Services.AddScoped<IBonusRepository, BonusRepository>();
builder.Services.AddScoped<FortuneWheelUseCase>();
builder.Services.AddScoped<IPetBonusManagerService, PetBonusManagerService>();
builder.Services.AddScoped<IPetLoggerService, PetLoggerService>();
builder.Services.AddScoped<IBackgroundRepository, BackgroundRepository>();

builder.Services.AddScoped<BattleUseCase>();
builder.Services.AddScoped<IBattleRepository, BattleRepository>();
builder.Services.AddScoped<BattleService>();

builder.Services.AddHostedService<SleepDepletionService>();
builder.Services.AddHostedService<PetDecreasedSatietyService>();
builder.Services.AddHostedService<PetWakeupService>();

builder.Services.AddScoped<VkLaunchParamsService>();

builder.Services.Configure<VkApiSettings>(builder.Configuration.GetSection("VkApi"));

builder.Services.AddHttpClient<VkApiService>((httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://api.vk.com/method");

    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

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
app.MapHub<BattleHub>("battle-hub");

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<HeaderValidationMiddleware>();

app.Run();
