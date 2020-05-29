using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.Base.Mappers
{
    /// <summary>
    /// Maps using AutoMapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TInObject"></typeparam>
    /// <typeparam name="TOutObject"></typeparam>
    public class BaseMapper<TInObject, TOutObject> : IBaseMapper<TInObject, TOutObject> 
        where TInObject : class?, new() 
        where TOutObject : class?, new()
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly IMapper Mapper;
        
        public BaseMapper()
        {
            Mapper = new MapperConfiguration(config =>
                {
                    config.CreateMap<TInObject, TOutObject>();
                    config.CreateMap<TOutObject, TInObject>();
                })
                .CreateMapper();
        }

        public virtual TOutObject Map(TInObject inObject)
        {
            return Mapper.Map<TInObject, TOutObject>(inObject);
        }

        public TInObject Map(TOutObject outObject)
        {
            return Mapper.Map<TOutObject, TInObject>(outObject);
        }
    }
}