using Terjeki.Scheduler.Web.Components.Drivers;

namespace Terjeki.Scheduler.Web.Pages
{
    public partial class Drivers
    {
        private IList<DriverModel> drivers = new List<DriverModel>();
        [Inject] IDriverService DriverService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            drivers = new List<DriverModel>();
            var query = new GetDriversQuery();
            drivers = (List<DriverModel>)await DriverService.GetAll();
            StateHasChanged();
        }
        private async Task OnCreate()
        {
            var result = await DialogService.OpenAsync<CreateDriverDialog>($"Sofőr felvitele");
            if (result != null) await Refresh();

        }
        private async Task OnEdit(DriverModel driver)
        {
            var parameters = new Dictionary<string, object>() { { "Selected", driver } };
            var result = await DialogService.OpenAsync<UpdateDriverDialog>($"{driver.Name} adatainak módosítása", parameters);
            if (result != null) await Refresh();
        }
        private async Task OnDelete(Guid id)
        {
            var result = await DriverService.Delete(new DeleteDriverCommand(id), new CancellationToken());
            if (result) await Refresh();
        }
    }
}
