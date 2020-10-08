﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class RoomTypeConvenienceMapper : DALMapper<RoomTypeConvenience, DALAppDTO.RoomTypeConvenience>
    {
        public RoomTypeConvenienceMapper()
        {
            MapperConfigurationExpression.CreateMap<RoomType, DALAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, RoomType>();

            MapperConfigurationExpression.CreateMap<ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, ConvenienceGroup>();

            MapperConfigurationExpression.CreateMap<Convenience, DALAppDTO.Convenience>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Convenience, Convenience>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}