namespace Terjeki.Scheduler.Core
{
    public class MessageModel: INotification
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
