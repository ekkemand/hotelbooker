using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Person = BLL.App.DTO.Person;

namespace Contracts.BLL.App.Services
{
    public interface IPersonService : IBaseEntityService<Person>, IPersonRepositoryCustom<Person>
    {
    }
}