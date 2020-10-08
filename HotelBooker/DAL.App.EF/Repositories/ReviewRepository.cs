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
    public class ReviewRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Review, DAL.App.DTO.Review>,
        IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext,
            new ReviewMapper())
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Review>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.User)
                .Include(o => o.Hotel)
                .Include(o => o.RoomType)
                .Include(o => o.ReviewCategory)
                .OrderBy(o => o.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Review> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(o => o.User)
                .Include(o => o.Hotel)
                .Include(o => o.RoomType)
                .Include(o => o.ReviewCategory)
                .FirstOrDefaultAsync(o => o.Id == id);
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}