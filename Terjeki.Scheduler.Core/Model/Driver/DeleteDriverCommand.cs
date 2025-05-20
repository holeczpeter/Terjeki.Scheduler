namespace Terjeki.Scheduler.Core
{
    public record DeleteDriverCommand(Guid Id) : IRequest<bool>;
}
