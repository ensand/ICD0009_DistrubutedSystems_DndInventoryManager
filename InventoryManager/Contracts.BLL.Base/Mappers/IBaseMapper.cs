namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : com.enola.inventorymanager.Contracts.DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        
    }
}