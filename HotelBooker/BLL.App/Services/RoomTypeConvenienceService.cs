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
    public class RoomTypeConvenienceService :
        BaseEntityService<IAppUnitOfWork, IRoomTypeConvenienceRepository, IRoomTypeConvenienceServiceMapper, DAL.App.DTO.RoomTypeConvenience,
            BLL.App.DTO.RoomTypeConvenience>, IRoomTypeConvenienceService
    {
        public RoomTypeConvenienceService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.RoomTypeConveniences, new RoomTypeConvenienceServiceMapper())
        {
        }
        
        public async Task<IEnumerable<GroupedConvenience>> GetRoomTypeConveniences(Guid roomTypeId)
        {
            var roomTypeConveniences = (await GetAllAsync())
                .Where(c => c.RoomTypeId == roomTypeId);
            var convenienceGroupsViewModels = new List<GroupedConvenience>();
            var convenienceGroupListObjects = new Dictionary<string, GroupedConvenience>();
            foreach (var roomTypeConvenience in roomTypeConveniences)
            {
                var key = roomTypeConvenience.Convenience!.ConvenienceGroup!.Name;
                if (convenienceGroupListObjects.ContainsKey(key))
                {
                    var list = convenienceGroupListObjects[key].Conveniences;
                    convenienceGroupListObjects[key].Conveniences = list.Append(roomTypeConvenience.Convenience);
                }
                else
                {
                    var groupsViewModel = new GroupedConvenience
                    {
                        ConvenienceGroup = roomTypeConvenience.Convenience.ConvenienceGroup,
                        Conveniences = new List<Convenience>
                        {
                            roomTypeConvenience.Convenience
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