using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using ee.itcollege.ekmand.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IHotelRepository : IBaseRepository<Hotel>, IHotelRepositoryCustom
    {
    }
}