using JobTo.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetClients()
        {
            return Ok(new List<People>
            {
                new People
                {
                    Id = 1,
                    Name = "SOTech Sistemas",
                    Document = "17.021.901/0001-62"
                }
            });
        }
    }
}
