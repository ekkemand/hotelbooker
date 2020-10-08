﻿using AutoMapper;
using Domain.App;
using Domain.App.Identity;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ReservationMapper : DALMapper<Reservation, DALAppDTO.Reservation>
    {
        public ReservationMapper()
        {
            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();

            MapperConfigurationExpression.CreateMap<Person, DALAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, Person>();

            MapperConfigurationExpression.CreateMap<RoomType, DALAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, RoomType>();

            MapperConfigurationExpression.CreateMap<AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}