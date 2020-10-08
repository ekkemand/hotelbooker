using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Domain.App.Identity;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ConvenienceGroupRepository : EFBaseRepository<AppDbContext, AppUser,
        Domain.App.ConvenienceGroup, DAL.App.DTO.ConvenienceGroup>, IConvenienceGroupRepository
    {
        public ConvenienceGroupRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.ConvenienceGroup, DAL.App.DTO.ConvenienceGroup>())
        {
        }
    }
}