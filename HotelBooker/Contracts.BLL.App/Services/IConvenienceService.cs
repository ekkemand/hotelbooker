using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IConvenienceService : IBaseEntityService<Convenience>, IConvenienceRepositoryCustom<Convenience>
    {
        public Task<IEnumerable<Convenience>> GetSuitableConveniences(IEnumerable<Guid> ids);
    }
}