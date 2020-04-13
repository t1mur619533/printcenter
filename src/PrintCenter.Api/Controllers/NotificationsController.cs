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
        private readonly ICurrentUserAccessor currentUserAccessor;

        public NotificationsController(IMediator mediator, ICurrentUserAccessor currentUserAccessor)
        {
            this.mediator = mediator;
            this.currentUserAccessor = currentUserAccessor;
        }

        [HttpPut]
        public async Task Post(int id, [FromQuery] int? minutes)
        {
            await mediator.Send(new Remind.Command(currentUserAccessor.GetUsername(), id, minutes));
        }

        [HttpGet]
        public async Task<NotificationsEnvelope> Get([FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await mediator.Send(new List.Query(currentUserAccessor.GetUsername(), offset, limit));
        }

        [HttpDelete]
        public async Task Delete([FromBody] List<int> notifications)
        {
            await mediator.Send(new Delete.Command(currentUserAccessor.GetUsername(), notifications));
        }
    }
}