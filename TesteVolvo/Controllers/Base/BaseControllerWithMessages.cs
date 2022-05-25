using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TesteVolvo.Controllers.Base
{
    public class BaseControllerWithMessages : Controller
    {
        private readonly INotyfService _notyf;

        public BaseControllerWithMessages(INotyfService notyf)
        {
            _notyf = notyf;
        }

        protected void WriteSuccessMessage(string message)
        {
            _notyf.Success(message);
        }

        protected void WriteWarningMessage(string message)
        {
            _notyf.Warning(message);
        }

        protected void WriteErrorMessage(string message)
        {
            _notyf.Error(message);
        }

        protected void WriteErrorMultipleNotifications(IReadOnlyCollection<Flunt.Notifications.Notification> notifications)
        {
            string finalMessage = string.Empty;
            int messageCount = 1;

            foreach (var notification in notifications)
            {
                if (messageCount == 1)
                {
                    finalMessage += $"{notification.Message}";
                }
                else
                {
                    finalMessage += $"\n{notification.Message}";
                }

                messageCount ++;
            }

            _notyf.Error(finalMessage);
        }
    }
}
