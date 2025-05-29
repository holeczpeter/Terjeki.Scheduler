namespace Terjeki.Scheduler.Application
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public UpdateUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.UpdateUser(request, cancellationToken);
        }
    }
}
