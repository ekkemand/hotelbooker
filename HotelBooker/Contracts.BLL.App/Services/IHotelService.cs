using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IHotelService : IBaseEntityService<Hotel>, IHotelRepositoryCustom<Hotel>
    {
        public Task<IEnumerable<Hotel>> GetByHotelsConvenience(Guid convenienceId, IEnumerable<Hotel> hotels);
        public Task<IEnumerable<Hotel>> GetByReviewCategory(Guid reviewCategoryId, IEnumerable<Hotel> hotels);
    }
}