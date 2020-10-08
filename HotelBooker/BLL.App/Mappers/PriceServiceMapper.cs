using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class PriceServiceMapper : BLLMapper<DALAppDTO.Price, BLLAppDTO.Price>, IPriceServiceMapper
    {
        public PriceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Campaign, BLLAppDTO.Campaign>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Campaign, DALAppDTO.Campaign>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Hotel, BLLAppDTO.Hotel>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Hotel, DALAppDTO.Hotel>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Person, DALAppDTO.Person>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Product, DALAppDTO.Product>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Currency, BLLAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Currency, DALAppDTO.Currency>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}