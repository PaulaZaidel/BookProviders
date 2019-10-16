using BookProviders.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookProviders.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            // If not has notification(error) return true
            return !_notifier.HasNotification();
        }

    }
}
