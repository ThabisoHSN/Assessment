using Backend.Services.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseReadService _courseReadService;
        private readonly ICourseManagementService _courseManagementService;

        public CourseController(ICourseReadService courseReadService, ICourseManagementService courseManagementService)
        {
            _courseReadService = courseReadService;
            _courseManagementService = courseManagementService;
        }

        [HttpGet("GetCourse/{courseCode}")]
        public async Task<IActionResult> GetCourse(string courseCode)
        {
            try
            {
                var response = await _courseReadService.GetCourse(courseCode);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = ex.Message });
            }
        }

        [HttpGet("enrolled/{studentNumber}")]
        public async Task<IActionResult> GetEnrolledCourses(string studentNumber)
        {
            try
            {
                var response = await _courseReadService.EnrolledCourses(studentNumber);

                return Ok(response);
            }
            catch (Exception ex)
            { 
                return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = ex.Message });
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCourses()
        {
            try
            {
                var studentNumber = User.FindFirst("StudentNumber")?.Value;
                
                if(string.IsNullOrEmpty(studentNumber))
                {
                    return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = "Invalid token"});
                }

                var response = await _courseReadService.AvailableCourses(studentNumber);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewCourse([FromBody] SharedLibrary.DTO.Course.Request request)
        {
            try
            {
                var response = await _courseManagementService.CourseRegistrationManagement(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = ex.Message });
            }
        }


        [HttpPost("deregister")]
        public async Task<IActionResult> DeregisterCourse(SharedLibrary.DTO.Course.Request request)
        {
            try
            {
                var response = await _courseManagementService.CourseDeRegistrationManagement(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new SharedLibrary.DTO.Course.Response() { Error = ex.Message });
            }
        }
    }
}
