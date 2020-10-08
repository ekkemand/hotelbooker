using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class HotelServiceMapper : BLLMapper<DALAppDTO.Hotel, BLLAppDTO.Hotel>, IHotelServiceMapper
    {
        public HotelServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.OwnerCompany, BLLAppDTO.OwnerCompany>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OwnerCompany, DALAppDTO.OwnerCompany>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ImageOfRoom, BLLAppDTO.ImageOfRoom>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ImageOfRoom, DALAppDTO.ImageOfRoom>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, BLLAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Review, DALAppDTO.Review>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, BLLAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ReviewCategory, DALAppDTO.ReviewCategory>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}