namespace Terjeki.Scheduler.Core
{
    public record DeleteEventCommand(Guid Id) : IRequest<bool>;
}
