namespace Terjeki.Scheduler.Web.Services
{
    public interface IUserService
    {  
        /// <summary>
       /// Visszaadja a „Driver” szerepben lévő összes regisztrált felhasználót.
       /// </summary>
        Task<IEnumerable<UserModel>> GetAllDrivers(CancellationToken cancellationToken = default);
    }
}