using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IReservationRowService : IBaseEntityService<ReservationRow>,
        IReservationRowRepositoryCustom<ReservationRow>
    {
        public Task<ReservationRow> AddRowWithReservation(Reservation reservation);
        public Task<IEnumerable<ReservationRow>> GetReservationRowsByReservationId(Guid id);
    }
}