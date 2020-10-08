using Contracts.BLL.App.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ReviewCategoryServiceMapper : BLLMapper<DALAppDTO.ReviewCategory, BLLAppDTO.ReviewCategory>,
        IReviewCategoryServiceMapper
    {
        public ReviewCategoryServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, BLLAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Review, DALAppDTO.Review>();
        }
    }
}