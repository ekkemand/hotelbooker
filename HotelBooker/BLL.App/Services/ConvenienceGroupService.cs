using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ConvenienceGroupService : BaseEntityService<IAppUnitOfWork, IConvenienceGroupRepository,
        IConvenienceGroupServiceMapper,
        DAL.App.DTO.ConvenienceGroup, BLL.App.DTO.ConvenienceGroup>, IConvenienceGroupService
    {
        public ConvenienceGroupService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ConvenienceGroups, new ConvenienceGroupServiceMapper())
        {
        }
    }
}