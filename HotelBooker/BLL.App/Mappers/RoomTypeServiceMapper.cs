using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class RoomTypeServiceMapper : BLLMapper<DALAppDTO.RoomType, BLLAppDTO.RoomType>, IRoomTypeServiceMapper
    {
        public RoomTypeServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.ImageOfRoom, BLLAppDTO.ImageOfRoom>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ImageOfRoom, DALAppDTO.ImageOfRoom>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Review, BLLAppDTO.Review>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Review, DALAppDTO.Review>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.ReviewCategory, BLLAppDTO.ReviewCategory>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ReviewCategory, DALAppDTO.ReviewCategory>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Product, DALAppDTO.Product>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Room, BLLAppDTO.Room>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Room, DALAppDTO.Room>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Reservation, BLLAppDTO.Reservation>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Reservation, DALAppDTO.Reservation>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}