using APIStart.Business.Services;
using APIStart.Business.Services.Implementations;
using APIStart.Core.DTOs.EmployeeModelDTOs;
using APIStart.Core.Repositories;
using APIStart.Data.DAL;
using APIStart.Data.Repositories.Implementations;
using APIStart.MappingProfiles;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

                                
builder.Services.AddControllers().AddFluentValidation(opt=>
{
    opt.RegisterValidatorsFromAssembly(typeof(EmployeeCreateDtoValidator).Assembly);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
builder.Services.AddScoped<IEmployeeProfessionRepository, EmployeeProfessionRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProfessionService, ProfessionService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

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
