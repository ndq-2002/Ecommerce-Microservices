using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ecommerce.Carts.Infrastructure.AutofacModules;
using ECommerce.Carts.Domain.IServices;
using ECommerce.Carts.Infrastructure.AutofacModules;
using ECommerce.Carts.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

// Config Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ApplicationModule(configuration.GetConnectionString("CatalogConnectionString"), configuration.GetConnectionString("OrderConnectionString"))));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ValidationModule()));
// Add services to the container.

builder.Services.AddControllers();

// Config Redis
var redisHost = configuration["Redis:Host"];
var redisPort = configuration["Redis:Port"];
var redisConnection = $"{redisHost}:{redisPort}";
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
