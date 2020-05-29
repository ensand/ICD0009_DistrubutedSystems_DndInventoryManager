namespace Contracts.DAL.Base.Mappers
{
    public interface IBaseMapper<TInObject, TOutObject>
        where TInObject : class?, new()
        where TOutObject : class?, new()
    {
        TOutObject Map(TInObject inObject);
        TInObject Map(TOutObject outObject);
    }
}