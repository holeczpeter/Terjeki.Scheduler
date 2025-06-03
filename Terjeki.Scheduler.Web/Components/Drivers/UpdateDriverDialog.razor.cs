using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Drivers
{

    public partial class UpdateDriverDialog
    {
        [Inject] 
        IBusService BusService { get; set; }
        [Inject] 
        IDriverService DriverService { get; set; }

        [Parameter]
        public Guid SelectedId { get; set; } = default!;

        private UpdateDriverForm form = new();

        private EditContext editContext = new(new UpdateDriverForm());

        private List<BusItemModel> buses = new();

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        protected override async Task OnInitializedAsync()
        {
            var currentDriver = await DriverService.Get(SelectedId, _cancellationTokenSource.Token);
            buses = (await BusService.GetAll(_cancellationTokenSource.Token)).Select(x=> new BusItemModel() 
            {
              Id = x.Id,
              Brand = x.Brand,
              CurrentMileage = x.CurrentMileage,
              LicensePlateNumber = x.LicensePlateNumber
            }).ToList();

            form = new UpdateDriverForm()
            {
                Id = currentDriver.Id,
                UserId = currentDriver.UserId,
                DriverName = currentDriver.Name,
                Bus = currentDriver.Bus
            };

            editContext = new EditContext(form);

            await base.OnInitializedAsync();
        }

        private async Task OnSave()
        {
            if (editContext.Validate())
            {
                var request = new UpdateDriverCommand()
                {
                    Id = form.Id,
                    BusId = form.Bus?.Id,
                    DriverName = form.DriverName,
                    UserId = form.UserId
                };
                var result = await DriverService.Update(request, _cancellationTokenSource.Token);
                if (result == null)
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Sikertelen mentés",
                        Duration = 2000
                    });
                    DialogService.Close(null);
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = $"Sikeres mentés",
                        Duration = 2000
                    });
                    DialogService.Close(result);
                }
            }
        }

        private void Close()
        {
            DialogService.Close(null);
        }
    }

}
