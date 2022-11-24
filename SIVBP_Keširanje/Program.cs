using Microsoft.EntityFrameworkCore;
using SIVBP_Keširanje.Models;
using SIVBP_Keširanje.Repositories;
using SIVBP_Keširanje.Services;
using SIVBP_Keširanje.Subscribers;
using StackExchange.Redis;

var CorsRule = "_customCorsRule";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsRule,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
    ConnectionMultiplexer.Connect(builder.Configuration["CacheConnection"]));

builder.Services.AddDbContext<SIVBPContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowDatabase"),
            options => options.EnableRetryOnFailure()));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();

builder.Services.AddSingleton<ICacheRepository, CacheRepository>();

builder.Services.AddHostedService<UserEfficiencySubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsRule);

app.UseAuthorization();

app.MapControllers();

app.Run();
