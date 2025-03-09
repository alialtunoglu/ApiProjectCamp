using System.Reflection;
using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using ApiProjeKampi.WebApi.Services.ProductServices;
using ApiProjeKampi.WebApi.ValidationRules;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
