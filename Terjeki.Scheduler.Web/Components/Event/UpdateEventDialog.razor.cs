using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Event
{
    public partial class UpdateEventDialog
    {
        [Inject] IBusService BusService { get; set; }
        [Inject] ICapacityService CapacityService { get; set; }
        [Inject] IDriverService DriverService { get; set; }
        [Inject] IEventService EventService { get; set; }
        [Parameter]
        public EventModel Selected { get; set; } = default!;

        private UpdateEventForm form = new();

        private EditContext editContext = new(new UpdateEventForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;

        private List<CapacityModel> capacities;

        private List<BusItemModel> buses;

        private List<EventModel> events;

        private List<DriverItemModel> drivers;

        protected override async Task OnInitializedAsync()
        {
            var allCapacities = await CapacityService.GetAll(_cancellationTokenSource.Token);
            capacities = allCapacities.ToList();

            var allDrivers = await DriverService.GetAll(_cancellationTokenSource.Token);
            drivers = allDrivers.Select(x => new DriverItemModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var allBus = await BusService.GetAll(_cancellationTokenSource.Token);
            buses = allBus.Select(x => new BusItemModel()
            {
                Id = x.Id,
                Brand = x.Brand,
                CurrentMileage = x.CurrentMileage,
                LicensePlateNumber = x.LicensePlateNumber
            }).ToList();

            var allEvents = await EventService.GetEvents(_cancellationTokenSource.Token);
            events = allEvents.ToList();


            form = new UpdateEventForm()
            {
                Id = Selected.Id,
                Bus = Selected.Bus,
                Capacity = Selected.Capacity,
                Description = Selected.Description,
                Start = Selected.StartDate,
                End = Selected.EndDate,
                Drivers = Selected.Drivers,
            };
            editContext = new EditContext(form);
            messageStore = new ValidationMessageStore(editContext);
            editContext.OnFieldChanged += ValidateForm;
            form.PropertyChanged += OnFieldChanged;

            await base.OnInitializedAsync();
        }
        private async Task ChangeCapacity(object change)
        {
            if (Int32.TryParse(change?.ToString(), out int currentCapacity))
            {
                //buses = buses.Where(x => x.Capacity.Capacity == currentCapacity).ToList();
            }
        }
        private void OnFieldChanged(object? sender, PropertyChangedEventArgs e)
        {
            // editContext.NotifyFieldChanged(new FieldIdentifier(form, nameof(UpdateEventForm.Type)));
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
                        ev.Id != form.Id &&
                        ev.Bus != null &&
                        ev.Bus.Id == form.Bus.Id &&
                        (
                            (form.Start >= ev.StartDate && form.Start < ev.EndDate) ||
                            (form.End > ev.StartDate && form.End <= ev.EndDate) ||
                            (form.Start <= ev.StartDate && form.End >= ev.EndDate)
                        )
                    );

                    if (overlapping)
                    {
                        var currentOverlap = events.Where(ev => ev.Id != form.Id &&
                            ev.Bus != null &&
                            ev.Bus.Id == form.Bus.Id &&
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
            var request = new UpdateEventCommand()
            {
                Id = form.Id,
                DriverIds = form.Drivers.Select(x=>x.Id).ToList(),
                Capacity = form.Capacity.Seats,
                Description = form.Description,
                BusId = form.Bus.Id,
                StartDate = form.Start,
                EndDate = form.End,
                Type = EventTypes.Event,
                Status = form.Bus != null ? EventStatuses.Accepted : EventStatuses.Plan

            };
            var result = await EventService.Update(request, _cancellationTokenSource.Token);
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
                    Summary = $"Sikeres módosítás",
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
