﻿using AutoMapper;
using Domain.App;
using Domain.App.Identity;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ReviewMapper : DALMapper<Review, DALAppDTO.Review>
    {
        public ReviewMapper()
        {
            MapperConfigurationExpression.CreateMap<AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, AppUser>();

            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();
            
            MapperConfigurationExpression.CreateMap<RoomType, DALAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, RoomType>();
            
            MapperConfigurationExpression.CreateMap<ReviewCategory, DALAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, ReviewCategory>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}