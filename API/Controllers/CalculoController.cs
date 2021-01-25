using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneAPI.Services;
using System.Threading.Tasks;

namespace StoneAPI.Controllers
{
    [ApiController]
    [Route("api/calcular")]
    public class CalculoController : ControllerBase
    {
        private readonly CalculoService _calculoService;

        public CalculoController(CalculoService calculoService)
        {
            _calculoService = calculoService;
        }

        /// <summary>
        /// Executa o cálculo das cobranças.
        /// </summary>
        /// <response code="200">Calculo todas as cobranças para os clientes cadastrados.</response>
        [HttpGet]
        public async Task<IActionResult> Calcular()
        {
            await _calculoService.calculaValores();

            return StatusCode(StatusCodes.Status200OK, "Calculo realizado com sucesso."); ;
        }
            
    }
}
