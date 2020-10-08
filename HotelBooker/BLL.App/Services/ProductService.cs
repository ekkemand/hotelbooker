using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ProductService :
        BaseEntityService<IAppUnitOfWork, IProductRepository, IProductServiceMapper, DAL.App.DTO.Product,
            BLL.App.DTO.Product>, IProductService
    {
        public ProductService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Products, new ProductServiceMapper())
        {
        }

        public async Task<IEnumerable<Product>> GetOtherProducts(IEnumerable<Product> bookedProducts)
        {
            return (await GetAllAsync()).Where(o => bookedProducts.All(e => e.Id != o.Id))
                .Where(o => o.RoomTypeId == null);
        }

        // public override async Task<Product> RemoveAsync(Product entity, object? userId = null)
        // {
        //     await DeleteChildEntities(entity.Id);
        //     return await base.RemoveAsync(entity, userId);
        // }
        //
        // public override async Task<Product> RemoveAsync(Guid id, object? userId = null)
        // {
        //     await DeleteChildEntities(id);
        //     return await base.RemoveAsync(id, userId);
        // }
        //
        // private async Task<IEnumerable<ReservationRow>> DeleteChildEntities(Guid productId)
        // {
        //     var children = (await UnitOfWork.ReservationRows.GetAllAsync())
        //         .Where(o => o.ProductId == productId);
        //     foreach (var child in children)
        //     {
        //         await UnitOfWork.ReservationRows.RemoveAsync(child);
        //     }
        //     return new List<ReservationRow>();
        // }
    }
}