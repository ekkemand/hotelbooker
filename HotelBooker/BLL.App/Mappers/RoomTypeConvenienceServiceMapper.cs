using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RoomTypeConvenienceServiceMapper :
        BLLMapper<DALAppDTO.RoomTypeConvenience, BLLAppDTO.RoomTypeConvenience>, IRoomTypeConvenienceServiceMapper
    {
        public RoomTypeConvenienceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, BLLAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RoomType, DALAppDTO.RoomType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, BLLAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Convenience, BLLAppDTO.Convenience>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Convenience, DALAppDTO.Convenience>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}