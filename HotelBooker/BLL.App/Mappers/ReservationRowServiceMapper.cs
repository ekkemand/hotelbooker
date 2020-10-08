using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ReservationRowServiceMapper : BLLMapper<DALAppDTO.ReservationRow, BLLAppDTO.ReservationRow>,
        IReservationRowServiceMapper
    {
        public ReservationRowServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Reservation, BLLAppDTO.Reservation>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Reservation, DALAppDTO.Reservation>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Product, DALAppDTO.Product>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}