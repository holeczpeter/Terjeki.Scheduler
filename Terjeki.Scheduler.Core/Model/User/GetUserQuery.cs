namespace Terjeki.Scheduler.Core
{
    public record GetUserQuery(Guid Id) : IRequest<UserModel>;
    
}
