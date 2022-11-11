namespace ShopDev.APIModels;

public class RequestBase
{
    public Guid? UserId { get; set; }
    public Token? Token { get; set; }
}