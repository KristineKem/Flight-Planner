using AutoMapper;
using FlightPlanner;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validation;
using FlightPlanner.Data;
using FlightPlanner.Handlers;
using FlightPlanner.Services;
using FlightPlanner.Services.Validations.FlightValidations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddDbContext<FlightPlannerDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

builder.Services.AddTransient<IFlightPlannerDbContext, FlightPlannerDbContext>();

builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IAirportService, AirportService>();

builder.Services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());

builder.Services.AddScoped<IFlightValidate, AirportValidator>();
builder.Services.AddScoped<IFlightValidate, AirportValuesValidator>(); 
builder.Services.AddScoped<IFlightValidate, AppropriateDateValidator>();
builder.Services.AddScoped<IFlightValidate, CarrierValidator>();
builder.Services.AddScoped<IFlightValidate, FlightTimeValidator>();
builder.Services.AddScoped<IFlightValidate, FlightValidator>();
builder.Services.AddScoped<IFlightValidate, SameAirportCodeValidator>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
