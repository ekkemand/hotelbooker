using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RoomService :
        BaseEntityService<IAppUnitOfWork, IRoomRepository, IRoomServiceMapper, DAL.App.DTO.Room,
            BLL.App.DTO.Room>, IRoomService
    {
        public RoomService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Rooms, new RoomServiceMapper())
        {
        }
    }
}