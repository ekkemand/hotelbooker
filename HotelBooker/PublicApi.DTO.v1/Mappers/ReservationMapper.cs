using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class ReservationMapper : BaseMapper<BLLAppDTO.Reservation, Reservation>
    {
        public ReservationMapper()
        {
            MapperConfigurationExpression.CreateMap<Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, Hotel>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}