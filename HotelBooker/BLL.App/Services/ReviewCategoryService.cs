using System;
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
    public class ReviewCategoryService : BaseEntityService<IAppUnitOfWork, IReviewCategoryRepository,
        IReviewCategoryServiceMapper, DAL.App.DTO.ReviewCategory, BLL.App.DTO.ReviewCategory>, IReviewCategoryService
    {
        public ReviewCategoryService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ReviewCategories, new ReviewCategoryServiceMapper())
        {
        }

        public async Task<ReviewCategory> AcceptCategory(Guid id)
        {
            var category = await FirstOrDefaultAsync(id);
            category.Accepted = true;
            return await UpdateAsync(category);
        }
    }
}