using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Accounts;
using PrintCenter.Domain.Users;
using PrintCenter.Infrastructure.Accessors;

namespace PrintCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserAccessor currentUserAccessor;

        public AccountsController(IMediator mediator, ICurrentUserAccessor currentUserAccessor)
        {
            this.mediator = mediator;
            this.currentUserAccessor = currentUserAccessor;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<AccountEnvelope> Post([FromBody] Login.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpGet]
        public async Task<UserEnvelope> Get()
        {
            return await mediator.Send(new Details.Query(currentUserAccessor.GetUsername()));
        }

        [HttpPut]
        public async Task Put([FromBody] EditPassword.Command command)
        {
            command.Login = currentUserAccessor.GetUsername();
            await mediator.Send(command);
        }
    }
}
