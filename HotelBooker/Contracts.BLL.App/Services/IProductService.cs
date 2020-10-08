using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProductService : IBaseEntityService<Product>, IProductRepositoryCustom<Product>
    {
        public Task<IEnumerable<Product>> GetOtherProducts(IEnumerable<Product> bookedProducts);
    }
}