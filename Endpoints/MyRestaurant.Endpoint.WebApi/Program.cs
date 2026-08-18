using MyRestaurant.Application._ConfigurationExtensions;
using MyRestaurant.Application.Query._ConfigurationExtensions;
using MyRestaurant.DomainService._ConfigurationExtension;
using MyRestaurant.EF._ConfigurationExtensions;
using MyRestaurant.EF.Read._ConfigurationExtensions;
using MyRestaurant.Endpoint.WebApi.ExceptionHandlers;
using MyRestaurant.Endpoint.WebApi.Middlewares;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.HttpContext;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
// Learn more about configuring Open API at https://aka.ms/aspnet/openapi
// FUTURE PHASES: Authentication
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IContextAccessor, ContextAccessor>();
builder.Services.AddSingleton<ITimestampIdGenerator, TimestampIdGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services
    .AddRestaurantContext(builder.Configuration)
    .AddRestaurantQueryContext(builder.Configuration)
    .AddRepositories()
    .AddQueryRepositories()
    .AddCommandHandlers()
    .AddQueryHandlers()
    .AddDomainServices()
    .AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("X-Total-Count", "X-Page-Index", "X-Page-Size", "X-Total-Pages");
    });
});
builder.Services.AddExceptionHandler<RichExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// And in your pipeline

//builder.Services.AddSwaggerUI();
var app = builder.Build();

app.UseExceptionHandler(new ExceptionHandlerOptions()
{
    AllowStatusCode404Response = true
});
app.UseMiddleware<RequestResponseLoggingMiddleware>();
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
