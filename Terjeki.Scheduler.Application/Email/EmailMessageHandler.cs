using Terjeki.Scheduler.Core.Services;

namespace Terjeki.Scheduler.Application
{
    public class EmailMessageHandler : INotificationHandler<MessageModel>
    {
        private readonly IEmailService _emailService;
        public EmailMessageHandler(IEmailService emailService)
        {
            this._emailService = emailService;
        }
        public async Task Handle(MessageModel notification, CancellationToken cancellationToken)
        {
           await _emailService.SendEmailAsync(notification.To, notification.Subject, notification.Body);
           
        }
    }
}
