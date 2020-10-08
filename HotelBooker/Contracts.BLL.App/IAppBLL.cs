using Contracts.BLL.App.Services;
using ee.itcollege.ekmand.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public ICampaignService Campaigns { get; }
        public IConvenienceGroupService ConvenienceGroups { get; }
        public IConvenienceService Conveniences { get; }
        public ICurrencyService Currencies { get; }
        public IHotelConvenienceService HotelConveniences { get; }
        public IHotelService Hotels { get; }
        public IImageOfRoomService ImageOfRooms { get; }
        public IOwnerCompanyService OwnerCompanies { get; }
        public IPersonService Persons { get; }
        public IPriceService Prices { get; }
        public IProductGroupService ProductGroups { get; }
        public IProductService Products { get; }
        public IReservationRowService ReservationRows { get; }
        public IReservationService Reservations { get; }
        public IReviewService Reviews { get; }
        public IReviewCategoryService ReviewCategories { get; }
        public IRoomService Rooms { get; }
        public IRoomTypeConvenienceService RoomTypeConveniences { get; }
        public IRoomTypeService RoomTypes { get; }
        public IUserService Users { get; }
    }
}