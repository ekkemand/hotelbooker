﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProductGroupMapper : DALMapper<ProductGroup, DALAppDTO.ProductGroup>
    {
        public ProductGroupMapper()
        {
            MapperConfigurationExpression.CreateMap<ProductGroup, DALAppDTO.ProductGroup>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductGroup, ProductGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}