using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ConvenienceRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Convenience, DAL.App.DTO.Convenience>,
        IConvenienceRepository
    {
        public ConvenienceRepository(AppDbContext dbContext) : base(dbContext,
            new ConvenienceMapper())
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Convenience>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.ConvenienceGroup)
                .OrderBy(o => o.Name);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Convenience> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(o => o.ConvenienceGroup)
                .FirstOrDefaultAsync(o => o.Id == id);
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}