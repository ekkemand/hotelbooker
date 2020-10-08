using ee.itcollege.ekmand.Contracts.DAL.Base.Repositories;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    
    public interface IUserRepository : IBaseRepository<AppUser>, IUserRepositoryCustom
    {
        
    }
}