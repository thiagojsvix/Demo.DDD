using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Demo.DDD.Shared.Validations;
using FluentValidation.Results;

namespace Demo.DDD.Shared.Notifications
{
    [ExcludeFromCodeCoverage]
    public class NotificationList
    {

        private readonly List<Notification> notifications;
        public IReadOnlyCollection<Notification> Notifications => this.notifications;
        public bool HasNotifications => this.notifications.Any();

        /// <summary>
        /// Obtem Mensagens de notificações separadas por linha
        /// </summary>
        public override string ToString() => string.Join(Environment.NewLine, this.GetMessages());
        private IEnumerable<string> GetMessages() => this.Notifications.Select(notification => notification.Message);

        public NotificationList()
        {
            this.notifications = new List<Notification>();
        }

        public void AddNotification(string key, string message) => this.notifications.Add(new Notification(key, message));

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                this.AddNotification(error.ErrorCode, error.ErrorMessage);
        }

        public void AddNotifications(params IValidation[] notificationList)
        {
            foreach (var notification in notificationList.Where(x => x.Invalid))
                this.AddNotifications(notification.ValidationResult);
        }
    }
}
