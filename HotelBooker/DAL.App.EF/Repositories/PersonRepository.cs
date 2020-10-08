using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.ekmand.DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository :
        EFBaseRepository<AppDbContext, AppUser, Person, DAL.App.DTO.Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Person, DAL.App.DTO.Person>())
        {
        }
    }
}