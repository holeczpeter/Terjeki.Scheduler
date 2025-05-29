using Microsoft.AspNetCore.Components.Forms;
using Terjeki.Scheduler.Web.Forms;

namespace Terjeki.Scheduler.Web.Components.Holiday
{
    public partial class CreateHolidayDialog
    {
       
        [Inject] IDriverService DriverService { get; set; }
        [Inject] IEventService EventService { get; set; }

        [Inject] IHolidayService HolidayService { get; set; }

        private CreateHolidayForm form = new();

        private EditContext editContext = new(new CreateHolidayForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;


        private List<HolidayModel> holidayTypes;
        private List<DriverModel> drivers;
        private List<EventModel> events;
        protected override async Task OnInitializedAsync()
        {
            var allEvents = await EventService.GetEvents(_cancellationTokenSource.Token);
            events = allEvents.ToList();
            var allDrivers = await DriverService.GetAll(_cancellationTokenSource.Token);
            drivers = allDrivers.ToList();

            var allholidayTypes = await HolidayService.GetAllTypesAsync(_cancellationTokenSource.Token);
            holidayTypes = allholidayTypes.ToList();

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

                if (form.Driver != null)
                {
                    var overlapping = events.Any(ev =>
                        ev.Drivers != null &&
                        ev.Drivers.Contains(form.Driver) &&
                        (
                            (form.Start >= ev.StartDate && form.Start < ev.EndDate) ||
                            (form.End > ev.StartDate && form.End <= ev.EndDate) ||
                            (form.Start <= ev.StartDate && form.End >= ev.EndDate)
                        )
                    );

                    if (overlapping)
                    {
                        var currentOverlap = events.Where(ev => ev.Drivers != null && ev.Drivers.Contains(form.Driver) &&
                        (
                            (form.Start >= ev.StartDate && form.Start < ev.EndDate) ||
                            (form.End > ev.StartDate && form.End <= ev.EndDate) ||
                            (form.Start <= ev.StartDate && form.End >= ev.EndDate)
                        )).FirstOrDefault();
                        messageStore.Add(new FieldIdentifier(form, nameof(form.Start)), $"Ütközés más eseménnyel: {currentOverlap.Description} - {currentOverlap.StartDate.Date.ToString("yyyy.MM.dd")} - {currentOverlap.EndDate.Date.ToString("yyyy.MM.dd")} ");
                        messageStore.Add(new FieldIdentifier(form, nameof(form.End)), $"Ütközés más eseménnyel: {currentOverlap.Description} - {currentOverlap.StartDate.Date.ToString("yyyy.MM.dd")} - {currentOverlap.EndDate.Date.ToString("yyyy.MM.dd")} ");
                    }
                }
            }



            editContext.NotifyValidationStateChanged();
            isFormValid = editContext.Validate();
            StateHasChanged();
        }


        public async Task OnSave()
        {
            var request = new CreateHolidayCommand()
            {

                Driver = form.Driver,
                Description = form.Description,
                StartDate = form.Start,
                EndDate = form.End,
                Type = EventTypes.Holiday,
                HolidayType = form.Type,

            };
            var result = await HolidayService.CreateAsync(request, _cancellationTokenSource.Token);
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
