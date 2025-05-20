namespace Terjeki.Scheduler.Core
{
    public record DeleteUserCommand(Guid Id) : IRequest<bool>;
    
}
