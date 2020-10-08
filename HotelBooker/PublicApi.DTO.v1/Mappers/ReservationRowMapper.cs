using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class ReservationRowMapper : BaseMapper<BLLAppDTO.ReservationRow, ReservationRow>
    {
        public ReservationRowMapper()
        {
            MapperConfigurationExpression.CreateMap<Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Product, Product>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}