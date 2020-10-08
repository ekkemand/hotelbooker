using Contracts.DAL.App.Repositories;
using BLL.App.DTO.Identity;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IUserService : IBaseEntityService<AppUser>, IUserRepositoryCustom<AppUser>
    {
        
    }
}