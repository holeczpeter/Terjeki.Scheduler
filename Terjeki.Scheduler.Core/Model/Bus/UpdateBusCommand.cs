namespace Terjeki.Scheduler.Core
{
    public class UpdateBusCommand : CreateBusCommand
    {
        [Required]
        public Guid Id { get; set; }
    }
}
