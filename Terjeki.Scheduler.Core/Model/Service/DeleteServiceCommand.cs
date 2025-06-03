namespace Terjeki.Scheduler.Core
{
    public record DeleteServiceCommand(Guid Id) : IRequest<bool>;
}


