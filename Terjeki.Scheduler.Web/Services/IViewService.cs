

namespace Terjeki.Scheduler.Web.Services
{
    public interface IViewService
    {
        public ViewTypes ViewType { get; set; }

        public Action<ViewTypes> OnViewChange { get; set; }
    }
}
