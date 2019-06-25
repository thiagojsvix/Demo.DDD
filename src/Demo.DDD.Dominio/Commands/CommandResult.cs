using Demo.DDD.Shared.Commands;
using Demo.DDD.Shared.Notifications;

namespace Demo.DDD.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }

        public CommandResult(bool sucess, string message)
        {
            this.Sucess = sucess;
            this.Message = message;
        }

        public CommandResult(NotificationList notificationList)
        {
            this.Sucess = !notificationList.HasNotifications;
            this.Message = notificationList.ToString();
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
    }
}
