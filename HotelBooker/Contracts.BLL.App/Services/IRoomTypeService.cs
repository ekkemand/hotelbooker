using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App.HelperClasses;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IRoomTypeService : IBaseEntityService<RoomType>, IRoomTypeRepositoryCustom<RoomType>
    {
        public Task<RoomTypeAdditionalData> GetEarliestReservationStartDate(Guid roomTypeId);
    }
}