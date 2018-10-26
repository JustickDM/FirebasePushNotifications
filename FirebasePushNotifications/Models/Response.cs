using System;

namespace FirebasePushNotifications.Models
{
    public class Response
    {
        public Exception Exception { get; set; }

        public MessageResponse MessageResult { get; set; }
    }
}
