using System;
using System.Collections;
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
    public class ConvenienceService : BaseEntityService<IAppUnitOfWork, IConvenienceRepository,
        IConvenienceServiceMapper, DAL.App.DTO.Convenience, BLL.App.DTO.Convenience>, 
        IConvenienceService
    {
        public ConvenienceService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Conveniences, new ConvenienceServiceMapper())
        {
        }

        public async Task<IEnumerable<Convenience>> GetSuitableConveniences(IEnumerable<Guid> ids)
        {
            return (await GetAllAsync()).Where(o => ids.All(e => e != o.Id));
        }
    }
}