namespace Terjeki.Scheduler.Core
{
    public class UpdateAllowedEmailCommand : CreateAllowedEmailCommand, IRequest<AllowedEmailModel>
    {
        public Guid Id { get; set; }
    }
}
