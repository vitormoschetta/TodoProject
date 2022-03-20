using System.Threading.Tasks;
using AntDesign;

namespace Todo.App.Helpers
{
    public class TodoNotification
    {
        private readonly NotificationService _notificationService;

        public TodoNotification(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async void NoticeWithIcon(string message, string description, NotificationType type, NotificationPlacement placement = NotificationPlacement.TopRight)
        {
            await _notificationService.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }
    }
}