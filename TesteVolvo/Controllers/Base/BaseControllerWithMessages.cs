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
    }
}
