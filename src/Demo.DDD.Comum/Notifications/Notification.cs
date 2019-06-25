using System.Diagnostics.CodeAnalysis;

namespace Demo.DDD.Shared.Notifications
{
    [ExcludeFromCodeCoverage]
    public class Notification
    {
        public string Key { get; }
        public string Message { get; }

        public Notification(string key, string message)
        {
            this.Key = key;
            this.Message = message;
        }
    }
}
