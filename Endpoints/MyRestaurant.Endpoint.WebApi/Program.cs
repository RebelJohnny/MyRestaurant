using MyRestaurant.Application._ConfigurationExtensions;
using MyRestaurant.Application.Query._ConfigurationExtensions;
using MyRestaurant.Application.Query.Reports;
using MyRestaurant.EF._ConfigurationExtensions;
using MyRestaurant.EF.Read._ConfigurationExtensions;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.HttpContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Open API at https://aka.ms/aspnet/openapi
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
    .AddQueryHandlers()
    .AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddScoped<AllPersonnelDailyReservesReportService>();
//builder.Services.AddSwaggerUI();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
    });
}
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
