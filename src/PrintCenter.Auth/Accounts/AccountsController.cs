using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Users;
using PrintCenter.Infrastructure.Accessors;

namespace PrintCenter.Auth.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserIdentifier currentUserIdentifier;

        public AccountsController(IMediator mediator, ICurrentUserIdentifier currentUserIdentifier)
        {
            this.mediator = mediator;
            this.currentUserIdentifier = currentUserIdentifier;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<AccountEnvelope> Post([FromBody] Login.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<UserEnvelope> Get()
        {
            return await mediator.Send(new Details.Query(currentUserIdentifier.GetUsername()));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task Put([FromBody] EditPassword.Command command)
        {
            command.Login = currentUserIdentifier.GetUsername();
            await mediator.Send(command);
        }
    }
}
