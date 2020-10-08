using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IReviewCategoryService : IBaseEntityService<ReviewCategory>,
        IReviewCategoryRepositoryCustom<ReviewCategory>
    {
        public Task<ReviewCategory> AcceptCategory(Guid id);
    }
}