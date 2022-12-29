using Zip.Installments.API.Extensions.Logging;
using Zip.Installments.API.Extensions.Swagger;
using Zip.Installments.DAL.Extensions;
using Zip.Installments.Middleware.Exceptions;
using Zip.InstallmentsService.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddAppLogging(config);
builder.Services.AddInfrastructure(config);
builder.Services.AddServiceExtensions();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    // Disable automatic model state validation by using and use only fluent validation
    options.SuppressModelStateInvalidFilter = true;
});

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
app.UseMiddleware<FluentExceptionMiddleware>();
app.UseHttpsRedirection(); 

app.MapControllers();

app.Run();
