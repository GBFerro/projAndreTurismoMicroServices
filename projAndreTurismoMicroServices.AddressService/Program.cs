using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using projAndreTurismoApp.AddressService.Data;
using projAndreTurismoApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<projAndreTurismoAppAddressServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("projAndreTurismoAppAddressServiceContext") ?? throw new InvalidOperationException("Connection string 'projAndreTurismoAppAddressServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PostOfficesService>();

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
