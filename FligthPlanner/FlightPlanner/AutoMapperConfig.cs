﻿using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAirportRequest, Airport>()
                    .ForMember(m => m.AirportCode, opt => opt.MapFrom(s => s.Airport))
                    .ForMember(d => d.Id, opt => opt.Ignore());
                cfg.CreateMap<Airport, AddAirportRequest>()
                    .ForMember(m => m.Airport, opt => opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<AddFlightRequest, Flight>();
                cfg.CreateMap<Flight, AddFlightRequest>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
