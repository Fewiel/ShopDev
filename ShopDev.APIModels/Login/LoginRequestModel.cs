namespace ShopDev.APIModels.Login;

public class LoginRequestModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public bool Validate()
    {
        if (string.IsNullOrEmpty(Username))
            return false;

        if (string.IsNullOrEmpty(Password)) 
            return false;

        return true;
    }
}