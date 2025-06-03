namespace Terjeki.Scheduler.Core
{
    public record GetAllowedEmailQuery(Guid Id) : IRequest<AllowedEmailModel>;
    
}
