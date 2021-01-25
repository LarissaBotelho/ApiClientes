using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneAPI.Models;
using StoneAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneAPI.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("teste")]
        public ActionResult<IEnumerable<string>> GetTest()
        {
            return new string[] { "teste1", "teste2" };
        }

        /// <summary>
        /// Lista os clientes.
        /// </summary>
        /// <returns>Os clientes cadastrados.</returns>
        /// <response code="200">Retorna todos os clientes cadastrados.</response>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            StatusCode(StatusCodes.Status200OK, await _clienteService.Get());

        /// <summary>
        /// Lista os clientes por Id.
        /// </summary>
        /// <returns>Os clientes cadastrados com o id informado.</returns>
        /// <response code="200">Retorna todos os clientes cadastrados com o id informado.</response>
        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public async Task<IActionResult> Get(string id)
        {
            var cliente = await _clienteService.Get(id);

            if (cliente == null)
            {
                return StatusCode(StatusCodes.Status200OK, "O CPF informado não está vinculado a nenhum cliente.");
            }

            return StatusCode(StatusCodes.Status200OK, cliente);
        }

        /// <summary>
        /// Lista os clientes por cpf.
        /// </summary>
        /// <returns>Os clientes cadastrados com o cpf informado.</returns>
        /// <response code="200">Returna todos os clientes cadastrados com o cpf informado.</response>
        [HttpGet("{cpf}", Name = "GetClienteByCpf")]
        public async Task<ActionResult> GetByCpf(string cpf)
        {
            var cliente = await _clienteService.GetByCpf(cpf);

            if (cliente == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            var clienteExistente = await _clienteService.GetByCpf(cliente.Cpf);

            if(clienteExistente != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "O CPF informado já existe.");
            }

            if(cliente.Cpf.Length < 11)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "O CPF deve conter 11 dígitos.");
            }

            await _clienteService.Create(cliente);

            return StatusCode(StatusCodes.Status201Created, cliente);
        }
    }
}
