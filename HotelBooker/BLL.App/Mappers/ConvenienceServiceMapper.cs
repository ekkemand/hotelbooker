using AutoMapper;
using Contracts.BLL.App.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ConvenienceServiceMapper : BLLMapper<DALAppDTO.Convenience, BLLAppDTO.Convenience>,
        IConvenienceServiceMapper
    {
        public ConvenienceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.ConvenienceGroup, BLLAppDTO.ConvenienceGroup>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ConvenienceGroup, DALAppDTO.ConvenienceGroup>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}