using JobTo.Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JobTo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly ILogger<ClientController> _logger;
        readonly IClientRepository _repository;
        public ClientController(ILogger<ClientController> logger, IClientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Metodo para retornar todos os clientes
        /// </summary>
        /// <returns>
        /// Uma resposta com os dados dos clientes.
        /// </returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _repository.All();
            _logger.LogInformation($"Retornando {clients.Count} cliente(s)");
            return Ok(clients);
        }
    }
}
