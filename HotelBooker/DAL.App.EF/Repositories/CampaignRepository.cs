using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Domain.App.Identity;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class CampaignRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Campaign, DAL.App.DTO.Campaign>,
        ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Campaign, DAL.App.DTO.Campaign>())
        {
        }
    }
}