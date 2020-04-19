using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintCenter.Domain.Notifications;
using PrintCenter.Infrastructure.Accessors;

namespace PrintCenter.Api.Controllers
{
    [Route("api/Users/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserIdentifier currentUserIdentifier;

        public NotificationsController(IMediator mediator, ICurrentUserIdentifier currentUserIdentifier)
        {
            this.mediator = mediator;
            this.currentUserIdentifier = currentUserIdentifier;
        }

        [HttpPut]
        public async Task Post(int id, [FromQuery] int? minutes)
        {
            await mediator.Send(new Remind.Command(currentUserIdentifier.GetUsername(), id, minutes));
        }

        [HttpGet]
        public async Task<NotificationsEnvelope> Get([FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await mediator.Send(new List.Query(currentUserIdentifier.GetUsername(), offset, limit));
        }

        [HttpDelete]
        public async Task Delete([FromBody] List<int> notifications)
        {
            await mediator.Send(new Delete.Command(currentUserIdentifier.GetUsername(), notifications));
        }
    }
}