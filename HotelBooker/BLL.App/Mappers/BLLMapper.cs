using AutoMapper;
using ee.itcollege.ekmand.BLL.Base.Mappers;
using DALAppDTOUser = DAL.App.DTO.Identity;
using BLLAppDTOUser = BLL.App.DTO.Identity;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper()
        { 
            // AppUser
            MapperConfigurationExpression.CreateMap<DALAppDTOUser.AppUser, BLLAppDTOUser.AppUser>();
            MapperConfigurationExpression.CreateMap<BLLAppDTOUser.AppUser, DALAppDTOUser.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}