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
    public class HotelService : BaseEntityService<IAppUnitOfWork, IHotelRepository, IHotelServiceMapper,
        DAL.App.DTO.Hotel, BLL.App.DTO.Hotel>, IHotelService
    {
        public HotelService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Hotels, new HotelServiceMapper())
        {
        }

        public async Task<IEnumerable<Hotel>> GetByHotelsConvenience(Guid convenienceId, IEnumerable<Hotel> hotels)
        {
            var conveniences = (await UnitOfWork.HotelConveniences.GetAllAsync())
                .Where(o => o.ConvenienceId == convenienceId);
            var newList = new List<Hotel>();
            foreach (var hotelConvenience in conveniences)
            {
                newList.Add(hotels.FirstOrDefault(o => o.Id == hotelConvenience.HotelId));
            }
            return newList;
        }

        public async Task<IEnumerable<Hotel>> GetByReviewCategory(Guid reviewCategoryId, IEnumerable<Hotel> hotels)
        {
            var categories = (await UnitOfWork.Reviews.GetAllAsync())
                .Where(o => o.ReviewCategoryId == reviewCategoryId);   
            var newList = new List<Hotel>();
            foreach (var category in categories)
            {
                newList.Add(hotels.FirstOrDefault(o => o.Id == category.HotelId));
            }

            return newList;
        }
    }
}