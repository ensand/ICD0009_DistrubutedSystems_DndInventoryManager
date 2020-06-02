namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new() 
        where TRightObject : class, new()
    {
       
    }
}