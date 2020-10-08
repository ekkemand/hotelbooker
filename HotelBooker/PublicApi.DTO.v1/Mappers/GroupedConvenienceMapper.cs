using AutoMapper;
using BLLAppDTO=BLL.App.DTO;
namespace PublicApi.DTO.v1.Mappers
{
    public class GroupedConvenienceMapper : BaseMapper<BLLAppDTO.HelperClasses.GroupedConvenience, GroupedConvenienceDTO>
    {
        public GroupedConvenienceMapper()
        {
            MapperConfigurationExpression.CreateMap<ConvenienceGroup, BLLAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ConvenienceGroup, ConvenienceGroup>();
            
            MapperConfigurationExpression.CreateMap<Convenience, BLLAppDTO.Convenience>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Convenience, Convenience>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}