using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class CurrencyService : BaseEntityService<IAppUnitOfWork, ICurrencyRepository,
        ICurrencyServiceMapper, DAL.App.DTO.Currency, BLL.App.DTO.Currency>, 
        ICurrencyService
    {
        public CurrencyService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Currencies, new CurrencyServiceMapper())
        {
        }
    }
}