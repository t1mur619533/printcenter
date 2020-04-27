using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
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

        //https://localhost:5001/api/customers?filter={}&range=[0,9]&sort=["id","ASC"]
        //{ pagination: { page: {int} , perPage: {int} }, sort: { field: {string}, order: {string} }, filter: {Object} }
        [HttpGet]
        public async Task<List<Customer>> Get(
            [FromQuery] string sortField,
            [FromQuery] string order,
            [FromQuery] string filter, 
            [FromQuery] string searchString, 
            [FromQuery] int page,
            [FromQuery] int perPage)
        {
            var result = await mediator.Send(new List.Query(sortField, order, filter, searchString, page, perPage));
            Response.Headers.Add("Content-Range", $"customers {0}-{result.Customers.Count}/{result.Total}");
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
