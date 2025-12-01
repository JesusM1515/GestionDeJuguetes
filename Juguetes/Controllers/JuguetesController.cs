using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juguetes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuguetesController : ControllerBase
    {
        private readonly IJuguetes _juguetesService;

        public JuguetesController(IJuguetes juguetesService)
        {
            _juguetesService = juguetesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddJuguete([FromBody] JuguetesDTO dto)
        {
            var result = await _juguetesService.AddJuguete(dto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJuguete(string id, [FromBody] JuguetesUpdateDTO dto)
        {
            var result = await _juguetesService.Updateasync(id, dto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _juguetesService.GetAllasync();

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _juguetesService.GetbyIdasync(id);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("getByName/{nombre}")]
        public async Task<IActionResult> GetByName(string nombre)
        {
            var result = await _juguetesService.GetbyNameasync(nombre);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id, [FromQuery] bool confirmar)
        {
            var result = await _juguetesService.DeleteAsync(id, confirmar);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
