using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ImageOfRoomServiceMapper : BLLMapper<DALAppDTO.ImageOfRoom, BLLAppDTO.ImageOfRoom>,
        IImageOfRoomServiceMapper
    {
        public ImageOfRoomServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, BLLAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RoomType, DALAppDTO.RoomType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}