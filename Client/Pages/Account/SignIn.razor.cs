using System;
using Blazored.SessionStorage;
using Blazored.Toast.Services;
using Client.Services;
using Client.Services.AccountService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.Account;

public partial class SignIn
{
    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; }
    [Inject]
    private NavigationManager _navigateManager { get; set; }
    [Inject]
    private IAccountService accountService { get; set; }
    [Inject]
    private ISessionStorageService _sessionStorage { get; set; }
    [Inject]
    private IToastService toastService { get; set; }

    EditContext editContext;
    bool IsLoading = false;
    SharedLibrary.DTO.Account.SignIn.Request signIn = new();
    protected override async Task OnInitializedAsync()
    {
        var user = (await AuthState).User;

        if (user.Identity.IsAuthenticated)
        {
            _navigateManager.NavigateTo("/");
        }

        editContext = new EditContext(signIn);
    }

    private async void HandleSignIn()
    {
        IsLoading = true;
        await InvokeAsync(StateHasChanged);
        var signInModel = (SharedLibrary.DTO.Account.SignIn.Request)editContext.Model;

        var response = await accountService.Sign(signInModel);

        if (!string.IsNullOrEmpty(response.Error))
        {
            IsLoading = false;
            await InvokeAsync(StateHasChanged);
            toastService.ShowError(response.Error);
            return;
        }

        _sessionStorage.SetItemAsync("Token", response.SecurityToken);
        _sessionStorage.SetItemAsync("StudentName", response.Name + " " + response.Surname);
        _sessionStorage.SetItemAsync("StudentNumber", response.StudentNumber);

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
        _navigateManager.NavigateTo("/", forceLoad: true);
    }
}
