using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPriceService : IBaseEntityService<Price>, IPriceRepositoryCustom<Price>
    {
        public Task<ICollection<Price>> GetPricesForProductAsync(Guid productId);
        public Task<IEnumerable<Price>> GetPricesForProducts(IEnumerable<Product> products);
    }
}