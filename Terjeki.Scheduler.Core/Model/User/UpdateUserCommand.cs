namespace Terjeki.Scheduler.Core
{
    public class UpdateUserCommand : CreateUserCommand, IRequest<UserModel>
    {
        public Guid Id { get; set; }
    }
}
