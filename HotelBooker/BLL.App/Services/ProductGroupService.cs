using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ProductGroupService :
        BaseEntityService<IAppUnitOfWork, IProductGroupRepository, IProductGroupServiceMapper, DAL.App.DTO.ProductGroup,
            BLL.App.DTO.ProductGroup>, IProductGroupService
    {
        public ProductGroupService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProductGroups, new ProductGroupServiceMapper())
        {
        }
    }
}