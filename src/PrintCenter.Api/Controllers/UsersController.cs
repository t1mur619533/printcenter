using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}