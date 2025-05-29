using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Drivers
{
    public partial class UpdateDriverDialog
    {
        [Inject] IBusService BusService { get; set; }
        [Inject] IDriverService DriverService { get; set; }

        private UpdateDriverForm form = new();

        private EditContext editContext = new(new UpdateDriverForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;

        private List<BusModel> buses;

        [Parameter]
        public DriverModel Selected { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var allBus = await BusService.GetAll(_cancellationTokenSource.Token);
            buses = allBus.ToList();

            form = new UpdateDriverForm()
            {
                Id = Selected.Id,
                Name = Selected.Name,
                Bus = Selected.Bus
            };


            editContext = new EditContext(form);
            messageStore = new ValidationMessageStore(editContext);
            editContext.OnFieldChanged += ValidateForm;
            form.PropertyChanged += OnFieldChanged;
            await base.OnInitializedAsync();
        }

        private void OnFieldChanged(object? sender, PropertyChangedEventArgs e)
        {
            editContext.NotifyFieldChanged(new FieldIdentifier(form, nameof(CreateDriverForm.Driver)));
        }

        private void ValidateForm(object? sender, FieldChangedEventArgs e)
        {

            isFormValid = editContext.Validate();
            StateHasChanged();
        }


        public async Task OnSave()
        {
            if (isFormValid)
            {
                var request = new UpdateDriverCommand()
                {
                    Id = form.Id,
                    BusId = form.Bus?.Id,
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
                    DialogService.Close(result);
                    DialogService.CloseSide(result);
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
                    DialogService.CloseSide(result);
                }

            }
        }
        private void Close()
        {
            DialogService.Close(null);
        }
    }
}
