using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ReviewServiceMapper : BLLMapper<DALAppDTO.Review, BLLAppDTO.Review>, IReviewServiceMapper
    {
        public ReviewServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, BLLAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RoomType, DALAppDTO.RoomType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, BLLAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ReviewCategory, DALAppDTO.ReviewCategory>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}