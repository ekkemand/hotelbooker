using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class OwnerCompanyRepository : EFBaseRepository<AppDbContext, AppUser, OwnerCompany,
            DAL.App.DTO.OwnerCompany>,
        IOwnerCompanyRepository
    {
        public OwnerCompanyRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<OwnerCompany, DAL.App.DTO.OwnerCompany>())
        {
        }
    }
}