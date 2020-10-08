using System;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IReservationService : IBaseEntityService<Reservation>, IReservationRepositoryCustom<Reservation>
    {
    }
}