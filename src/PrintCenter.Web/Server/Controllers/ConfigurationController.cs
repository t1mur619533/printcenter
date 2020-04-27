using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PrintCenter.Shared;

namespace PrintCenter.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IOptions<Configuration> configuration;

        public ConfigurationController(IOptions<Configuration> configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public Configuration Get()
        {
            return configuration.Value;
        }
    }
}