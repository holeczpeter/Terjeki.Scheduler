namespace Terjeki.Scheduler.Application
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public CreateUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.CreateUser(request, cancellationToken);
        }
    }
}
