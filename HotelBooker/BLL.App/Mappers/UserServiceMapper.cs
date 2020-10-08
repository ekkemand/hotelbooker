using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class UserServiceMapper : BLLMapper<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>, IUserServiceMapper
    {
        public UserServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Person, DALAppDTO.Person>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}