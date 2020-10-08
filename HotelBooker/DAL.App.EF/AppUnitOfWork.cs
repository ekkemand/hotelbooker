using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.ekmand.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IPersonRepository Persons =>
            GetRepository<IPersonRepository>(() =>
                new PersonRepository(UOWDbContext));

        public ICampaignRepository Campaigns =>
            GetRepository<ICampaignRepository>(() =>
                new CampaignRepository(UOWDbContext));

        public IConvenienceGroupRepository ConvenienceGroups =>
            GetRepository<IConvenienceGroupRepository>(() =>
                new ConvenienceGroupRepository(UOWDbContext));

        public IConvenienceRepository Conveniences =>
            GetRepository<IConvenienceRepository>(() =>
                new ConvenienceRepository(UOWDbContext));

        public ICurrencyRepository Currencies =>
            GetRepository<ICurrencyRepository>(() =>
                new CurrencyRepository(UOWDbContext));

        public IHotelConvenienceRepository HotelConveniences =>
            GetRepository<IHotelConvenienceRepository>(() =>
                new HotelConvenienceRepository(UOWDbContext));

        public IHotelRepository Hotels =>
            GetRepository<IHotelRepository>(() =>
                new HotelRepository(UOWDbContext));

        public IImageOfRoomRepository ImageOfRooms =>
            GetRepository<IImageOfRoomRepository>(() =>
                new ImageOfRoomRepository(UOWDbContext));

        public IOwnerCompanyRepository OwnerCompanies =>
            GetRepository<IOwnerCompanyRepository>(() =>
                new OwnerCompanyRepository(UOWDbContext));

        public IPriceRepository Prices =>
            GetRepository<IPriceRepository>(() =>
                new PriceRepository(UOWDbContext));

        public IProductGroupRepository ProductGroups =>
            GetRepository<IProductGroupRepository>(() =>
                new ProductGroupRepository(UOWDbContext));

        public IProductRepository Products =>
            GetRepository<IProductRepository>(() =>
                new ProductRepository(UOWDbContext));

        public IReservationRowRepository ReservationRows =>
            GetRepository<IReservationRowRepository>(() =>
                new ReservationRowRepository(UOWDbContext));

        public IReservationRepository Reservations =>
            GetRepository<IReservationRepository>(() =>
                new ReservationRepository(UOWDbContext));

        public IReviewRepository Reviews =>
            GetRepository<IReviewRepository>(() =>
                new ReviewRepository(UOWDbContext));
        
        public IReviewCategoryRepository ReviewCategories =>
            GetRepository<IReviewCategoryRepository>(() =>
                new ReviewCategoryRepository(UOWDbContext));

        public IRoomRepository Rooms =>
            GetRepository<IRoomRepository>(() =>
                new RoomRepository(UOWDbContext));

        public IRoomTypeRepository RoomTypes =>
            GetRepository<IRoomTypeRepository>(() =>
                new RoomTypeRepository(UOWDbContext));

        public IRoomTypeConvenienceRepository RoomTypeConveniences =>
            GetRepository<IRoomTypeConvenienceRepository>(() =>
                new RoomTypeConvenienceRepository(UOWDbContext));

        public IUserRepository Users =>
            GetRepository<IUserRepository>(() =>
                new UserRepository(UOWDbContext));
    }
}