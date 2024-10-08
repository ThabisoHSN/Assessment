using Backend.Helper;
using Backend.Services.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentCreateService _studentCreateService;
        private readonly IStudentManagementService _studentManagementService;
        private readonly IStudentReadService _studentReadService;
        public StudentController(IStudentCreateService studentCreateService, IStudentReadService studentReadService, IStudentManagementService studentManagementService)
        {
            _studentCreateService = studentCreateService;
            _studentReadService = studentReadService;
            _studentManagementService = studentManagementService;
        }
               

        [HttpPut("{GID}")]
        public async Task<IActionResult> UpdateStudent([FromBody] SharedLibrary.DTO.Student.Request request, Guid GID)
        {
            try
            {

                var student = await _studentReadService.GetStudentByGID(GID);

                if (student == null)
                {
                    return NotFound(new SharedLibrary.DTO.Student.Response() { Error = "This student is not found" });
                }

                if (!GlobalHelper.CompareObject(request, student))
                {
                    return Ok(new SharedLibrary.DTO.Student.Response() { Error = "No changes made" });
                }

                if (!await _studentManagementService.UpdateStudent(request))
                {
                    return BadRequest(new SharedLibrary.DTO.Student.Response() { Error = "Failed to update student" });
                }

                return Ok();

            }
            catch (Exception error)
            {
                return BadRequest(new SharedLibrary.DTO.Student.Response() { Error = error.Message });
            }
        }
    }
}
