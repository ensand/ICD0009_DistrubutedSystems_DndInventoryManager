using Contracts.DAL.Base.Mappers;

namespace DAL.Base.Mappers
{
    public class IdentityMapper<TInObject, TOutObject> : IBaseMapper<TInObject, TOutObject> 
        where TInObject : class?, new() 
        where TOutObject : class?, new()
    {
        public TOutObject Map(TInObject inObject)
        {
            return inObject as TOutObject ?? default!;
        }

        public TInObject Map(TOutObject outObject)
        {
            return outObject as TInObject ?? default!;
        }
    }
}