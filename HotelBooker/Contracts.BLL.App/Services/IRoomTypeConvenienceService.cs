﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.HelperClasses;
using Contracts.BLL.App.HelperClasses;
using ee.itcollege.ekmand.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IRoomTypeConvenienceService : IBaseEntityService<RoomTypeConvenience>,
        IRoomTypeConvenienceRepositoryCustom<RoomTypeConvenience>
    {
        public Task<IEnumerable<GroupedConvenience>> GetRoomTypeConveniences(Guid roomTypeId);
    }
}