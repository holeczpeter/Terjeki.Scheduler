namespace Terjeki.Scheduler.Core
{
    public record DeleteAllowedEmailCommand(Guid Id) : IRequest<bool>;
    
}
