using ApiCobranca.Models;
using ApiCobranca.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneAPI.Services;
using System.Threading.Tasks;

namespace ApiCobranca.Controllers
{
    [ApiController]
    [Route("api/cobranca")]
    public class CobrancaController : ControllerBase
    {

        private readonly CobrancaService _cobrancaService;
        private ClienteService _clienteService;
        public CobrancaController(CobrancaService cobrancaService, ClienteService clienteService)
        {
            _cobrancaService = cobrancaService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Lista as cobranças.
        /// </summary>
        /// <returns>As cobranças cadastradas.</returns>
        /// <response code="200">Returna todas as cobranças cadastradas.</response>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            StatusCode(StatusCodes.Status200OK, await _cobrancaService.Get());

        /// <summary>
        /// Lista as cobranças por Id.
        /// </summary>
        /// <returns>As cobranças cadastradas com o id informado.</returns>
        /// <response code="200">Returna todas as cobranças cadastradas com o id informado.</response>
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var cobranca = await _cobrancaService.Get(id);

            if (cobranca == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, cobranca);
        }

        /// <summary>
        /// Lista as cobranças por cpf.
        /// </summary>
        /// <returns>As cobranças cadastradas com o cpf informado.</returns>
        /// <response code="200">Returna todas as cobranças cadastradas com o cpf informado.</response>
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            
            var cobranca = await _cobrancaService.GetByCpf(cpf);

            if (cobranca == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, cobranca);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cobranca cobranca)
        {

            if (cobranca.Cpf.Length < 11)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "O CPF deve conter 11 dígitos.");
            }

            var cliente = await _clienteService.GetByCpf(cobranca.Cpf);

            if(cliente == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "O CPF informado não está vinculado a nenhum cliente.");
            }

            await _cobrancaService.Create(cobranca);

            return StatusCode(StatusCodes.Status201Created, cobranca);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cobranca newCobranca)
        {
            var cobranca = await _cobrancaService.Get(id);

            if(cobranca == null)
            {
                return NotFound();
            }

           await _cobrancaService.Update(id, newCobranca);

            return StatusCode(StatusCodes.Status200OK, cobranca); ;
        }

    }
}
