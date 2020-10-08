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
    public class PriceRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Price, DAL.App.DTO.Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext, new PriceMapper())
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Price>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Campaign)
                .Include(o => o.Hotel)
                .Include(o => o.Product)
                .Include(o => o.Currency)
                .OrderBy(o => o.Value);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Price> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(o => o.Campaign)
                .Include(o => o.Hotel)
                .Include(o => o.Product)
                .Include(o => o.Currency)
                .FirstOrDefaultAsync(o => o.Id == id);
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
    }
}