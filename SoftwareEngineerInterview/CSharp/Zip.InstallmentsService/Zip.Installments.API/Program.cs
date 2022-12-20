using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Zip.Installments.API.Extensions.Swagger;
using Zip.Installments.DAL.AppContext;
using Zip.Installments.DAL.Extensions;
using Zip.Installments.Validations.Controllers;
using Zip.InstallmentsService.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddInfrastructure(config);
builder.Services.AddServiceExtensions();

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<CreateOrdersValidator>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggApiVersioning();
builder.Services.AddSwagerApiVersionExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUISetup();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
