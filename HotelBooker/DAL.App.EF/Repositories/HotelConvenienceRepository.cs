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
    public class HotelConvenienceRepository : EFBaseRepository<AppDbContext, AppUser,
            Domain.App.HotelConvenience, DAL.App.DTO.HotelConvenience>,
        IHotelConvenienceRepository
    {
        public HotelConvenienceRepository(AppDbContext dbContext) : base(dbContext,
            new HotelConvenienceMapper())
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.HotelConvenience>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Hotel)
                .Include(o => o.Convenience)
                .ThenInclude(o => o!.ConvenienceGroup);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.HotelConvenience> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(o => o.Hotel)
                .Include(o => o.Convenience)
                .ThenInclude(o => o!.ConvenienceGroup)
                .FirstOrDefaultAsync(o => o.Id == id);
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}