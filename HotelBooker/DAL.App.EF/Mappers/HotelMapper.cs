﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class HotelMapper : DALMapper<Hotel, DALAppDTO.Hotel>
    {
        public HotelMapper()
        {
            MapperConfigurationExpression.CreateMap<OwnerCompany, DALAppDTO.OwnerCompany>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OwnerCompany, OwnerCompany>();
            
            MapperConfigurationExpression.CreateMap<ImageOfRoom, DALAppDTO.ImageOfRoom>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ImageOfRoom, ImageOfRoom>();
            
            MapperConfigurationExpression.CreateMap<Review, DALAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, Review>();
             
            MapperConfigurationExpression.CreateMap<ReviewCategory, DALAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, ReviewCategory>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}