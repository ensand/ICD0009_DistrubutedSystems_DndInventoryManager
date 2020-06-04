namespace com.enola.inventorymanager.BLL.Base.Mappers
{
    /// <summary>
    /// Maps using AutoMapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public class BaseMapper<TLeftObject, TRightObject> : com.enola.inventorymanager.DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new() 
        where TRightObject : class?, new()
    {
        
    }
}