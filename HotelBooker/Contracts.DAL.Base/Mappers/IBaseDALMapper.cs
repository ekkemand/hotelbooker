namespace Contracts.DAL.Base.Mappers
{
    public interface IBaseDALMapper<in TInObject, out TOutObject>
        where TOutObject : class, new()
        where TInObject : class
    {
        TOutObject Map(TInObject inObject);

        TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
            where TMapOutObject : class, new()
            where TMapInObject : class;
    }
}