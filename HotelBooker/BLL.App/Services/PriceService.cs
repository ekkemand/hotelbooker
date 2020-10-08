using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PriceService :
        BaseEntityService<IAppUnitOfWork, IPriceRepository, IPriceServiceMapper, DAL.App.DTO.Price,
            BLL.App.DTO.Price>, IPriceService
    {
        public PriceService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Prices, new PriceServiceMapper())
        {
        }

        public async Task<ICollection<Price>> GetPricesForProductAsync(Guid productId)
        {
            return (await GetAllAsync()).Where(o => o.ProductId == productId)
                .OrderBy(o => o.Value).ToList();
        }
        
        public async Task<IEnumerable<Price>> GetPricesForProducts(IEnumerable<Product> products)
        {
            var prices = new List<Price>();
            foreach (var product in products)
            {
                var pricesForProduct = await GetPricesForProductAsync(product.Id);
                foreach (var price in pricesForProduct)
                {
                    prices.Add(price);
                }
            }

            return prices;
        }
    }
}