namespace Terjeki.Scheduler.Core
{
    public record GetAllowedEmailsQuery() : IRequest<IEnumerable<AllowedEmailModel>>;
}
