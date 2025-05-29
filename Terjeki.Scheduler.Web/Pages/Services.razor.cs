
using Terjeki.Scheduler.Web.Components.Services;

namespace Terjeki.Scheduler.Web.Pages
{
    public partial class Services
    {
        [Inject] DialogService DialogService { get; set; } = default!;
        [Inject] IServiceService ServiceService { get; set; }
        [Inject] IEventService EventService { get; set; }

        private IList<EventModel> services = new List<EventModel>();

        RadzenDataGrid<EventModel> busesGrid;
      
        protected override async Task OnInitializedAsync()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            services = new List<EventModel>();

            var query = new GetServiceEventsQuery();
            services = (List<EventModel>)await ServiceService.GetAll();
            StateHasChanged();
        }
        string GetColor(EventModel data)
        {
            var color = data.ServiceType switch
            {
                ServiceTypes.Other => "orange",
                ServiceTypes.Inspection => "blue",
                ServiceTypes.OilChange => "red",
                _ => "gray"
            };
            return $" width: 5px;height: 25px;background-color: {color};";
        }
        void RowRender(RowRenderEventArgs<EventModel> args)
        {
        }
        void CellRender(DataGridCellRenderEventArgs<EventModel> args)
        {
            var color = args.Data.ServiceType switch
            {
                ServiceTypes.Other => "orange",
                ServiceTypes.Inspection => "blue",
                ServiceTypes.OilChange => "red",
                _ => "transparent"
            };
            args.Attributes.Add("style", $"background-color: {color};");
        }
        private async Task OnCreate()
        {
            var result = await DialogService.OpenAsync<CreateServiceDialog>($"Szervíz időpont felvitele");
            if (result != null) await Refresh();

        }
        private async Task OnEdit(EventModel eventModel)
        {
            var parameters = new Dictionary<string, object>() { { "Selected", eventModel } };
            var result = await DialogService.OpenAsync<UpdateServiceDialog>($"{eventModel.Bus.Brand} szervíz módosítása", parameters);
            if (result != null) await Refresh();
        }
        private async Task OnDelete(Guid id)
        {
            var result = await EventService.Delete(new DeleteEventCommand(id), new CancellationToken()); 
            if (result) await Refresh();
        }
    }
}
