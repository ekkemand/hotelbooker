using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;

namespace BLL.App.Services
{
    public class ReservationService :
        BaseEntityService<IAppUnitOfWork, IReservationRepository, IReservationServiceMapper, DAL.App.DTO.Reservation,
            BLL.App.DTO.Reservation>, IReservationService
    {
        public ReservationService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Reservations, new ReservationServiceMapper())
        {
        }

        
    }
}