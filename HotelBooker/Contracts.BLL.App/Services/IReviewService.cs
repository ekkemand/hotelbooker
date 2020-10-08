using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IReviewService : IBaseEntityService<Review>, IReviewRepositoryCustom<Review>
    {
        public Task<IEnumerable<Review>> GetHotelReviews(Guid hotelId);
        public Task<IEnumerable<Review>> GetRoomTypeReviews(Guid roomTypeId);
        public Task<IEnumerable<ReviewCategory>> GetAllCategoriesForHotelsAsync();
    }
}