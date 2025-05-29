using Microsoft.AspNetCore.Components.Forms;

namespace Terjeki.Scheduler.Web.Components.Users
{
    public partial class CreateUserDialog
    {
        [Inject] IUserService UserService { get; set; }
       
        private CreateUserForm form = new();

        private EditContext editContext = new(new CreateUserForm());

        private bool isFormValid = false;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private ValidationMessageStore messageStore;

        private List<UserModel> users;
        private List<RoleModel> roles;
        protected override async Task OnInitializedAsync()
        {
            var allRoles = await UserService.GetRolesAsync(_cancellationTokenSource.Token);
            roles = allRoles.ToList();
            var allUser = await UserService.GetAllAsync(_cancellationTokenSource.Token);
            users = allUser.ToList();

            editContext = new EditContext(form);
            messageStore = new ValidationMessageStore(editContext);
            editContext.OnFieldChanged += ValidateForm;
            form.PropertyChanged += OnFieldChanged;

            await base.OnInitializedAsync();
        }

        private void OnFieldChanged(object? sender, PropertyChangedEventArgs e)
        {
            editContext.NotifyFieldChanged(new FieldIdentifier(form, nameof(CreateUserForm.Name)));
        }

        private void ValidateForm(object? sender, FieldChangedEventArgs e)
        {
            if (e.FieldIdentifier.FieldName == nameof(CreateUserForm.Name))
            {
                ValidateName();
            }

            isFormValid = editContext.Validate();
            StateHasChanged();
        }

        private void ValidateName()
        {

            messageStore.Clear(new FieldIdentifier(form, nameof(CreateUserForm.Name)));

            if (users.Any(n => n.Name.Equals(form.Name, StringComparison.OrdinalIgnoreCase) || n.Email.Equals(form.Email, StringComparison.OrdinalIgnoreCase)))
            {
                messageStore.Add(new FieldIdentifier(form, nameof(CreateUserForm.Name)), $"A '{form.Name}' nevű felhasználó már létezik");
            }

            editContext.NotifyValidationStateChanged();
        }
        public async Task OnSave()
        {
            if (!string.IsNullOrEmpty(form.Name))
            {
                var request = new CreateUserCommand()
                {

                    Name = form.Name,
                    Email = form.Email,
                    Role = form.Role,
                };
                var result = await UserService.CreateAsync(request, _cancellationTokenSource.Token);
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
    }
}
