using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Product, DAL.App.DTO.Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext,
            new ProductMapper())
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Product>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.RoomType)
                .Include(o => o.ProductGroup);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Product> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(o => o.RoomType)
                .Include(o => o.ProductGroup)
                .FirstOrDefaultAsync(o => o.Id == id);
            var result = Mapper.Map(domainEntity);
            return result;
        }

        // public override Task<Product> RemoveAsync(Product entity, object? userId = null)
        // {
        //     var list = RepoDbContext.ReservationRows.Where(o => o.ProductId == entity.Id);
        //     RepoDbContext.ReservationRows.RemoveRange(list);
        //     RepoDbContext.SaveChanges();
        //     
        //     return base.RemoveAsync(entity, userId);
        // }
        
    }
}