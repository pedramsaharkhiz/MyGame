using System.Threading.Tasks;
using API.Data;
using API.Dtos.User;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request) 
        { 
            var response=await _authRepository.Register(
                new User{Username=request.Username},request.Password
            );
            if(!response.Success){
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request) 
        { 
            var response=await _authRepository.Login(
                request.Username,request.Password
            );
            if(!response.Success){
                return BadRequest(response.Message);
            }
            return Ok($"Welcome {response.Message} ! it's your Token : {response.Data} !");
        }
    }
}