namespace Terjeki.Scheduler.Core
{
    public record DeleteBusCommand(Guid Id) : IRequest<bool>;
}
