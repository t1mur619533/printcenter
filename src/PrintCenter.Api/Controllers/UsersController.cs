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

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task Create([FromBody] Create.Command command)
        {
            await mediator.Send(command);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<UserEnvelope> Login([FromBody] Login.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<List<Tuple<string, Role>>> GetAvailableRoles()
        {
            return await Task.FromResult(Enum.GetNames(typeof(Role)).Select(s =>
                new Tuple<string, Role>(Enum.Parse<Role>(s).GetDisplayName(), Enum.Parse<Role>(s))).ToList());
        }
    }
}