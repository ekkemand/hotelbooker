using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ImageOfRoomService : BaseEntityService<IAppUnitOfWork, IImageOfRoomRepository,
            IImageOfRoomServiceMapper, DAL.App.DTO.ImageOfRoom, BLL.App.DTO.ImageOfRoom>, 
        IImageOfRoomService
    {
        public ImageOfRoomService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ImageOfRooms, new ImageOfRoomServiceMapper())
        {
        }

        public async Task<IEnumerable<ImageOfRoom>> GetAllImagesOfRoomTypeAsync(Guid roomTypeId)
        {
            return (await GetAllAsync()).Where(o => o.RoomTypeId == roomTypeId);
        }
    }
}