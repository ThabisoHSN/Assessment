using System;
using System.Net.Http.Json;
using Client.Constants;
using SharedLibrary.DTO.Course;
using static System.Net.WebRequestMethods;

namespace Client.Services.CourseService;

public class CourseManagement : ICourseManagement
{
    private readonly IHttpClientFactory httpClient;
    private readonly HttpClient _http;

    public CourseManagement(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory;
        _http = httpClient.CreateClient("AssessmentApi");
    }

    public async Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> AvailableCourses()
    {
        try
        {
            var response = await _http.GetAsync(ClientConstants.CourseService.CoursesAvailable);

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
                {
                    Error = "Failed to retrieve available courses"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>>();

            return result ?? new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
            {
                Error = "Failed to parse response"
            };
        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
            {
                Error = ex.Message,
            };
        }
    }

    public async Task<RegistrationResponse> CourseDeRegistrationManagement(Request request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(ClientConstants.CourseService.CourseDeregister, request);

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.Course.RegistrationResponse()
                {
                    Error = "Failed to deregister"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.Course.RegistrationResponse>();

            return result;

        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.Course.RegistrationResponse()
            {
                Error = ex.Message,
            };
        }
    }

    public async Task<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>> EnrolledCourses(string StudentNumber)
    {
        try
        {
            var response = await _http.GetAsync(ClientConstants.CourseService.CoursesEnrolled + $"/{StudentNumber}");

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
                {
                    Error = "Failed to retrieve enrolled courses"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>>();

            return result ?? new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
            {
                Error = "Failed to parse response"
            };
        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.DynamicListResponse<SharedLibrary.DTO.Course.Response>
            {
                Error = ex.Message,
            };
        }
    }

    public async Task<RegistrationResponse> CourseRegistrationManagement(Request request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(ClientConstants.CourseService.CourseRegister, request);

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.Course.RegistrationResponse()
                {
                    Error = "Failed to register"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.Course.RegistrationResponse>();

            return result;

        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.Course.RegistrationResponse()
            {
                Error = ex.Message,
            };
        }
    }

    public async Task<Response> FindCourse(string CourseCode)
    {
        try
        {
            var response = await _http.GetAsync(ClientConstants.CourseService.FindCourse + $"/{CourseCode}");

            if (!response.IsSuccessStatusCode)
            {
                return new SharedLibrary.DTO.Course.Response
                {
                    Error = "Failed to retrieve course"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<SharedLibrary.DTO.Course.Response>();

            return result ?? new SharedLibrary.DTO.Course.Response
            {
                Error = "Failed to parse response"
            };
        }
        catch (Exception ex)
        {
            return new SharedLibrary.DTO.Course.Response
            {
                Error = ex.Message,
            };
        }
    }
}

