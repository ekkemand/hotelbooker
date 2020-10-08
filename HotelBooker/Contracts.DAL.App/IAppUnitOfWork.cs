using Contracts.DAL.App.Repositories;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        ICampaignRepository Campaigns { get; }
        IConvenienceGroupRepository ConvenienceGroups { get; }
        IConvenienceRepository Conveniences { get; }
        ICurrencyRepository Currencies { get; }
        IHotelConvenienceRepository HotelConveniences { get; }
        IHotelRepository Hotels { get; }
        IImageOfRoomRepository ImageOfRooms { get; }
        IOwnerCompanyRepository OwnerCompanies { get; }
        IPersonRepository Persons { get; }
        IPriceRepository Prices { get; }
        IProductGroupRepository ProductGroups { get; }
        IProductRepository Products { get; }
        IReservationRowRepository ReservationRows { get; }
        IReservationRepository Reservations { get; }
        IReviewRepository Reviews { get; }
        IReviewCategoryRepository ReviewCategories { get; }
        IRoomRepository Rooms { get; }
        IRoomTypeConvenienceRepository RoomTypeConveniences { get; }
        IRoomTypeRepository RoomTypes { get; }
        IUserRepository Users { get; }
    }
}