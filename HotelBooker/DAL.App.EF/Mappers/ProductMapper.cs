﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProductMapper : DALMapper<Product, DALAppDTO.Product>
    {
        public ProductMapper()
        {
            MapperConfigurationExpression.CreateMap<RoomType, DALAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, RoomType>();

            MapperConfigurationExpression.CreateMap<ProductGroup, DALAppDTO.ProductGroup>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductGroup, ProductGroup>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}