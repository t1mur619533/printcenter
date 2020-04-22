using MediatR;

namespace PrintCenter.Domain.Infrastructure
{
    public abstract class Notification<TContent> : INotification
    {
        public TContent Content { get; protected set; }
    }
}
