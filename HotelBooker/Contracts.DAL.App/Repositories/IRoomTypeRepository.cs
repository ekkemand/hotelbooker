﻿using ee.itcollege.ekmand.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRoomTypeRepository : IBaseRepository<RoomType>, IRoomTypeRepositoryCustom
    {
        
    }
}