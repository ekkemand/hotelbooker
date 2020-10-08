using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IConvenienceGroupService : IBaseEntityService<ConvenienceGroup>,
        IConvenienceGroupRepositoryCustom<ConvenienceGroup>
    {
    }
}