namespace PublicApi.DTO.V1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : com.enola.inventorymanager.DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}