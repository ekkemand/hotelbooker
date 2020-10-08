using AutoMapper;
using Domain.App;
using Domain.App.Identity;
using ee.itcollege.ekmand.DAL.Base.Mappers;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper()
        {
            // AppUser
            MapperConfigurationExpression.CreateMap<AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}