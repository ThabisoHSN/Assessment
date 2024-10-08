using Blazored.Toast.Services;
using Client.Services.CourseService;
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Pages.Course;

public partial class Courses
{
    [Inject]
    private ICourseManagement _courseManagement { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    bool IsLoading = false;
    private List<SharedLibrary.DTO.Course.Response> CourseList = [
        new SharedLibrary.DTO.Course.Response{CourseName = "Mathematics", CourseCode = "M2024", ClassMembers = 100, CourseDescription= "This is a new course", RegisteredDate = DateTime.Now.AddDays(-3),}
    ];

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await InvokeAsync(StateHasChanged);
        var results = await _courseManagement.AvailableCourses();
        IsLoading = false;
        await InvokeAsync(StateHasChanged);
        CourseList = results.Result;

    }

    void HandleGoToCourse(string courseCode)
    {
        _navigationManager.NavigateTo($"/course/{courseCode}");
    }
}
