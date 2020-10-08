using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.ekmand.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(UnitOfWork));

        public IPriceService Prices =>
            GetService<IPriceService>(() => new PriceService(UnitOfWork));

        public IProductGroupService ProductGroups =>
            GetService<IProductGroupService>(() => new ProductGroupService(UnitOfWork));

        public IProductService Products =>
            GetService<IProductService>(() => new ProductService(UnitOfWork));

        public IReservationRowService ReservationRows =>
            GetService<IReservationRowService>(() => new ReservationRowService(UnitOfWork));

        public IUserService Users =>
            GetService<IUserService>(() => new UserService(UnitOfWork));

        public IReservationService Reservations =>
            GetService<IReservationService>(() => new ReservationService(UnitOfWork));

        public IReviewService Reviews =>
            GetService<IReviewService>(() => new ReviewService(UnitOfWork));
        
        public IReviewCategoryService ReviewCategories =>
            GetService<IReviewCategoryService>(() => new ReviewCategoryService(UnitOfWork));

        public IRoomService Rooms =>
            GetService<IRoomService>(() => new RoomService(UnitOfWork));

        public IRoomTypeConvenienceService RoomTypeConveniences =>
            GetService<IRoomTypeConvenienceService>(() => new RoomTypeConvenienceService(UnitOfWork));

        public IRoomTypeService RoomTypes =>
            GetService<IRoomTypeService>(() => new RoomTypeService(UnitOfWork));

        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UnitOfWork));

        public IConvenienceGroupService ConvenienceGroups =>
            GetService<IConvenienceGroupService>(() => new ConvenienceGroupService(UnitOfWork));

        public IConvenienceService Conveniences =>
            GetService<IConvenienceService>(() => new ConvenienceService(UnitOfWork));

        public ICurrencyService Currencies  =>
            GetService<ICurrencyService>(() => new CurrencyService(UnitOfWork));

        public IHotelConvenienceService HotelConveniences =>
            GetService<IHotelConvenienceService>(() => new HotelConvenienceService(UnitOfWork));

        public IHotelService Hotels =>
            GetService<IHotelService>(() => new HotelService(UnitOfWork));

        public IImageOfRoomService ImageOfRooms =>
            GetService<IImageOfRoomService>(() => new ImageOfRoomService(UnitOfWork));

        public IOwnerCompanyService OwnerCompanies =>
            GetService<IOwnerCompanyService>(() => new OwnerCompanyService(UnitOfWork));
    }
}