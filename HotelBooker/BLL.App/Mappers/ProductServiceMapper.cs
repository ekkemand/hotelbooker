using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ProductServiceMapper : BLLMapper<DALAppDTO.Product, BLLAppDTO.Product>, IProductServiceMapper
    {
        public ProductServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.RoomType, BLLAppDTO.RoomType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RoomType, DALAppDTO.RoomType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductGroup, BLLAppDTO.ProductGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ProductGroup, DALAppDTO.ProductGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}