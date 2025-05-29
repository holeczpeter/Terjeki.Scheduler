using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Services
{
    public partial class CreateServiceDialog
    {
        [Inject] IServiceService ServiceService { get; set; }
        [Inject] IBusService BusService { get; set; }
       
        [Inject] IEventService EventService { get; set; }

        private CreateServiceForm form = new();

        private EditContext editContext = new(new CreateServiceForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;


        private List<ServiceModel> serviceTypes;
        private List<BusModel> buses;
        private List<EventModel> events;
        protected override async Task OnInitializedAsync()
        {
            var allEvents = await EventService.GetEvents(_cancellationTokenSource.Token);
            events = allEvents.ToList();

            var allBus = await BusService.GetAll(_cancellationTokenSource.Token);
            buses = allBus.ToList();

            var allServiceTypes = await ServiceService.GetAllTypesAsync(_cancellationTokenSource.Token);
            serviceTypes = allServiceTypes.ToList();

            editContext = new EditContext(form);
            messageStore = new ValidationMessageStore(editContext);
            editContext.OnFieldChanged += ValidateForm;
            await base.OnInitializedAsync();
        }



        private void ValidateForm(object? sender, FieldChangedEventArgs e)
        {
            messageStore.Clear();

            if (form.Start != default && form.End != default)
            {
                if (form.Start > form.End)
                {
                    messageStore.Add(new FieldIdentifier(form, nameof(form.Start)), "A kezdő dátum nem lehet nagyobb, mint a befejezés dátum.");
                    messageStore.Add(new FieldIdentifier(form, nameof(form.End)), "A befejezés dátuma nem lehet kisebb, mint a kezdés.");
                }

                if (form.Bus != null)
                {
                    var overlapping = events.Any(ev =>
                        ev.Bus.Id == form.Bus.Id &&
                        (
                            (form.Start >= ev.StartDate && form.Start < ev.EndDate) ||
                            (form.End > ev.StartDate && form.End <= ev.EndDate) ||
                            (form.Start <= ev.StartDate && form.End >= ev.EndDate)
                        )
                    );

                    if (overlapping)
                    {
                        var currentOverlap = events.Where(ev => ev.Bus.Id == form.Bus.Id &&
                        (
                            (form.Start >= ev.StartDate && form.Start < ev.EndDate) ||
                            (form.End > ev.StartDate && form.End <= ev.EndDate) ||
                            (form.Start <= ev.StartDate && form.End >= ev.EndDate)
                        )).FirstOrDefault();
                        messageStore.Add(new FieldIdentifier(form, nameof(form.Start)), $"Ütközés más eseménnyel: {currentOverlap.Description} - {currentOverlap.StartDate.ToString("yyyy.MM.dd")} - {currentOverlap.EndDate.ToString("yyyy.MM.dd")} ");
                        messageStore.Add(new FieldIdentifier(form, nameof(form.End)), $"Ütközés más eseménnyel: {currentOverlap.Description} - {currentOverlap.StartDate.ToString("yyyy.MM.dd")} - {currentOverlap.EndDate.ToString("yyyy.MM.dd")} ");
                    }
                }
            }



            editContext.NotifyValidationStateChanged();
            isFormValid = editContext.Validate();
            StateHasChanged();
        }


        public async Task OnSave()
        {
            var request = new CreateEventCommand()
            {

                Bus = form.Bus,
                StartDate = form.Start,
                EndDate = form.End,
                Type = EventTypes.Service,
                ServiceType = form.Type

            };
            var result = await EventService.Create(request, _cancellationTokenSource.Token);
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
        private void Close()
        {
            DialogService.Close(null);
        }
    }

}
