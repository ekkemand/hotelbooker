using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class PriceMapper : BaseMapper<BLLAppDTO.Price, Price>
    {
        public PriceMapper()
        {
            MapperConfigurationExpression.CreateMap<Currency, BLLAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Currency, Currency>();
            
            MapperConfigurationExpression.CreateMap<Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Product, Product>();
            
            MapperConfigurationExpression.CreateMap<Campaign, BLLAppDTO.Campaign>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Campaign, Campaign>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}