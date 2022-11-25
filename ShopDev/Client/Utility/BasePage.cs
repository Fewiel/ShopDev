using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopDev.Client.Models;

namespace ShopDev.Client.Utility;

public class BasePage : ComponentBase
{
    [Inject]
    protected AppState State { get; set; } = null!;
    [Inject]
    protected ILocalStorageService Storage { get; set; } = null!;
    [Inject]
    protected NavigationManager NavManager { get; set; } = null!;

    protected override async Task OnInitializedAsync() => await CheckLoginNavigateAsync();

    protected async Task<bool> CheckLoginAsync()
    {
        if (State.User == null)
            State.User = await Storage.GetItemAsync<UserState>("User");

        return State.LoggedIn;
    }

    protected async Task<bool> CheckLoginNavigateAsync()
    {
        if (!await CheckLoginAsync())
        {
            State.NavigationURLAfterAction = "/" + NavManager.ToBaseRelativePath(NavManager.Uri);
            NavManager.NavigateTo("/Login");
            return false;
        }

        return true;
    }
}