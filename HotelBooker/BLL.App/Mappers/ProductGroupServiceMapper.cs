using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ProductGroupServiceMapper : BLLMapper<DALAppDTO.ProductGroup, BLLAppDTO.ProductGroup>, 
        IProductGroupServiceMapper
    {
        public ProductGroupServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductGroup, BLLAppDTO.ProductGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ProductGroup, DALAppDTO.ProductGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}