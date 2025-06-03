namespace Terjeki.Scheduler.Core
{
    public interface ICurrentUserService
    {
        Guid? GetUserId();
        string GetUserName();
        string GetUserRole();
    }
}