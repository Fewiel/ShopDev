namespace ShopDev.APIModels;

public class RequestBase
{
    public Guid? UserId { get; set; }
    public Token? Token { get; set; }

    public virtual bool Validate()
    {
        return UserId != null && Token != null;
    }
}