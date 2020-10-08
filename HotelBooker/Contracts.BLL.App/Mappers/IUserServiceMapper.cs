using ee.itcollege.ekmand.Contracts.BLL.Base.Mappers;
using BLLAppDTOIdentity=BLL.App.DTO.Identity;
using DALAppDTOIdentity=DAL.App.DTO.Identity;

namespace Contracts.BLL.App.Mappers
{
    public interface IUserServiceMapper: IBaseMapper<DALAppDTOIdentity.AppUser, BLLAppDTOIdentity.AppUser>
    {
        
    }
}