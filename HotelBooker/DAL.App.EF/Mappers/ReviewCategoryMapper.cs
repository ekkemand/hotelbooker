﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ReviewCategoryMapper : DALMapper<ReviewCategory, DALAppDTO.ReviewCategory>
    {
        public ReviewCategoryMapper()
        {
            MapperConfigurationExpression.CreateMap<Review, DALAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, Review>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}