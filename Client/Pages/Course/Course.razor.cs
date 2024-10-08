using System;
using Blazored.SessionStorage;
using Blazored.Toast.Services;
using Client.Services.CourseService;
using Microsoft.AspNetCore.Components;
using SharedLibrary.DTO.Course;

namespace Client.Pages.Course;

public partial class Course
{
    [Parameter]
    public string CourseId { get; set; }

    [Inject]
    public ICourseManagement _courseManagement { get; set; }
    [Inject]
    public ISessionStorageService _sessionStorage { get; set; }

    [Inject]
    public NavigationManager _navigate { get; set; }
    [Inject]
    private IToastService toastService { get; set; }

    bool IsLoading = false;

    private SharedLibrary.DTO.Course.Response course = new();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await InvokeAsync(StateHasChanged);
        course = await _courseManagement.FindCourse(CourseId);
        IsLoading = false;
        await InvokeAsync(StateHasChanged);

        if (!string.IsNullOrEmpty(course.Error))
        {
            _navigate.NavigateTo("/courses");
        }
    }

    private void HandleCancel(bool result)
    {
        _navigate.NavigateTo("/courses");
    }

    private async void HandleRegistration()
    {
        IsLoading = true;
        await InvokeAsync(StateHasChanged);
        var studentNumber = await _sessionStorage.GetItemAsync<string>("StudentNumber");

        if (string.IsNullOrEmpty(studentNumber))
        {
            IsLoading = false;
            await InvokeAsync(StateHasChanged);
            toastService.ShowError("Student not found");
            return;
        }

        SharedLibrary.DTO.Course.Request request = new()
        {
            StudentNumber = studentNumber,
            CourseCode = CourseId,
            IsRegistered = true,
        };

        var response = await _courseManagement.CourseRegistrationManagement(request);

        if (!string.IsNullOrEmpty(response.Error))
        {
            IsLoading = false;
            await InvokeAsync(StateHasChanged);
            toastService.ShowError(response.Error);
            return;
        }
        toastService.ShowSuccess("You've been registered to the course");
        IsLoading = false;
        await InvokeAsync(StateHasChanged);
        _navigate.NavigateTo("/courses");

    }
}
