using Terjeki.Scheduler.Web.Components.Users;

namespace Terjeki.Scheduler.Web.Pages
{
    public partial class Users
    {
        [Inject] IAllowedEmailService UserService { get; set; }

        private IList<AllowedEmailModel> users = new List<AllowedEmailModel>();

        protected override async Task OnInitializedAsync()
        {

            await Refresh();
        }
        private async Task Refresh()
        {
           
            var allUser = await UserService.GetAllAsync();
            users = allUser.ToList();
            StateHasChanged();
        }
        private async Task OnCreate()
        {
            var result = await DialogService.OpenAsync<CreateUserDialog>($"Felhasználó felvitele");
            if (result != null) await Refresh();

        }
        private async Task OnEdit(AllowedEmailModel user)
        {
            var parameters = new Dictionary<string, object>() { { "Selected", user } };
            var result = await DialogService.OpenAsync<UpdateUserDialog>($"{user.Name} adatainak módosítása", parameters);
            if (result != null) await Refresh();
        }
        private async Task OnDelete(Guid id)
        {
            var result = await UserService.DeleteAsync(new DeleteAllowedEmailCommand(id));
            if (result) await Refresh();
        }
    }
}
