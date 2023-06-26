using Microsoft.EntityFrameworkCore;
using Payment.Api.Extensions;
using Payment.Application.Helper;
using Payment.Application.Interfaces.Repositories;
using Payment.Application.Interfaces.Services;
using Payment.Application.Services;
using Payment.Infrastructure.Persistence.DbContext;
using Payment.Infrastructure.Persistence.Repositories;
using Payment.Infrastructure.Services.Payment.Adyen.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyDbContext, MyDbContext>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("AppCnnString"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("AppCnnString"));
});

builder.Services.Configure<PaymentSettings>(builder.Configuration.GetSection("PaymentSettings"));
builder.Services.AddAutoMapper(typeof(ServiceBase).Assembly);
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddTransient<IAdyenClient, AdyenClient>();
builder.Services.AddPaymentProviderFactory();
builder.Services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();