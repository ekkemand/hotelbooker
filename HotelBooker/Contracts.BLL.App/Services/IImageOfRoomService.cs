using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IImageOfRoomService : IBaseEntityService<ImageOfRoom>, IImageOfRoomRepositoryCustom<ImageOfRoom>
    {
        public Task<IEnumerable<ImageOfRoom>> GetAllImagesOfRoomTypeAsync(Guid roomTypeId);
    }
}