using ee.itcollege.ekmand.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>, IProductRepositoryCustom
    {
        
    }
}