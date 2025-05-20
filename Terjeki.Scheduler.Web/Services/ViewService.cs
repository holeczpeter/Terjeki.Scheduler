using Terjeki.Scheduler.Core.Enums;

namespace Terjeki.Scheduler.Web.Services
{
    public class ViewService : IViewService
    {
        
        private ViewTypes _viewType = ViewTypes.Desktop;
        public ViewTypes ViewType 
        {
            get { return _viewType; }
            set
            {
                _viewType = value;
                OnViewChange?.Invoke(value);
            }
        }
        public Action<ViewTypes> OnViewChange { get; set; }
    }
}
