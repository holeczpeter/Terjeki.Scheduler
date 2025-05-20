namespace Terjeki.Scheduler.Application.User
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;
        public DeleteUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.DeleteUser(request.Id, cancellationToken);
        }
    }
}
