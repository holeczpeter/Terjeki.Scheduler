namespace Terjeki.Scheduler.Web.Pages
{
    public partial class Buses : IDisposable
    {
        private IList<BusModel> buses = new List<BusModel>();

        RadzenDataGrid<BusModel> busesGrid;
        [Inject] DialogService DialogService { get; set; } = default!;
        [Inject] IBusService BusService { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            buses = (await BusService.GetAll()).ToList();
            StateHasChanged();
        }
        private async Task OnCreate()
        {
            var result = await DialogService.OpenAsync<CreateBusDialog>($"Busz felvitele");
            if (result != null) await Refresh();

        }
        private async Task OnTimeLine(BusModel bus)
        {
            var parameters = new Dictionary<string, object>() { { "Selected", bus } };
            var result = await DialogService.OpenAsync<BusHistoryDialog>($"{bus.Brand} szervíz történet", parameters);
            if (result != null) await Refresh();
        }
        private async Task OnEdit(BusModel bus)
        {
            var parameters = new Dictionary<string, object>() { { "Selected", bus } };
            var result = await DialogService.OpenAsync<UpdateBusDialog>($"{bus.Brand} adatainak módosítása", parameters);
            if (result != null) await Refresh();
        }
        private async Task OnDelete(Guid id)
        {
            var result = await BusService.Delete(new DeleteBusCommand(id), new CancellationToken());
            if (result) await Refresh();
        }
        public void Dispose()
        {
            
        }
    }
}
