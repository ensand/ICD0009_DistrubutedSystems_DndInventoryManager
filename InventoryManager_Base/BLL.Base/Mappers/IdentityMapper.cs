namespace com.enola.inventorymanager.BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : com.enola.inventorymanager.DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new() 
        where TRightObject : class, new()
    {
       
    }
}