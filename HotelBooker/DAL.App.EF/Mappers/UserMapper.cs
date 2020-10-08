﻿﻿using AutoMapper;
using Domain.App;
using Domain.App.Identity;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class UserMapper : DALMapper<AppUser, DALAppDTO.Identity.AppUser>
    {
        public UserMapper()
        {
            MapperConfigurationExpression.CreateMap<Person, DALAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, Person>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}