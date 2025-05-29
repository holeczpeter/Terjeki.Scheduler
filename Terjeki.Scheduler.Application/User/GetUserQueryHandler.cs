namespace Terjeki.Scheduler.Application
{
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetUserQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetUser(request.Id, cancellationToken);
        }
    }
}
