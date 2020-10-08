using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PersonService :
        BaseEntityService<IAppUnitOfWork, IPersonRepository, IPersonServiceMapper, DAL.App.DTO.Person,
            BLL.App.DTO.Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Persons, new PersonServiceMapper())
        {
        }
    }
}