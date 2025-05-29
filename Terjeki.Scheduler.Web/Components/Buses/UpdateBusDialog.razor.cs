using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Buses
{
    public partial class UpdateBusDialog : IDisposable
    {
        [Inject] IBusService BusService { get; set; }
        [Inject] ICapacityService CapacityService { get; set; }
        [Inject] IDriverService DriverService { get; set; }

        private UpdateBusForm form = new();

        private EditContext editContext = new(new UpdateBusForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;

        private List<string> buses;

        private List<DriverModel> drivers;

        [Parameter]
        public BusModel Selected { get; set; } = default!;

        private List<CapacityModel> capacities;
        protected override async Task OnInitializedAsync()
        {
            var allCapacities = await CapacityService.GetAll(_cancellationTokenSource.Token);
            capacities = allCapacities.ToList();

            var allDrivers = await DriverService.GetAll(_cancellationTokenSource.Token);
            drivers = allDrivers.ToList();

            var allBus = await BusService.GetAll(_cancellationTokenSource.Token);
            buses = allBus.Where(x => x.Id != Selected.Id).Select(x => x.Brand).ToList();

            form = new UpdateBusForm()
            {
                Id = Selected.Id,
                Brand = Selected.Brand,
                Capacity = Selected.Capacity,
                LicensePlateNumber = Selected.LicensePlateNumber,
                Description = Selected.Description,
                Driver = Selected.Driver,
                CurrentMileage = form.CurrentMileage,
            };

           

            editContext = new EditContext(form);
            messageStore = new ValidationMessageStore(editContext);
            editContext.OnFieldChanged += ValidateForm;
            form.PropertyChanged += OnFieldChanged;

            await base.OnInitializedAsync();
        }

        private void OnFieldChanged(object? sender, PropertyChangedEventArgs e)
        {
            editContext.NotifyFieldChanged(new FieldIdentifier(form, nameof(UpdateBusForm.Brand)));
        }

        private void ValidateForm(object? sender, FieldChangedEventArgs e)
        {
            if (e.FieldIdentifier.FieldName == nameof(UpdateBusForm.Brand))
            {
                ValidateName();
            }

            isFormValid = editContext.Validate();
            StateHasChanged();
        }

        private void ValidateName()
        {
            messageStore.Clear(new FieldIdentifier(form, nameof(UpdateBusForm.Brand)));

            if (buses.Any(n => n.Equals(form.Brand, StringComparison.OrdinalIgnoreCase)))
            {
                messageStore.Add(new FieldIdentifier(form, nameof(UpdateBusForm.Brand)), $"A '{form.Brand}' nevű busz már létezik");
            }

            editContext.NotifyValidationStateChanged();
        }
        public async Task OnSave()
        {
            if (!string.IsNullOrEmpty(form.Brand))
            {
                var request = new UpdateBusCommand()
                {
                    Id = form.Id,
                    Name = form.Brand,
                    Description = form.Description,
                    LicensePlateNumber = form.LicensePlateNumber,
                    Capacity = form.Capacity,
                    DriverId = form.Driver?.Id,
                    CurrentMileage = form.CurrentMileage,
                };
                var result = await BusService.Update(request,_cancellationTokenSource.Token);
                if (result == null)
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Sikertelen mentés",
                        Duration = 2000
                    });
                    DialogService.Close(result);
                    DialogService.CloseSide(result);
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = $"Sikeres rögzítés",
                        Duration = 2000
                    });
                    DialogService.Close(result);
                    DialogService.CloseSide(result);
                }

            }
        }
        private void Close()
        {
            DialogService.Close(null);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
