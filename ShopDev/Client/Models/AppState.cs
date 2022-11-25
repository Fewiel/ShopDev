namespace ShopDev.Client.Models;

public class AppState
{
    public bool LoggedIn => User != null;
    public UserState? User { get; set; }

    public string NavigationURLAfterAction { get; set; } = "/Home";
}