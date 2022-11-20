namespace ShopDev.APIModels.Login;

public class LoginRequestModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public bool Validate(out Dictionary<string, string>? validationErrors)
    {
        validationErrors = new();

        if (string.IsNullOrEmpty(Username))
            validationErrors.Add("Username", "Username is required!");

        if (string.IsNullOrEmpty(Password))
            validationErrors.Add("Password", "Password is required!");

        return validationErrors.Count == 0;
    }
}