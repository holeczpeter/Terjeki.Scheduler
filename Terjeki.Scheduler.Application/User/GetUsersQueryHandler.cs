namespace Terjeki.Scheduler.Application.User
{
    internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetUsersQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<UserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetUsers(cancellationToken);
        }
    }
}
