using Exam.Application.Models;
using Exam.Application.Models.DTOs.AccountDTOs;
using Exam.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                var token = await _accountService.Register(model);
                var result = new Response<string>(201, "Пользователь успешно зарегистрирован", token);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var result = new Response<string>(400, ex.Message);
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var token = await _accountService.Login(login);
                var result = new Response<string>(200, "Пользователь успешно вошел в систему", token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new Response<string>(400, ex.Message);
                return BadRequest(result);
            }
        }
    }
}
