using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Drivers
{
    public partial class CreateDriverDialog
    {
        [Inject]

        public IDriverService DriverService { get; set; }
        [Inject]

        public IBusService BusService { get; set; }

        [Inject]

        public IUserService UserService { get; set; }


        private CreateDriverForm form = new CreateDriverForm();

        private EditContext editContext = new(new CreateDriverForm());

        private List<UserModel> users = new();
        private UserModel? selectedUser;
        private List<BusModel> buses = new();
        private List<RadioOption> Options = new List<RadioOption> {
                                           new RadioOption { Value = true, Label = "Felhasználó" },
                                           new RadioOption { Value = false, Label = "Alkalmi sofőr" }
                                       };
        protected override async Task OnInitializedAsync()
        {
           
            var allBus = await BusService.GetAll();
            buses = allBus.ToList();

            users = (await UserService.GetAllDrivers()).ToList();

            editContext = new EditContext(form);
            
            form.PropertyChanged += (_, __) => editContext.NotifyValidationStateChanged();

            await base.OnInitializedAsync();
        }

        private void OnUserChanged(object? args)
        {
          
            if (selectedUser != null)
            {
                form.DriverUserId = selectedUser.Id;
            }
            else
            {
                form.DriverUserId = null;
            }
        }

        private async Task OnSave()
        {
            
            if (editContext.Validate())
            {
               
                var request = new CreateDriverCommand
                {
                   
                    DriverUserId = form.IsExistingUser ? form.DriverUserId : null,
                    DriverName = form.IsExistingUser ? selectedUser.FullName : form.DriverName,
                    BusId = form.Bus?.Id
                };

                var created = await DriverService.Create(request);
                if (created == null)
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
                        Summary = "Sikeres rögzítés",
                        Duration = 2000
                    });
                    DialogService.Close(created);
                }
            }
        }

        private void Close()
        {
            DialogService.Close(null);
        }

        
        private class RadioOption
        {
            public bool Value { get; set; }
            public string Label { get; set; } = string.Empty;
        }
    }
}
