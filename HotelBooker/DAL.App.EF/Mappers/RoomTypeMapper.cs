﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class RoomTypeMapper : DALMapper<RoomType, DALAppDTO.RoomType>
    {
        public RoomTypeMapper()
        {
            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();

            MapperConfigurationExpression.CreateMap<ImageOfRoom, DALAppDTO.ImageOfRoom>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ImageOfRoom, ImageOfRoom>();

            MapperConfigurationExpression.CreateMap<Review, DALAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, Review>();

            MapperConfigurationExpression.CreateMap<ReviewCategory, DALAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, ReviewCategory>();

            MapperConfigurationExpression.CreateMap<Product, DALAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, Product>();

            MapperConfigurationExpression.CreateMap<Room, DALAppDTO.Room>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Room, Room>();

            MapperConfigurationExpression.CreateMap<Reservation, DALAppDTO.Reservation>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Reservation, Reservation>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}