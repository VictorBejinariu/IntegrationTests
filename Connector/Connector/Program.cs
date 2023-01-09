using Connector.Interfaces;
using Connector.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var config = builder.Configuration;

services.AddSingleton<IPaymentService, PaymentService>();
services.AddSingleton<IPaymentBuilder, PaymentBuilder>();
services.AddSingleton<IPaymentRepository, PaymentMsSqlRepository>();
services.AddSingleton<IPaymentRequestValidator, PaymentRequestValidator>();
services.AddSingleton<IGuidProvider, GuidProvider>();
services.AddHttpClient("Cashier", client =>
{
    client.BaseAddress = new Uri(config.GetValue<string>("CashierUrl"));
    client.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    client.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, $"IntTests-{Environment.MachineName}");
});

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