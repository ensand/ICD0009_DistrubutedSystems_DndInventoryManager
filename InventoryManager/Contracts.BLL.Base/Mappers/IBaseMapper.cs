namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseBLLMapper<TInObject, TOutObject>
        where TInObject : class, new()
        where TOutObject : class, new()
    {
        TOutObject Map(TInObject inObject);

        TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
            where TMapInObject : class, new()
            where TMapOutObject : class, new();
    }
}