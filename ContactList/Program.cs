using ContactList.Application.Interfaces;
using ContactList.Application.UseCases;
using ContactList.Infrastructure.Services;
using ContactList.Infrastructure.Persistence;
using ContactList.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

builder.Services.AddScoped<CreateContactUseCase>();
builder.Services.AddScoped<UpdateContactUseCase>();
builder.Services.AddScoped<GetAddressDetailsByCepQuery>();


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
