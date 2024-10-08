
using Blazored.SessionStorage;
using Blazored.Toast.Services;
using Client.Services.CourseService;
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Pages;

public partial class Home
{

    [Inject]
    public ISessionStorageService _sessionStorage { get; set; }
    [Inject]
    public ICourseManagement _courseManagement { get; set; }
    [Inject]
    private IToastService toastService { get; set; }

    private string CurrentDate;
    private string Month;
    private int AvailableCourses;
    private int EnrolledCourses;
    private bool IsLoading = false;
    private string CourseCode;

    private List<SharedLibrary.DTO.Course.Response> Courses = [
        new SharedLibrary.DTO.Course.Response{CourseName = "Mathematics", CourseCode = "M2024", ClassMembers = 100, CourseDescription= "This is a new course", RegisteredDate = DateTime.Now.AddDays(-3),}
    ];

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await InvokeAsync(StateHasChanged);

        CurrentDate = DateTime.Now.ToString("dd");
        Month = DateTime.Now.ToString("MMMM");

        await RefreshDataAsync();

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task RefreshDataAsync()
    {
        var available = await _courseManagement.AvailableCourses();
        AvailableCourses = available?.Result?.Count ?? 0;

        var studentNumber = await _sessionStorage.GetItemAsync<string>("StudentNumber");

        if (!string.IsNullOrEmpty(studentNumber))
        {
            var enrolled = await _courseManagement.EnrolledCourses(studentNumber);
            EnrolledCourses = enrolled?.Result?.Count ?? 0;
            var results = await _courseManagement.EnrolledCourses(studentNumber);
            Courses = results.Result ?? [];
        }

    }

    private async Task OnDelete(string courseCode)
    {
        CourseCode = courseCode;
        await InvokeAsync(StateHasChanged);
    }

    private async Task Deregister(bool resp)
    {
        if (!resp)
        {
            CourseCode = string.Empty;
            await InvokeAsync(StateHasChanged);
            return;
        }

        IsLoading = true;
        await InvokeAsync(StateHasChanged);

        var studentNumber = await _sessionStorage.GetItemAsync<string>("StudentNumber");
        if (string.IsNullOrEmpty(studentNumber))
        {
            IsLoading = false;
            CourseCode = string.Empty;
            await InvokeAsync(StateHasChanged);
            toastService.ShowError("Student Number not found");
            return;
        }

        SharedLibrary.DTO.Course.Request request = new()
        {
            StudentNumber = studentNumber,
            CourseCode = CourseCode,
            IsRegistered = true,
        };

        var response = await _courseManagement.CourseDeRegistrationManagement(request);

        if (!string.IsNullOrEmpty(response.Error))
        {
            IsLoading = false;
            CourseCode = string.Empty;
            await InvokeAsync(StateHasChanged);
            toastService.ShowError(response.Error);
            return;
        }

        await RefreshDataAsync();
        IsLoading = false;
        CourseCode = string.Empty;
        await InvokeAsync(StateHasChanged);
        toastService.ShowSuccess("Deregistered");

    }


}
