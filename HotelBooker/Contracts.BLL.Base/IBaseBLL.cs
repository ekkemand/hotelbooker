using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        
        public TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class;
    }
}