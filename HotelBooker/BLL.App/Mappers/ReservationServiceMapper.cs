using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ReservationServiceMapper : BLLMapper<DALAppDTO.Reservation, BLLAppDTO.Reservation>, IReservationServiceMapper
    {
        public ReservationServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Person, DALAppDTO.Person>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, BLLAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RoomType, DALAppDTO.RoomType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}