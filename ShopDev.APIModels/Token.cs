namespace ShopDev.APIModels;

public class Token
{
    public string? Content { get; set; }
    public DateTime ExpireTime { get; set; }
}