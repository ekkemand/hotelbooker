using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;
using Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class CurrencyRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Currency, DAL.App.DTO.Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Currency, DAL.App.DTO.Currency>())
        {
        }
    }
}