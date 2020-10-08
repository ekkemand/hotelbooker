using Contracts.BLL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : ee.itcollege.ekmand.DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
    }
}