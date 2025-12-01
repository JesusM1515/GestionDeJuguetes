using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juguetes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUser _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginUser loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation("Solicitud iniciada para iniciar sesion");

            if (loginDTO == null)
            {
                _logger.LogWarning("Los datos estan vacios");
                return BadRequest(new { mensaje = "Informacion no valida" });
            }

            var result = await _loginService.Login(loginDTO);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Error al iniciar sesion");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
