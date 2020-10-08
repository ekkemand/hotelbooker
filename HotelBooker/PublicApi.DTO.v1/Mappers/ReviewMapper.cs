using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class ReviewMapper : BaseMapper<BLLAppDTO.Review, Review>
    {
        public ReviewMapper()
        {
            MapperConfigurationExpression.CreateMap<ReviewCategory, BLLAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ReviewCategory, ReviewCategory>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}