using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Users;
using PrintCenter.Shared;
using User = PrintCenter.Shared.User;

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.SuperAdmin))]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody] Create.Command command)
        {
            var created = await mediator.Send(command);
            await mediator.Publish(new Create.Notification(created));
        }


        [HttpGet("Roles")]
        public async Task<List<Tuple<string, Role>>> GetAvailableRoles()
        {
            return await Task.FromResult(Enum.GetNames(typeof(Role)).Select(s =>
                new Tuple<string, Role>(Enum.Parse<Role>(s).GetDisplayName(), Enum.Parse<Role>(s))).ToList());
        }

        [HttpGet]
        public async Task<UsersEnvelope> Get([FromQuery] int limit, [FromQuery] int offset)
        {
            return await mediator.Send(new List.Query(limit, offset));
        }


        [HttpGet("{login}")]
        public async Task<User> Get(string login)
        {
            return await mediator.Send(new Details.Query(login));
        }

        [HttpPut]
        public async Task Edit([FromBody] Edit.Command command)
        {
            await mediator.Send(command);
        }

        [HttpDelete("{login}")]
        public async Task Delete(string login)
        {
            await mediator.Send(new Delete.Command(login));
        }
    }
}