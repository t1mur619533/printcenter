using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Materials;

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MaterialsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<MaterialsEnvelope> Get([FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await mediator.Send(new List.Query(limit, offset));
        }

        [HttpPost]
        public async Task Create([FromBody]Create.Command command)
        {
            await mediator.Send(command);
        }
    }
}
