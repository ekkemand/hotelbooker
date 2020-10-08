﻿using ee.itcollege.ekmand.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IRoomServiceMapper : IBaseMapper<DALAppDTO.Room, BLLAppDTO.Room>
    {
    }
}