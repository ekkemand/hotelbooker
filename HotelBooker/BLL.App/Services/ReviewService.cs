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
using Microsoft.EntityFrameworkCore.Internal;

namespace BLL.App.Services
{
    public class ReviewService : BaseEntityService<IAppUnitOfWork, IReviewRepository, IReviewServiceMapper,
        DAL.App.DTO.Review, BLL.App.DTO.Review>, IReviewService
    {
        public ReviewService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Reviews, new ReviewServiceMapper())
        {
        }

        public async Task<IEnumerable<Review>> GetHotelReviews(Guid hotelId)
        {
            return (await GetAllAsync()).Where(o => o.HotelId == hotelId && o.RoomTypeId == null);
        }
        
        public async Task<IEnumerable<Review>> GetRoomTypeReviews(Guid roomTypeId)
        {
            return (await GetAllAsync()).Where(o => o.RoomTypeId == roomTypeId);
        }

        public async Task<IEnumerable<ReviewCategory>> GetAllCategoriesForHotelsAsync()
        {
            var reviews = (await GetAllAsync())
                .Where(o => o.RoomTypeId == null && o.ReviewCategoryId != null);
            var categories = reviews.Select(o => o.ReviewCategory!)
                .Where(o => o != null && o!.Accepted);

            categories = categories.Where(o => o != null);
            
            return categories.GroupBy(l => l.Name).Select(group => group.First());
        }
        
    }
}