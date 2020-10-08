using AutoMapper;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ConvenienceMapper : DALMapper<Convenience, DALAppDTO.Convenience>
    {
        public ConvenienceMapper()
        {
            MapperConfigurationExpression.CreateMap<ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, ConvenienceGroup>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}