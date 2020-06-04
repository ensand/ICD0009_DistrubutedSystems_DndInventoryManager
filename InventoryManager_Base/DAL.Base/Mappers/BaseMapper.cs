using AutoMapper;
using AutoMapper.Configuration;
using com.enola.inventorymanager.Contracts.DAL.Base.Mappers;

namespace com.enola.inventorymanager.DAL.Base.Mappers
{
    /// <summary>
    /// Maps using AutoMapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public class BaseMapper<TLeftObject, TRightObject> : IBaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected IMapper Mapper;
        protected readonly MapperConfigurationExpression MapperConfigurationExpression;

        public BaseMapper()
        {
            MapperConfigurationExpression = new MapperConfigurationExpression();
            
            MapperConfigurationExpression.CreateMap<TLeftObject, TRightObject>();
            MapperConfigurationExpression.CreateMap<TRightObject, TLeftObject>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public virtual TRightObject Map(TLeftObject inObject)
        {
            return Mapper.Map<TLeftObject, TRightObject>(inObject);
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return Mapper.Map<TRightObject, TLeftObject>(inObject);
        }
    }

}