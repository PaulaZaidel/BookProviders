
using BookProviders.Business.Notifications;
using System.Collections.Generic;

namespace BookProviders.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
