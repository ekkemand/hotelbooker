using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.HelperClasses;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.HelperClasses;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class HotelConvenienceService : BaseEntityService<IAppUnitOfWork, IHotelConvenienceRepository,
            IHotelConvenienceServiceMapper, DAL.App.DTO.HotelConvenience, BLL.App.DTO.HotelConvenience>,
        IHotelConvenienceService
    {
        public HotelConvenienceService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.HotelConveniences, new HotelConvenienceServiceMapper())
        {
        }
        
        public async Task<IEnumerable<GroupedConvenience>> GetHotelConveniences(Guid hotelId)
        {
            var hotelConveniences = (await GetAllAsync())
                .Where(c => c.HotelId == hotelId);
            var convenienceGroupsViewModels = new List<GroupedConvenience>();
            var convenienceGroupListObjects = new Dictionary<string, GroupedConvenience>();
            foreach (var hotelConvenience in hotelConveniences)
            {
                var key = hotelConvenience.Convenience!.ConvenienceGroup!.Name;
                if (convenienceGroupListObjects.ContainsKey(key))
                {
                    var list = convenienceGroupListObjects[key].Conveniences;
                    convenienceGroupListObjects[key].Conveniences = list.Append(hotelConvenience.Convenience);
                }
                else
                {
                    var groupsViewModel = new GroupedConvenience
                    {
                        ConvenienceGroup = hotelConvenience.Convenience.ConvenienceGroup,
                        Conveniences = new List<Convenience>
                        {
                            hotelConvenience.Convenience
                        }
                    };
                    convenienceGroupListObjects.Add(key, groupsViewModel);
                }
            }

            foreach (var listObject in convenienceGroupListObjects)
            {
                convenienceGroupsViewModels.Add(listObject.Value);
            }

            return convenienceGroupsViewModels;
        }
    }
}