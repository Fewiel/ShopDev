namespace ShopDev.APIModels.Login;

public class LoginResponseModel : ResponseBase<LoginResponseModel>
{
    public Guid UserId { get; set; }
    public Token? Token { get; set; }
    public List<string>? Permissions { get; set; }
    public Dictionary<string, int>? Limits { get; set; }
}