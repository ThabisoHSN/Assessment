using Backend.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO.Account.SignIn;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService){
            _accountService = accountService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SharedLibrary.DTO.Account.SignIn.Request request){
            try
            {
                var account = await _accountService.SignIn(request);

                return Ok(account);
            }
            catch(Exception ex){
                return BadRequest(new Response(){Error = ex.Message});
            }
        }

        [HttpPost("register")]

         public async Task<IActionResult> Register([FromBody] SharedLibrary.DTO.Account.Request request){
            try
            {
                var account = await _accountService.RegisterStudent(request);

                return Ok(account);
            }
            catch(Exception ex){
                return BadRequest(new Response(){Error = ex.Message});
            }
        }
    }
    
}
