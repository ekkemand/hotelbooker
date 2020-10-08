﻿using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class PriceMapper : DALMapper<Price, DALAppDTO.Price>
    {
        public PriceMapper()
        {
            MapperConfigurationExpression.CreateMap<Campaign, DALAppDTO.Campaign>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Campaign, Campaign>();
            
            MapperConfigurationExpression.CreateMap<Hotel, DALAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, Hotel>();
            
            MapperConfigurationExpression.CreateMap<Person, DALAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, Person>();
            
            MapperConfigurationExpression.CreateMap<Product, DALAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, Product>();
            
            MapperConfigurationExpression.CreateMap<Currency, DALAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Currency, Currency>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}