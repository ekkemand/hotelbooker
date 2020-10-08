﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class RoomMapper : DALMapper<Room, DALAppDTO.Room>
    {
        public RoomMapper()
        {
            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();

            MapperConfigurationExpression.CreateMap<RoomType, DALAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, RoomType>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}