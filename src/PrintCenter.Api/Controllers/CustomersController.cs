using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Customers;

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Customer>> Get([FromQuery] int limit, [FromQuery] int offset)
        {
            var result = await mediator.Send(new List.Query(limit, offset));
            Response.Headers.Add("Content-Range", $"customers {offset}-{result.Customers.Count}/{result.Total}");
            return result.Customers;
        }
        
        [HttpPost]
        public async Task Create([FromBody]Create.Command command)
        {
            await mediator.Send(command);
        }
        
        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
            return await mediator.Send(new Details.Query(id));
        }
        
        [HttpPut("{id}")]
        public async Task Edit(int id, [FromBody]Edit.Command command)
        {
            command.Id = id;
            await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mediator.Send(new Delete.Command(id));
        }
    }
}
