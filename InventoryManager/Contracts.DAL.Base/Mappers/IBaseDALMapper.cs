namespace Contracts.DAL.Base.Mappers
{
    public interface IBaseDALMapper<in TInObject, out TOutObject>
        where TInObject : class
        where TOutObject : class, new()
    {
        TOutObject Map(TInObject inObject);

        TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
            where TMapInObject : class
            where TMapOutObject : class, new();
    }
}