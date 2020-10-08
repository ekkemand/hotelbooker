﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class HotelConvenienceMapper : DALMapper<HotelConvenience, DALAppDTO.HotelConvenience>
    {
        public HotelConvenienceMapper()
        {
            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();
            
            MapperConfigurationExpression.CreateMap<Convenience, DALAppDTO.Convenience>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Convenience, Convenience>();
            
            MapperConfigurationExpression.CreateMap<ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, ConvenienceGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}