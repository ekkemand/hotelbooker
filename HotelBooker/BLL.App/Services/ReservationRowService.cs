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
    public class ReservationRowService :
        BaseEntityService<IAppUnitOfWork, IReservationRowRepository, IReservationRowServiceMapper, DAL.App.DTO.ReservationRow,
            BLL.App.DTO.ReservationRow>, IReservationRowService
    {
        public ReservationRowService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ReservationRows, new ReservationRowServiceMapper())
        {
        }
        
        public async Task<ReservationRow> AddRowWithReservation(Reservation reservation)
        {
            var roomType = await UnitOfWork.RoomTypes.FirstOrDefaultAsync(reservation.RoomTypeId);

            var reservationRow = new ReservationRow
            {
                ProductId = roomType.Product!.Id,
                Reservation = reservation
            };
            return Add(reservationRow);
        }

        public async Task<IEnumerable<ReservationRow>> GetReservationRowsByReservationId(Guid id)
        {
            return (await GetAllAsync()).Where(o => o.ReservationId == id);
        }
    }
}