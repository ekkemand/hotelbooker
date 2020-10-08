using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class HotelConvenienceServiceMapper : BLLMapper<DALAppDTO.HotelConvenience, BLLAppDTO.HotelConvenience>,
        IHotelConvenienceServiceMapper
    {
        public HotelConvenienceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Convenience, BLLAppDTO.Convenience>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Convenience, DALAppDTO.Convenience>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, BLLAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}