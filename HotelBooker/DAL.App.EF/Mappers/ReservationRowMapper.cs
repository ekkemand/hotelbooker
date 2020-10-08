﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ReservationRowMapper : DALMapper<ReservationRow, DALAppDTO.ReservationRow>
    {
        public ReservationRowMapper()
        {
            MapperConfigurationExpression.CreateMap<Reservation, DALAppDTO.Reservation>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Reservation, Reservation>();

            MapperConfigurationExpression.CreateMap<Product, DALAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, Product>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}