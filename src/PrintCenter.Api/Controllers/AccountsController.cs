using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Accounts;
using PrintCenter.Domain.Users;
using PrintCenter.Infrastructure.Accessors;
using PrintCenter.Shared;

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
        public async Task<Account> Post([FromBody] LoginData loginData)
        {
            return await mediator.Send(new Login.Command(loginData));
        }

        [HttpGet]
        public async Task<UserDetail> Get()
        {
            return await mediator.Send(new Details.Query(currentUserAccessor.GetUsername()));
        }

        [HttpPut]
        public async Task Put([FromBody] EditPasswordData editPasswordData)
        {
            await mediator.Send(new EditPassword.Command(currentUserAccessor.GetUsername(), editPasswordData));
        }
    }
}
