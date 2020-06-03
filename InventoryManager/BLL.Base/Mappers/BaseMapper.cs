namespace BLL.Base.Mappers
{
    /// <summary>
    /// Maps using AutoMapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public class BaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new() 
        where TRightObject : class?, new()
    {
        
    }
}