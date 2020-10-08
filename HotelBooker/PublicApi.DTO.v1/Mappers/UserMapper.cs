using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class UserMapper : BaseMapper<BLLAppDTO.Identity.AppUser, Identity.AppUser>
    {
        public UserMapper()
        {
            MapperConfigurationExpression.CreateMap<Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Person, Person>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}