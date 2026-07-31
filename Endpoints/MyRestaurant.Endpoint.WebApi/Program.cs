using MyRestaurant.Application._ConfigurationExtensions;
using MyRestaurant.Application.Query._ConfigurationExtensions;
using MyRestaurant.EF.ConfigurationExtensions;
using MyRestaurant.EF.Read.ConfigurationExtensions;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.HttpContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// FUTURE PHASES: Authentication
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ContextAccessor>();
builder.Services.AddSingleton<ITimestampIdGenerator, TimestampIdGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services
    .AddRestaurantContext(builder.Configuration)
    .AddRestaurantQueryContext(builder.Configuration)
    .AddRepositories()
    .AddQueryRepositories()
    .AddCommandHandlers()
    .AddQueryHandlers();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
//builder.Services.AddSwaggerUI();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Restaurant API");
});
app.UseCors(opt =>
{
    opt.SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
